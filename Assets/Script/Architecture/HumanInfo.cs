using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
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
