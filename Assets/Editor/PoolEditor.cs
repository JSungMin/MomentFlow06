using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

using Struct;

[CustomEditor(typeof(PositionPool))]
public class PoolEditor : Editor {
	#if UNITY_EDITOR
	PositionPool positionPool;
	List<EventItem> eventItemList;
	List<PositionItem> positionItemList;
	List<DurationItem> durationItemList;

	public void OnSceneGUI()
	{
		positionPool = target as PositionPool;
		if (null == positionPool)
		{
			return;
		}
		eventItemList = positionPool.eventItemList;
		positionItemList = positionPool.positionItemList;
		durationItemList = positionPool.durationItemList;

		for (int i = 0; i < eventItemList.Count; i++)
		{
			ShowEventPoint (i);

			ShowExtraButton (i);

			GUIStyle newStyle = new GUIStyle ();
			newStyle.normal.textColor = Color.cyan;
			newStyle.fontSize = 20;
			newStyle.alignment = TextAnchor.MiddleCenter;
			Vector3 point = GetEventPosition (i);
			Handles.Label (point + Vector3.down * 3f * handleSize, ((durationItemList[eventItemList[i].targetIndex].duration) * eventItemList[i].eventTimeList).ToString("N2"), newStyle);
		}

	}

	private Vector3 GetEventPosition (int i)
	{
		var eventIndex = eventItemList [i].targetIndex;
		var point = Vector3.Lerp (positionItemList [eventIndex].transform.position, positionItemList [Mathf.Min (eventIndex + 1, positionPool.size - 1)].transform.position, eventItemList [i].eventTimeList / durationItemList [eventIndex].duration);

		return point; 
	}

	private const float handleSize = 0.04f;
	private const float pickSize = 0.06f;

	private int selectedIndex = -1;

	public void ShowEventPoint (int index)
	{
		var eventItem = positionPool.eventItemList [index];
		Vector3 point = GetEventPosition (index);
		float size = HandleUtility.GetHandleSize(point);

		if (Handles.Button(point + handleSize * Vector3.down * 2f, Quaternion.identity, size*handleSize, pickSize, Handles.DotCap)) {
			selectedIndex = index;
			Repaint ();
		}

		if (selectedIndex == index)
		{
			EditorGUI.BeginChangeCheck ();
			point = Handles.DoPositionHandle (point, Quaternion.identity);


			if (EditorGUI.EndChangeCheck())
			{
				var prevPoint = GetEventPosition (index);
				var deltaLength = (point.x - positionItemList[eventItem.targetIndex].transform.position.x);
				var targetLength = (positionItemList[eventItem.targetIndex + 1].transform.position.x - positionItemList[eventItem.targetIndex].transform.position.x);

				var edittedIndex = eventItem.targetIndex;

				if (deltaLength / targetLength >= 1)
				{
					edittedIndex += 1;
				}
				else if (deltaLength / targetLength < 0)
				{
					edittedIndex -= 1;
				}
				edittedIndex = Mathf.Clamp (edittedIndex, 0, durationItemList.Count - 1);

				deltaLength = (point.x - positionItemList[edittedIndex].transform.position.x);
				targetLength = (positionItemList [edittedIndex + 1].transform.position.x - positionItemList [edittedIndex].transform.position.x);

				var edittedTime = Mathf.Clamp((durationItemList[edittedIndex].duration) * deltaLength / targetLength,0,(durationItemList[edittedIndex].duration));

				positionPool.eventItemList [index] = new EventItem (
					eventItem.eventName,
					edittedIndex,
					edittedTime,
					eventItem.OccurEvent
				);
				Undo.RecordObject (positionPool, "Move Point");
				EditorUtility.SetDirty (positionPool);

			}
		}
	}

	public void ShowExtraButton (int index)
	{
		if (index == selectedIndex)
		{
			Vector3 point = GetEventPosition (index);
			float size = HandleUtility.GetHandleSize(point);

			var uiPoint = HandleUtility.WorldToGUIPoint (point);

			Handles.BeginGUI ();
			float width = 150;
			float height = 100;
			GUILayout.Window (2, new Rect (uiPoint.x + width * 0.3f, uiPoint.y - height * 1.3f, width, height), (id) => {
				GUILayout.Button ("A Button");
			}, eventItemList [index].eventName);
			Handles.EndGUI ();
		}
	}
	#endif
}
