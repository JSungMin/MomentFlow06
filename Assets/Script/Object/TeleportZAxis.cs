using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class TeleportZAxis : InteractableObject {
	public LayerMask interactMask;
	public bool autoInteract;
	public bool autoCancel;

	public float fromZ;
	public float toZ;

	public UnityEvent interactEvent;

	private GameObject challenger;

	public List<TeleportZAxis> interactSyncObject;

	void Start() {
		doInteractActions += DoTeleport;
		cancelInteractActions += CancelTeleport;
	}

	public void OnTriggerExit (Collider col)
	{
		if ((1 << col.gameObject.layer & interactMask) == 0)
		{
			return;
		}
		if (autoInteract && !isInteracted) {
			TryInteract (col.GetComponentInParent<HumanInfo>().gameObject);
		}
		if (autoCancel){
			CancelTeleport (col.GetComponentInParent<HumanInfo> ().gameObject);
		}
	}

	#region implemented abstract members of InteractableObject
	public override bool TryInteract (GameObject challenger)
	{
		this.challenger = challenger;

		if (challenger.transform.position.z == fromZ) {
			doInteractActions.Invoke ();
		} else {
			cancelInteractActions.Invoke ();
		}
		for (int i = 0; i < interactSyncObject.Count; i++)
		{
			interactSyncObject [i].isInteracted = isInteracted;
		}
		return true;
	}
	#endregion

	public void DoTeleport ()
	{
		challenger.transform.position = new Vector3 (
			challenger.transform.position.x,
			challenger.transform.position.y,
			toZ);
		isInteracted = true;
		interactEvent.Invoke ();
	}

	public void CancelTeleport ()
	{
		challenger.transform.position = new Vector3 (
			challenger.transform.position.x,
			challenger.transform.position.y,
			fromZ);
		isInteracted = false;
	}

	public void CancelTeleport (GameObject challenger)
	{
		challenger.transform.position = new Vector3 (
			challenger.transform.position.x,
			challenger.transform.position.y,
			fromZ);
		isInteracted = false;
	}

	public static List<TeleportZAxis> FindZUpTeleporters (float fromZ)
	{
		var teleporter = GameObject.FindObjectsOfType <TeleportZAxis> ();
		var returnList = new List<TeleportZAxis> ();
		for (int i = 0; i < teleporter.Length; i++)
		{
			if (teleporter [i].fromZ == fromZ && teleporter [i].toZ > teleporter [i].fromZ)
			{
				returnList.Add (teleporter [i]);
			}
			if (teleporter [i].toZ == fromZ && teleporter [i].toZ < teleporter [i].fromZ)
			{
				returnList.Add (teleporter [i]);
			}
		}
		return returnList;
	}

	public static List<TeleportZAxis> FindZUpTeleporters (float fromZ, float toZ)
	{
		var teleporter = GameObject.FindObjectsOfType <TeleportZAxis> ();
		var returnList = new List<TeleportZAxis> ();
		for (int i = 0; i < teleporter.Length; i++)
		{
			if (teleporter [i].fromZ == fromZ &&
				teleporter [i].toZ == toZ &&
				teleporter [i].toZ > teleporter [i].fromZ)
			{
				returnList.Add (teleporter [i]);
			}
			if (teleporter [i].toZ == fromZ &&
				teleporter [i].fromZ == toZ &&
				teleporter [i].toZ < teleporter [i].fromZ)
			{
				returnList.Add (teleporter [i]);
			}
		}
		return returnList;
	}

	public static List<TeleportZAxis> FindZDownTeleporters (float fromZ)
	{
		var teleporter = GameObject.FindObjectsOfType <TeleportZAxis> ();
		var returnList = new List<TeleportZAxis> ();
		for (int i = 0; i < teleporter.Length; i++)
		{
			if (teleporter [i].fromZ == fromZ && teleporter [i].toZ < teleporter [i].fromZ)
			{
				returnList.Add (teleporter [i]);
			}
			if (teleporter [i].toZ == fromZ && teleporter [i].toZ > teleporter [i].fromZ)
			{
				returnList.Add (teleporter [i]);
			}
		}
		return returnList;
	}

	public static List<TeleportZAxis> FindZDownTeleporters (float fromZ, float toZ)
	{
		var teleporter = GameObject.FindObjectsOfType <TeleportZAxis> ();
		var returnList = new List<TeleportZAxis> ();
		for (int i = 0; i < teleporter.Length; i++)
		{
			if (teleporter [i].fromZ == fromZ &&
				teleporter [i].toZ == toZ &&
				teleporter [i].toZ < teleporter [i].fromZ)
			{
				returnList.Add (teleporter [i]);
			}
			if (teleporter [i].toZ == fromZ &&
				teleporter [i].fromZ == toZ &&
				teleporter [i].toZ > teleporter [i].fromZ)
			{
				returnList.Add (teleporter [i]);
			}
		}
		Debug.Log ("From : " + fromZ + "  To : " + toZ + "  Count : " + returnList.Count);
		return returnList;
	}

	//빚이다 빚
	public void ToNextScene ()
	{
		SceneManager.LoadScene (1);
	}
}
