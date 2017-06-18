using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : ItemBase {

	public float recoveryAmount;

	// Use this for initialization
	public void Start () {
		base.Start ();
	}

	public bool DrinkUp()
	{
		if (!isUsed) {
			isUsed = true;
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
		if (isUsed) {
			isUsed = false;
			return true;
		}
		return false;
	}
}
