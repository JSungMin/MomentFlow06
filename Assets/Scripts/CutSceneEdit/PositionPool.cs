using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Events;
#endif

using Struct;

public class PositionPool : MonoBehaviour {
	public CutSceneUnit unit;

	public Color trackColor;

	public bool selected {
		private set;
		get;
	}
	public int trackIndex;
	public int size;
	public int offset = 0;

	//Independece with size
	public List<PositionItem> positionItemList = new List<PositionItem>();
	public List<DurationItem> durationItemList = new List<DurationItem>();
	public List<CurveItem> curveItemList = new List<CurveItem>(); 

	//not dependence with size
	public List<EventItem> eventItemList = new List<EventItem>();
	// Use this for initialization
	void OnEnable () {
		if(unit == null)
			unit = transform.parent.GetComponentInChildren<CutSceneUnit> ();
	}

	void OnValidate(){
		#if UNITY_EDITOR
		//OccurEvent의 크기와 Event Item List의 크기를 같게 조정함
		for (int i = 0; i < eventItemList.Count; i++) {		
			var eventIndex = eventItemList [i].targetIndex;

			if (eventItemList [i].OccurEvent.GetPersistentEventCount() > 1)
			{
				for (int j = 1; j < eventItemList [i].OccurEvent.GetPersistentEventCount(); j++)
				{
					
					UnityEventTools.UnregisterPersistentListener(eventItemList[i].OccurEvent,j);
				}
				eventItemList.Add(new EventItem("New Event",0,0,new UnityEvent()));
			}
		}
		#endif
	}
	void OnDrawGizmos(){
		#if UNITY_EDITOR
		if (size < 2)
			size = 2;

		//size에 맞게 PositionItemList를 조정한다.
		{
			while (size > positionItemList.Count) {
				GameObject newPosition = new GameObject ((positionItemList.Count + 1).ToString ());
				newPosition.transform.parent = transform;
				if (positionItemList.Count != 0)
					newPosition.transform.position = positionItemList [positionItemList.Count - 1].transform.position;
				else
					newPosition.transform.position = Vector3.zero;
				newPosition.AddComponent<SnapOnGrid> ();
				newPosition.AddComponent<HierarchySystem> ();
				newPosition.GetComponent<HierarchySystem> ().SetParent (transform,unit);
				newPosition.GetComponent<HierarchySystem> ().index = positionItemList.Count;

				var newItem = new PositionItem (positionItemList.Count, newPosition.transform);
				positionItemList.Add (newItem);
			}

			while (size < positionItemList.Count) {
				DestroyImmediate (transform.GetChild(positionItemList.Count-1).gameObject);
				positionItemList.RemoveAt (positionItemList.Count - 1);
			}
		}

		//size-1에 맞게 durationItemList를 조정한다.
		{
			while (size - 1 > durationItemList.Count) {
				var newItem = new DurationItem (durationItemList.Count, 1);
				durationItemList.Add (newItem);
			}
			while(size-1<durationItemList.Count){
				durationItemList.RemoveAt (durationItemList.Count-1);
			}
		}
		//size-1에 맞게 curveItemList를 조정한다.
		{
			while (size - 1 > curveItemList.Count) {
				AnimationCurve newCurve = new AnimationCurve ();
				var newKey01 = new Keyframe (0, 0,0,0);
				var newKey02 = new Keyframe (1, 1,0,0);
				newCurve.AddKey (newKey01);
				newCurve.AddKey (newKey02);

				var newItem = new CurveItem (curveItemList.Count, newCurve);
				curveItemList.Add (newItem);
			}
			while(size-1<curveItemList.Count){
				curveItemList.RemoveAt (curveItemList.Count-1);
			}
		}

		//Set Select State
		if (UnityEditor.Selection.activeGameObject != null) {
			if (UnityEditor.Selection.activeGameObject == gameObject) {
				selected = true;
			} else if (UnityEditor.Selection.activeTransform.parent != null && UnityEditor.Selection.activeTransform.parent.GetComponent<PositionPool>() != null ||
				UnityEditor.Selection.activeTransform.GetComponent<PositionPool>() != null ||
				UnityEditor.Selection.activeTransform.parent == this.transform) {
				selected = true;
			} else {
				selected = false;
			}
		}

		//Show Position and Event Icon
		if(selected||unit.pinPath){
			for(int i = 0; i < size; i++){
				float tmpSize = 1f;
				tmpSize = HandleUtility.GetHandleSize(positionItemList[i].transform.position);
				GUIStyle newStyle = new GUIStyle ();
				newStyle.normal.textColor = CutSceneManager.GetInstance.fontColor;
				newStyle.fontSize = 20;
				newStyle.alignment = TextAnchor.MiddleCenter;
				Handles.Label (positionItemList[i].transform.position + Vector3.up*0.1f + Vector3.up*tmpSize*0.3f,(i + 1).ToString(),newStyle);
				for(int j = i; j < size; j++){
					if(positionItemList[i].index == positionItemList[j].index - 1){
						Gizmos.DrawIcon (positionItemList [i].transform.position + Vector3.up*0.05f, "Position.png",true);
						Gizmos.DrawIcon (positionItemList [j].transform.position + Vector3.up*0.05f, "Position.png",true);
					
						var tmpColor = new Color (trackColor.r - ((trackColor.r-0.3f)/(float)size)*positionItemList[i].index,trackColor.g - ((trackColor.g-0.3f)/(float)size)*positionItemList[i].index,trackColor.b - ((trackColor.b-0.3f)/(float)size)*positionItemList[i].index);

						Gizmos.color = tmpColor;
						if(positionItemList[i].transform.GetComponent<BeizerSpline>()==null)
							Gizmos.DrawLine (positionItemList[i].transform.position,positionItemList[j].transform.position);
					}
				}

				for (int j = 0; j < eventItemList.Count; j++)
				{
					var eventIndex = eventItemList [j].targetIndex;
					var pos = Vector3.Lerp(positionItemList[eventIndex].transform.position, positionItemList[Mathf.Min(eventIndex + 1, size - 1)].transform.position, eventItemList[j].eventTimeList / durationItemList[eventIndex].duration);
				
					Gizmos.DrawIcon (pos,"Event.png",true);
				}
			}
		}
		#endif
	}
	// Update is called once per frame
	void Update () {
		
	}
}
