using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Gun : Weapon {
	public GunType rifleType;
	public int maxAmmo;
	public int ammo;
	public int magazine;
	public GameObject usingBullet;
}
