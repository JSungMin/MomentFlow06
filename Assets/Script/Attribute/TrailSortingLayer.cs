using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailSortingLayer : MonoBehaviour {

	public TrailRenderer trail;

	// Use this for initialization
	void Start () {
		trail = GetComponent<TrailRenderer> ();
		trail.sortingLayerName = "Items";
	}
}
