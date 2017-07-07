using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CCTVField : MonoBehaviour {
	public SpriteRenderer cctvRenderer;
	public bool isDetectTarget = false;

	public List<GameObject> detectedObjectList;
	public LayerMask canDetectMask;

	public UnityEvent detectedEvent;
	public GameObject[] affectedObject;

	public Color normalStateColor;
	public Color alertStateColor;

	public void OnTriggerEnter (Collider col)
	{
		if (((1 << col.gameObject.layer) & canDetectMask) != 0)
		{
			if (!detectedObjectList.Contains (col.gameObject))
				detectedObjectList.Add (col.gameObject);
		}
	}

	public void OnTriggerStay (Collider col)
	{
		if (((1 << col.gameObject.layer) & canDetectMask) != 0)
		{
			if (!detectedObjectList.Contains (col.gameObject))
				detectedObjectList.Add (col.gameObject);
		}
	}

	public void OnTriggerExit (Collider col)
	{
		if (((1 << col.gameObject.layer) & canDetectMask) != 0)
		{
			if (detectedObjectList.Contains (col.gameObject))
				detectedObjectList.Remove (col.gameObject);
		}
	}

	public void Update()
	{
		if (detectedObjectList.Count != 0 && !TimeManager.isTimePaused) {
			isDetectTarget = true;
			Debug.Log ("Target Detected By CCTV");
			detectedEvent.Invoke ();
		}
		UpdateCCTVFieldColor ();
	}

	public void UpdateCCTVFieldColor ()
	{
		if (isDetectTarget) {
			cctvRenderer.color = alertStateColor;
		}
		else {
			cctvRenderer.color = normalStateColor;
		}
	}
}
