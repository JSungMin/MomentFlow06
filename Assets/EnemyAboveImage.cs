using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAboveImage : MonoBehaviour {

	public Collider targetCollider;
	public Vector3 offset;
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (targetCollider.transform.position.x, targetCollider.bounds.max.y + offset.y, targetCollider.transform.position.z);
	}
}
