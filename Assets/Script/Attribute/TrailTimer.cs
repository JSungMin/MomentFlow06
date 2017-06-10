using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailTimer : MonoBehaviour {
	private TrailRenderer trail;

	// Use this for initialization
	void Start () {
		trail = GetComponent<TrailRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (TimeManager.GetInstance().customTimeScale == 0)
		{
			trail.Clear ();
		}
	}
}
