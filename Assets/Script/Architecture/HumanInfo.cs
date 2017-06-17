using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanInfo : MonoBehaviour {

	public float hp;
	public int maxPocketCapacity;
	public int nowPocketCapacity;

	public bool CanStoreItem()
	{
		if (nowPocketCapacity < maxPocketCapacity)
			return true;
		return false;
	}
}
