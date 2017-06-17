using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : InteractableObject {
	public GameObject owner;

	private Transform originParent;
	private Collider potionCollider;
	public Rigidbody potionRigidbody;

	public float recoveryAmount;
	private bool isDrinked = false;

	// Use this for initialization
	void Start () {
		doInteractActions = new DoInteractActions (PickUp);
		cancelInteractActions = new CancelInteractActions (Drop);
		originParent = transform.parent;
		potionCollider = GetComponent<Collider> ();
		potionRigidbody = GetComponent<Rigidbody> ();
	}

	public bool DrinkUp()
	{
		if (!isDrinked) {
			isDrinked = true;
			transform.parent = originParent;
			transform.localPosition = Vector3.zero;
			owner.GetComponent<HumanInfo> ().hp += recoveryAmount;
			gameObject.SetActive (false);
			return true;
		}
		return false;
	}

	public bool Refill()
	{
		if (isDrinked) {
			isDrinked = false;
			return true;
		}
		return false;
	}

	//Potion의 TryInteract는 떨어져있는 Potion 또는 드랍된 포션을 줍는 과정이다.
	//isDrinked와 혼동되지 않게 주의 할 것
	public override bool TryInteract (GameObject challenger)
	{
		if (!isInteracted)
		{
			if (challenger.GetComponent<HumanInfo> ().CanStoreItem ()) {
				owner = challenger;
				doInteractActions.Invoke ();
				return true;
			} else {
				owner = challenger;
				cancelInteractActions.Invoke ();
			}
		}
		return false;
	}

	public void PickUp()
	{
		isInteracted = true;
		transform.parent = owner.transform;
		var pocket = owner.GetComponent<EquiptInfo> ().itemPocketList;
		pocket.Add (this.gameObject);

		potionCollider.isTrigger = true;
		potionRigidbody.isKinematic = true;
	}

	public void Drop()
	{
		isInteracted = false;
		transform.parent = originParent;
		var pocket = owner.GetComponent<EquiptInfo> ().itemPocketList;
		pocket.Remove (this.gameObject);

		potionCollider.isTrigger = false;
		potionRigidbody.isKinematic = false;
	}
}
