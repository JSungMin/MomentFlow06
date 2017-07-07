using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : InteractableObject, ILockable, ITimeRecallableForInteractable {

	private Animator doorAnimator;
    private AudioSource audioSource;

	void Start () {
		doorAnimator = GetComponent<Animator> ();
		cancelInteractActions += CloseDoor;
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
            audioSource.playOnAwake = false;
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
        //audioSource.Play();
    }

	private void OpenDoorToLeft()
	{
		isInteracted = true;
		doorAnimator.SetTrigger ("TriggerOpenToLeft");
        //audioSource.Play();
    }

	private void CloseDoor()
	{
		isInteracted = false;
		doorAnimator.SetTrigger ("TriggerClose");
	}

	public override bool TryInteract (GameObject challenger)
	{
		if (IsLocked)
			IsLocked = TryToReleaseLock (ref challenger.GetComponent<EquiptInfo>().itemInfoList);

		if (!IsLocked) {
			if (!isInteracted) {
				Debug.Log ("C : " + challenger.transform.position.x + "  D : " + transform.position.x);
				if (challenger.transform.position.x > transform.position.x) {
					doInteractActions = new DoInteractActions (OpenDoorToLeft);
				} else {
					doInteractActions = new DoInteractActions (OpenDoorToRight);
				}
				doInteractActions.Invoke ();
				return true;
			}
			else {
				cancelInteractActions.Invoke ();
			}
		}

		return false;
	}

	public bool TryToReleaseLock(ref List<ItemInfoStruct> pocketItems)
	{
		if (null == KeyObject && isLocked)
			return true;

		foreach(var item in pocketItems)
		{
			if (item.itemType == KeyObject.itemType && item.itemId == KeyObject.itemId)
			{
				pocketItems.Remove (item);
				return false;
			}
		}
		return true;
	}

	public bool isLocked;
	public ItemBase keyObject;

	public bool IsLocked {
		get {
			return isLocked;
		}
		set {
			isLocked = value;
		}
	}
	public ItemBase KeyObject {
		get {
			return keyObject;
		}
		set {
			keyObject = value;
		}
	}

	public void BreakLock ()
	{
		isLocked = false;
	}

	#region ITimeRecallableForInteractable implementation

	public bool TryRecall (GameObject challenger)
	{
		if (IsSatisfied (challenger)) {
			DoRecall (challenger);
			return true;
		}
		return false;
	}

	public bool IsSatisfied (GameObject challenger)
	{
		var mana = challenger.GetComponentInParent<HumanInfo> ().mana;
		Debug.Log (mana.ManaPoint);
		if (IsChangeable && mana.ManaPoint - ConsumeAmount >= 0) {
			mana.AddMana (-ConsumeAmount);
			return true;
		}
		return false;
	}

	public void DoRecall (GameObject challenger)
	{
		if (isLocked) {
			Debug.Log ("Lock Release");
			isLocked = false;
			TryInteract (challenger);
			return;
		} 
		else {
			Debug.Log ("Lock");
			TryInteract (challenger);
			isLocked = true;
		}
	}

	[SerializeField]
	private float consumeAmount;
	[SerializeField]
	private bool isChangeable;

	public float ConsumeAmount {
		get {
			return consumeAmount;
		}
		set {
			consumeAmount = value;
		}
	}

	public bool IsChangeable {
		get {
			return isChangeable;
		}
		set {
			isChangeable = value;
		}
	}
		
	#endregion
}
