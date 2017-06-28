using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using Struct;

[CustomEditor(typeof(HierarchySystem))]
public class HierachySystemEditor : Editor {
	#if UNITY_EDITOR

	HierarchySystem hierachySystem;
	List<DurationItem> durationItemList;
	List<CurveItem> curveItemList;
	float width = 150f;
	float height = 100f;

	public void OnSceneGUI ()
	{
		hierachySystem = target as HierarchySystem;

		if (null == hierachySystem)
		{
			return;
		}

		durationItemList = hierachySystem.positionPool.durationItemList;
		curveItemList = hierachySystem.positionPool.curveItemList;

		hierachySystem.durationCurve = curveItemList [Mathf.Min (hierachySystem.index, curveItemList.Count - 1)].curve;

		SceneView.lastActiveSceneView.LookAt (hierachySystem.transform.position);

		Vector3 uiPoint = HandleUtility.WorldToGUIPoint (hierachySystem.transform.position);
		GUILayout.Window (2, new Rect (uiPoint.x + width * 0.3f, uiPoint.y + height, width, height), (id) => {
			EditorGUILayout.BeginHorizontal ("Duration Area");
			GUILayout.Label ("Duration");
			EditorGUILayout.FloatField (durationItemList[Mathf.Min (hierachySystem.index,durationItemList.Count -1)].duration);
			EditorGUILayout.EndHorizontal ();

			EditorGUILayout.BeginVertical ("Curve Area");

			SerializedObject sPropObj = serializedObject;

			SerializedProperty sProp = sPropObj.FindProperty ("durationCurve");
			EditorGUIUtility.LookLikeInspector();

			EditorGUILayout.PropertyField (sProp);
			serializedObject.ApplyModifiedProperties();

			EditorGUILayout.EndVertical ();

		}, "Duration Curve");

	}

	#endif
}
