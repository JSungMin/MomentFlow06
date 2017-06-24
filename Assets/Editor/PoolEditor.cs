using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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

	List<int> tmpIndexList = new List<int>();

	float width = 150;
	float height = 30;

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

		ShowEventManagerWindow ();

		tmpIndexList = new List<int> ();
		for (int i = 0; i < positionItemList.Count; i++)
		{
			tmpIndexList.Add (0);
		}

		for (int i = 0; i < eventItemList.Count; i++)
		{
			GUIStyle newStyle = new GUIStyle ();
			newStyle.normal.textColor = Color.cyan;
			newStyle.fontSize = (int)(20 * handleSize);
			newStyle.alignment = TextAnchor.MiddleCenter;
			Vector3 point = GetEventPosition (i);
			point += tmpIndexList [eventItemList[i].targetIndex] * Vector3.down * handleSize * 5f;
			Handles.Label (point + Vector3.down * 3f * handleSize, (eventItemList[i].eventTimeList).ToString("N2"), newStyle);
			newStyle.normal.textColor = new Color (1f,0.5f,0.18f);
			Handles.Label (point + Vector3.up * 0.2f * handleSize, eventItemList[i].eventName, newStyle);

			ShowEventPoint (i);

			ShowIndexedEvent (i);

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

	void ShowEventManagerWindow ()
	{
		Handles.BeginGUI ();
		width = 150;
		height = 100;
		GUILayout.Window (2, new Rect (Screen.width - width * 1.2f, Screen.height * 0.8f, width, height), id =>  {
			GUILayout.Space (10f);
			if (GUILayout.Button ("Create Event")) {
				if (selectedIndex == -1)
					selectedIndex = 0;
				selectedIndex = Mathf.Clamp (selectedIndex,0,eventItemList.Count - 1);
				positionPool.eventItemList.Add (new EventItem ("New Event",selectedIndex, 0, new UnityEvent ()));
			}
			GUILayout.FlexibleSpace ();
			if (GUILayout.Button ("Delete Event")) {
				if (selectedIndex != -1)
					positionPool.eventItemList.RemoveAt (selectedIndex);
			}
			GUILayout.Space (10f);
		}, "Event Manager");
		Handles.EndGUI ();
	}

	public void ShowEventPoint (int index)
	{
		var eventItem = positionPool.eventItemList [index];
		Vector3 point = GetEventPosition (index);
		float size = HandleUtility.GetHandleSize(point);
		point += tmpIndexList [eventItem.targetIndex] * Vector3.down * handleSize * 5f;
		tmpIndexList [eventItem.targetIndex] += 1;


		if (Handles.Button(point + handleSize * Vector3.down * 2f, Quaternion.identity, size*handleSize, pickSize, Handles.DotCap)) {
			selectedIndex = index;
			SceneView.lastActiveSceneView.LookAt (point);
			Repaint ();
		}

		if (selectedIndex == index)
		{
			EditorGUI.BeginChangeCheck ();
			point = Handles.DoPositionHandle (point, Quaternion.identity);


			if (EditorGUI.EndChangeCheck())
			{
				var prevPoint = GetEventPosition (index);
				var deltaTargetPos = (positionItemList[eventItem.targetIndex + 1].transform.position - positionItemList[eventItem.targetIndex].transform.position);
				var deltaLength = 0f;
				var targetLength = 0f;

				if (Mathf.Abs (deltaTargetPos.x) > Mathf.Abs (deltaTargetPos.y)) {
					deltaLength = point.x - positionItemList [eventItem.targetIndex].transform.position.x;
					targetLength = (positionItemList [eventItem.targetIndex + 1].transform.position.x - positionItemList [eventItem.targetIndex].transform.position.x);
				} else {
					deltaLength = point.y - positionItemList [eventItem.targetIndex].transform.position.y;
					targetLength = (positionItemList [eventItem.targetIndex + 1].transform.position.y - positionItemList [eventItem.targetIndex].transform.position.y);
				}

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

				if (Mathf.Abs (deltaTargetPos.x) > Mathf.Abs (deltaTargetPos.y)) {
					deltaLength = point.x - positionItemList [edittedIndex].transform.position.x;
					targetLength = (positionItemList [edittedIndex + 1].transform.position.x - positionItemList [edittedIndex].transform.position.x);
				} else {
					deltaLength = point.y - positionItemList [edittedIndex].transform.position.y;
					targetLength = (positionItemList [edittedIndex + 1].transform.position.y - positionItemList [edittedIndex].transform.position.y);
				}

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
	Rect windowRect;
	int changedIndex = 0;
	float changedTime = 0f;
	string changedName;
	bool minimize = false;

	public void ShowIndexedEvent (int index)
	{
		if (index == selectedIndex)
		{
			var eventItem = positionPool.eventItemList [index];

			Vector3 point = GetEventPosition (index);
			float size = HandleUtility.GetHandleSize(point);

			var uiPoint = HandleUtility.WorldToGUIPoint (point);

			Handles.BeginGUI ();

			windowRect = GUILayout.Window (3, new Rect (uiPoint.x + width * 0.3f, uiPoint.y - height * 2, width, height), (id) => {
				EditorGUILayout.BeginHorizontal("Name Area");
				GUILayout.Label ("Name");
				changedName = EditorGUILayout.TextField(eventItem.eventName);
				EditorGUILayout.EndHorizontal();

				EditorGUILayout.BeginHorizontal("Index Area");
				GUILayout.Label ("Index");
				changedIndex = EditorGUILayout.IntField(eventItem.targetIndex);
				EditorGUILayout.EndHorizontal();

				EditorGUILayout.BeginHorizontal("Event Time Area");
				GUILayout.Label ("Event Time");
				changedTime = EditorGUILayout.FloatField(eventItem.eventTimeList);
				EditorGUILayout.EndHorizontal();
				if (minimize)
				{
					if (GUILayout.Button ("Normal"))
					{
						minimize = false;
					}
				}
				else
				{
				if (eventItem.OccurEvent.GetPersistentEventCount() != 0)
				{
					var objectName = eventItem.OccurEvent.GetPersistentTarget(0);
					EditorGUILayout.BeginHorizontal ("Object Area");
					GUILayout.Label ("Object");
					if (null != objectName)
					{
							var objectPos = GameObject.Find(objectName.name);
							if (GUILayout.Button (objectName.name))
							{
								if (null != objectPos)
									SceneView.lastActiveSceneView.LookAt (objectPos.transform.position);
								else
									Debug.LogError ("Select Object isn't Actived");
							}
					}
					EditorGUILayout.EndHorizontal ();

					EditorGUILayout.BeginHorizontal ("Fuction Area");
					var functionName = eventItem.OccurEvent.GetPersistentMethodName(0);
					GUILayout.Label ("Function");
						
					if (null != functionName)
					{
						GUILayout.Label (functionName);
					}
					EditorGUILayout.EndHorizontal();
					
					EditorGUILayout.BeginVertical("Controller");
					SerializedProperty sProp = serializedObject.FindProperty("eventItemList");
					SerializedProperty childProp = sProp.GetArrayElementAtIndex(index);

					childProp.Next(true);
					//Get Unity Event Prop
					for (int n = 0; n < 5; n++)
					{
						childProp.Next(false);
					}

					EditorGUIUtility.LookLikeControls();
					EditorGUILayout.PropertyField (childProp);
					//childProp.serializedObject.ApplyModifiedProperties();
					serializedObject.ApplyModifiedProperties();
					EditorGUILayout.EndVertical();
				}
				else{
						EditorGUILayout.BeginVertical("Controller");
						SerializedProperty sProp = serializedObject.FindProperty("eventItemList");
						SerializedProperty childProp = sProp.GetArrayElementAtIndex(index);

						childProp.Next(true);
						//Get Unity Event Prop
						for (int n = 0; n < 5; n++)
						{
							childProp.Next(false);
						}
						EditorGUIUtility.LookLikeControls();
						EditorGUILayout.PropertyField (childProp);
						//childProp.serializedObject.ApplyModifiedProperties();
						serializedObject.ApplyModifiedProperties();
						EditorGUILayout.EndVertical();
				}
				if (GUILayout.Button ("Minimize"))
				{
					minimize = true;
				}

				positionPool.eventItemList[index] =  new EventItem (
					changedName,
					changedIndex,
					changedTime,
					eventItem.OccurEvent
				);
				height = GUILayoutUtility.GetLastRect().height;
				}
			}, eventItem.eventName);
			Handles.EndGUI ();
		}
	}
	#endif
}
