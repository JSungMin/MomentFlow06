using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquiptInfo : MonoBehaviour {
	public List<Weapon> weapons;
	public Weapon nowEquiptWeapon;

	public List<GameObject> itemPocketList;

	public int defaultEquiptWeaponId;

	public void Awake()
	{
		AddWeapon (defaultEquiptWeaponId);
		EquiptWeapon (0);
	}

	public void AddWeapon (Weapon newWeapon)
	{
		weapons.Add (newWeapon);
	}

	public void AddWeapon (int weaponId)
	{
		weapons.Add(WeaponFactory.Instance.GetWeapon<Weapon>(weaponId));
	}

	public void DeleteWeapon (Weapon weapon)
	{
		weapons.Remove (weapon);
	}
	public void DeleteWeapon (int index)
	{
		if (index < weapons.Count && index >= 0)
			weapons.RemoveAt (index);
	}

	public void EquiptWeapon (int index)
	{
		nowEquiptWeapon = weapons [index];		
	}
}
