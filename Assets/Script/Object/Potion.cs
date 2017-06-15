using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : InteractableObject {
	public GameObject owner;

	private Transform originParent;
	private Collider potionCollider;
	public Rigidbody potionRigidbody;

	// Use this for initialization
	void Start () {
		doInteractActions = new DoInteractActions (PickUp);
		cancelInteractActions = new CancelInteractActions (Drop);
		originParent = transform.parent;
		potionCollider = GetComponent<Collider> ();
		potionRigidbody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override bool TryInteract (GameObject challenger)
	{
		if (!isInteracted)
		{
			owner = challenger;
			doInteractActions.Invoke ();
			return true;
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
