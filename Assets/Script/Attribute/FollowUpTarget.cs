using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowUpTarget : MonoBehaviour {

	public Transform followTarget;
	public Vector3 offset;
	
	// Update is called once per frame
	void Update () {
		transform.position = followTarget.transform.position + offset;
	}
}
