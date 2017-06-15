using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractableObject, ILockable {

	private Animator doorAnimator;

	// Use this for initialization
	void Start () {
		doorAnimator = GetComponent<Animator> ();
		cancelInteractActions += CloseDoor;
	}

	public void OnCollisionStay(Collision col)
	{
		if (col.gameObject.CompareTag("Player") && !isInteracted)
		{
			Debug.Log ("Force to player");
			var power = (col.transform.position - transform.position).normalized;
			if (Mathf.Sign (power.x) > 0)
				power.x = 1 - power.x;
			else
				power.x = -(power.x + 1);
			power.y = 0;
			power.z = 0;
			col.transform.position += power * 1.2f * Time.deltaTime;
		}
	}

	private void OpenDoorToRight ()
	{
		isInteracted = true;
		doorAnimator.SetTrigger ("TriggerOpenToRight");
	}

	private void OpenDoorToLeft()
	{
		isInteracted = true;
		doorAnimator.SetTrigger ("TriggerOpenToLeft");
	}

	private void CloseDoor()
	{
		isInteracted = false;
		doorAnimator.SetTrigger ("TriggerClose");
	}

	public override bool TryInteract (GameObject challenger)
	{
		IsLocked = TryToReleaseLock (ref challenger.GetComponent<EquiptInfo>().itemPocketList);

		if (!IsLocked) {
			Debug.Log ("C : " + challenger.transform.position.x + "  D : " + transform.position.x);
			if (challenger.transform.position.x > transform.position.x) {
				doInteractActions = new DoInteractActions(OpenDoorToLeft);
			}
			else {
				doInteractActions = new DoInteractActions(OpenDoorToRight);
			}
			doInteractActions.Invoke ();
			return true;
		}

		return false;
	}

	public bool TryToReleaseLock(ref List<GameObject> pocketItems)
	{
		if (null == KeyObject)
			return false;

		foreach(var item in pocketItems)
		{
			if (item == KeyObject || null == KeyObject)
			{
				pocketItems.Remove (item);
				return false;
			}
		}
		return true;
	}

	public bool isLocked;
	public GameObject keyObject;

	public bool IsLocked {
		get {
			return isLocked;
		}
		set {
			isLocked = value;
		}
	}
	public GameObject KeyObject {
		get {
			return keyObject;
		}
		set {
			keyObject = value;
		}
	}
}
