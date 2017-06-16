using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : InteractableObject, ILockable {

	private Animator chestAnimator;
	private GameObject challenger;

	// Use this for initialization
	void Start () {
		chestAnimator = GetComponent<Animator> ();
		doInteractActions = new DoInteractActions(OpenChest);
		cancelInteractActions = new CancelInteractActions (CloseChest);
	}

	private void OpenChest()
	{
		isInteracted = true;
		chestAnimator.SetTrigger ("TriggerOpen");
		for (int i = 0; i < willBeAffectedObjectList.Count; i++)
		{
			willBeAffectedObjectList [i].GetComponentInParent<InteractableObject> ().TryInteract (challenger);
		}
	}

	private void CloseChest()
	{
		isInteracted = false;
		chestAnimator.SetTrigger ("TriggerClose");
	}

	public override bool TryInteract (GameObject challenger)
	{
		IsLocked = TryToReleaseLock (ref challenger.GetComponent<EquiptInfo>().itemPocketList);

		if (!IsLocked){
			this.challenger = challenger;
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
