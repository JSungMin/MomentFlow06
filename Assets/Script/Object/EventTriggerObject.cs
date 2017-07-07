using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTriggerObject : MonoBehaviour {

	public UnityEvent triggerEvent;
	public LayerMask targetMask;

	public void OnTriggerEnter (Collider col)
	{
		if (((1 << col.gameObject.layer) & targetMask) != 0)
		{
			triggerEvent.Invoke ();
		}
	}
}
