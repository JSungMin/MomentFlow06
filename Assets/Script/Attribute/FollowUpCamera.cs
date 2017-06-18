using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowUpCamera : MonoBehaviour {

	public Transform followTarget;

	public Vector2 noneFollowOffset;

	private float minWidth, minHeight;
	private float maxWidth, maxHeight;

	private Vector2 deltaPos;

	// Use this for initialization
	void Start () {
		minWidth = -noneFollowOffset.x;
		maxWidth = noneFollowOffset.x;
		minHeight = -noneFollowOffset.y;
		maxHeight = noneFollowOffset.y;
	}
	
	// Update is called once per frame
	void Update () {
		deltaPos = new Vector2 (transform.position.x - followTarget.position.x, transform.position.y - followTarget.position.y);
	}
}
