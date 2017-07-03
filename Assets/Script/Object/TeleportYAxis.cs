using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TeleportYAxis : InteractableObject {
	public LayerMask interactMask;
	public bool canGoUp;
	public bool canGoDown;

	public Transform upPosition;
	public Transform downPosition;

	public UnityEvent interactEvent;

	private GameObject challenger;

	public List<TeleportYAxis> interactSyncObject;

	void Start() {
		doInteractActions += GoUpStair;
		cancelInteractActions += GoDownStair;
	}

	void OnTriggerEnter (Collider col)
	{
		
	}

	#region implemented abstract members of InteractableObject
	public override bool TryInteract (GameObject challenger)
	{
		this.challenger = challenger;
		if (challenger.CompareTag ("Player")) {
			Debug.Log ("Interact With Y");
			if (Input.GetKey (KeyCode.W)) {
				GoUpStair ();
			}
			else if (Input.GetKey (KeyCode.S))
			{
				GoDownStair ();
			}
		}
		return true;
	}
	#endregion

	public void GoUpStair ()
	{
		if (!canGoUp)
			return;
		challenger.transform.position = upPosition.position;
		interactEvent.Invoke ();
	}

	public void GoUpStair (GameObject challenger)
	{
		if (!canGoUp)
			return;
		challenger.transform.position = upPosition.position;
		interactEvent.Invoke ();
	}

	public void GoDownStair ()
	{
		if (!canGoDown)
			return;
		challenger.transform.position = downPosition.position;
	}

	public void GoDownStair (GameObject challenger)
	{
		if (!canGoDown)
			return;
		challenger.transform.position = downPosition.position;
	}

	public static List<TeleportYAxis> FindYUpTeleporters (Vector3 fromPos)
	{
		var teleporter = GameObject.FindObjectsOfType <TeleportYAxis> ();
		var returnList = new List<TeleportYAxis> ();


		for (int i = 0; i < teleporter.Length; i++)
		{
			var teleporterCol = teleporter [i].GetComponent<Collider> ();
			if (teleporterCol.bounds.max.y >= fromPos.y &&
				teleporterCol.bounds.min.y <= fromPos.y &&
				teleporter [i].canGoUp) {
				returnList.Add (teleporter [i]);
			}
		}
		return returnList;
	}

	public static List<TeleportYAxis> FindYDownTeleporters (Vector3 fromPos)
	{
		var teleporter = GameObject.FindObjectsOfType <TeleportYAxis> ();
		var returnList = new List<TeleportYAxis> ();
		for (int i = 0; i < teleporter.Length; i++)
		{
			var teleporterCol = teleporter [i].GetComponent<Collider> ();

			if (teleporterCol.bounds.max.y >= fromPos.y &&
				teleporterCol.bounds.min.y <= fromPos.y &&
				teleporter [i].canGoDown) {
				returnList.Add (teleporter [i]);
			}
		}
		return returnList;
	}
}
