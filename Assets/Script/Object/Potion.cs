using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : ItemBase {

	public float hpHealAmount;
	public float mpHealAmount;
	// Use this for initialization
	public void Start () {
		base.Start ();
	}

	public bool DrinkUp(HumanInfo human)
	{
		if (!isUsed) {
			isUsed = true;
			human.hp += hpHealAmount;
			//owner.GetComponent<Mana> ().ManaPoint += mpHealAmount;
			human.GetComponent<EquiptInfo>().itemInfoList.Remove(itemInfo);
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
