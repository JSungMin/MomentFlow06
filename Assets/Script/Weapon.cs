using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Weapon{
	//id와 weaponType 이용해 다형성으로 구성되는 EquiptInfo의 weapons에 추가 할 무기를 정한다.
	//무기를 정하고 추가하는 과정은 WeaponFactory에서 진행된다.
	public int id; 
	public string name;
	public WeaponType weaponType;

	public int damage;
	public float attackDelay;
	public float attackDelayTimer = 0;
}
