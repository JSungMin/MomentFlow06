using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HumanInfo : MonoBehaviour {

	public float hp;
	//PlayerInfo에 있는 MP를 HumanInfo단으로 끌어 올렸음 => Enemy 중 시간 능력 사용하는 녀석들이 존재
	public Mana mana { protected set; get; }
	public int maxPocketCapacity;
	public int nowPocketCapacity;

	public Collider bodyCollider;
	public Collider triggerCollider;

	public bool isCrouched;

	public bool CanStoreItem()
	{
		if (nowPocketCapacity < maxPocketCapacity)
			return true;
		return false;
	}
}
