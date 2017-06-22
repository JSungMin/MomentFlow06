using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : InteractableObject
{
	public ItemType itemType;
	public int itemId;
	public string itemName;

	public GameObject owner;
	public Sprite sprite;

	protected Transform originParent;
	protected Collider itemCollider;
	public Rigidbody itemRigidbody;
	public SpriteRenderer itemRenderer;

	public bool isUsed = false;
	[HideInInspector]
	public ItemInfoStruct itemInfo;

	// Use this for initialization
	protected void Start () {
		originParent = transform.parent;
		itemCollider = GetComponent<Collider> ();
		itemRigidbody = GetComponent<Rigidbody> ();
		itemRenderer = GetComponent<SpriteRenderer> ();

		if (null != sprite)
			itemRenderer.sprite = sprite;

		doInteractActions = new DoInteractActions (PickUpItem);
		cancelInteractActions = new CancelInteractActions (DropItem);
	}

	public void SetOriginParent(Transform parent)
	{
		originParent = parent;
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
		var pocket = owner.GetComponentInParent<EquiptInfo> ().itemInfoList;
		pocket.Add (itemInfo);

		DestroyObject (this.gameObject);
	}
		
	public void DropItem()
	{
		var pocket = owner.GetComponent<EquiptInfo> ().itemInfoList;
		pocket.Remove (itemInfo);

		if (itemRigidbody.isKinematic == true) {
			itemRigidbody.isKinematic = false;
			itemRenderer.enabled = true;
			return;
		}

		var item = ItemFactory.Instance.MakePotion (itemId, owner.transform.position + Vector3.up * 0.3f);
		DestroyObject (this.gameObject);
	}
}
