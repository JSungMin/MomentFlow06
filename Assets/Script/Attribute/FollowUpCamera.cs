using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowUpCamera : MonoBehaviour {

	public Transform followTarget;

	public Vector3 followOffset;

	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (
			transform.position.x,
			transform.position.y,
			followTarget.position.z
		) + followOffset;
	}
}
