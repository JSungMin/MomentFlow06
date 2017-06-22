using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquiptInfo : MonoBehaviour {
	public List<Weapon> weapons;
	public Weapon nowEquiptWeapon;
	public int nowEquiptWeaponIndex;

	public List<ItemInfoStruct> itemInfoList = new List<ItemInfoStruct>();

	public int defaultEquiptWeaponId;

	public void Awake()
	{
		var savedWeapon = WeaponFactory.Instance.GetWeapon<Weapon> (defaultEquiptWeaponId);
		weapons [0] = savedWeapon;
		weapons [1] = new Gun ();
		weapons [2] = new Gun ();
		EquiptWeapon (0);
	}

	public void AddWeapon (Weapon newWeapon)
	{
		weapons.Add (newWeapon);
	}

	public void AddWeapon (int weaponId)
	{
		var savedWeapon = WeaponFactory.Instance.GetWeapon<Weapon> (weaponId);
		weapons.Add(savedWeapon);
	}

	public void DeleteWeapon (Weapon weapon)
	{
		weapons.Remove (weapon);
	}
	public void DeleteWeapon (int id)
	{
		for (int i = 0; i < weapons.Count; i++)
		{
			if (weapons[i].id == id)
			{
				weapons.RemoveAt (i);
			}
		}
	}

	public void EquiptNextIndexWeapon ()
	{
		EquiptWeapon (nowEquiptWeaponIndex + 1);
	}

	public void EquiptPrevIndexWeapon ()
	{
		EquiptWeapon (nowEquiptWeaponIndex - 1);
	}

	public void EquiptWeapon (int index)
	{
		if (index < weapons.Count && index >= 0) {
			nowEquiptWeapon = weapons [index];		
			nowEquiptWeaponIndex = index;
		}
		else if (index >= weapons.Count)
		{
			nowEquiptWeapon = weapons [0];
			nowEquiptWeaponIndex = 0;
		}
		else if (index < 0)
		{
			nowEquiptWeaponIndex = weapons.Count - 1;
		}
	}
}
