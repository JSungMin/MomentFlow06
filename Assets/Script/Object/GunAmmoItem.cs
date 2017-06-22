using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAmmoItem : ItemBase {

	public int ammoAmount;
	public string usingBulletName;

	// Use this for initialization
	public void Start () {
		base.Start ();
	}

	public bool GetAmmo()
	{
		if (!isUsed)
		{
			isUsed = true;
			transform.parent = originParent;
			transform.localPosition = Vector3.zero;
			((Gun)owner.GetComponent<EquiptInfo> ().nowEquiptWeapon).magazine += ammoAmount;
			gameObject.SetActive (false);
			return true;
		}
		return false;
	}

	public bool Refill ()
	{
		if (isUsed) {
			isUsed = false;
			return true;
		}
		return false;
	}
}
