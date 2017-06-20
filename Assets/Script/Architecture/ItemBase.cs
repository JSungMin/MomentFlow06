using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : InteractableObject
{
	public int itemId;
	public GameObject owner;

	protected Transform originParent;
	protected Collider itemCollider;
	public Rigidbody itemRigidbody;
	public SpriteRenderer itemRenderer;

	public bool isUsed = false;

	// Use this for initialization
	protected void Start () {
		originParent = transform.parent;
		itemCollider = GetComponent<Collider> ();
		itemRigidbody = GetComponent<Rigidbody> ();
		itemRenderer = GetComponent<SpriteRenderer> ();

		doInteractActions = new DoInteractActions (PickUpItem);
		cancelInteractActions = new CancelInteractActions (DropItem);
	}

	//Potion의 TryInteract는 떨어져있는 Potion 또는 드랍된 포션을 줍는 과정이다.
	//isDrinked와 혼동되지 않게 주의 할 것
	public override bool TryInteract (GameObject challenger)
	{
		if (!isInteracted && !isConnected)
		{
			if (challenger.GetComponent<HumanInfo> ().CanStoreItem ()) 
			{
				Debug.Log ("Try To Pick UP");
				owner = challenger;
				doInteractActions.Invoke ();
				return true;
			} 
			else {
				owner = challenger;
				cancelInteractActions.Invoke ();
			}
		}
		return false;
	}

	public void PickUpItem()
	{
		isInteracted = true;
		transform.parent = owner.transform;
		var pocket = owner.GetComponentInParent<EquiptInfo> ().itemPocketList;
		pocket.Add (this.gameObject);

		itemRenderer.enabled = false;
		itemCollider.isTrigger = true;
		itemRigidbody.isKinematic = true;
	}
		
	public void DropItem()
	{
		isInteracted = false;
		transform.parent = originParent;
		var pocket = owner.GetComponent<EquiptInfo> ().itemPocketList;
		pocket.Remove (this.gameObject);

		itemRenderer.enabled = true;
		itemCollider.isTrigger = false;
		itemRigidbody.isKinematic = false;
	}
}
