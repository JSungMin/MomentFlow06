using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System;
using UnityEngine;

public class WeaponFactory : MonoBehaviour {
	private static WeaponFactory instance;

	public static WeaponFactory Instance {
		get {
			if (instance == null)
			{
				var newWeaponFactory = new GameObject ("WeaponFactory");
				instance = newWeaponFactory.AddComponent<WeaponFactory> ();
			}
			return instance;
		}
	}

	public List<Weapon> weaponList;

	public void Awake()
	{
		instance = this;
		LoadWeaponsInfo ();
	}

	private void SetGeneralWeaponInfo(Weapon weapon, XmlNode node)
	{
		weapon.weaponType = (WeaponType)Enum.Parse (typeof(WeaponType), node.SelectSingleNode ("WeaponType").InnerText);
		weapon.id = int.Parse(node.SelectSingleNode ("Id").InnerText);
		weapon.name = node.SelectSingleNode ("Name").InnerText;
		weapon.damage = int.Parse(node.SelectSingleNode ("Damage").InnerText);
		weapon.attackDelay = float.Parse(node.SelectSingleNode ("AttackDelay").InnerText);
	}

	private void LoadWeaponsInfo()
	{
		var doc = XmlManager.Instance.GetXmlDocument ("WeaponXml");
		if (null == doc) {
			Debug.LogError ("Doc is Null");
			return;
		}
		var nodes = doc.SelectNodes ("WeaponSet/Weapon");

		foreach (XmlNode node in nodes) {
			var weapon = new Weapon ();
			weapon.weaponType = (WeaponType)Enum.Parse (typeof(WeaponType), node.SelectSingleNode ("WeaponType").InnerText);

			if (weapon.weaponType == WeaponType.Rifle)
			{
				weapon = new Rifle ();
				SetGeneralWeaponInfo (weapon, node);
				((Rifle)weapon).maxAmmo = int.Parse(node.SelectSingleNode ("MaxAmmo").InnerText);
				((Rifle)weapon).ammo = ((Rifle)weapon).maxAmmo;
				((Rifle)weapon).magazine = int.Parse(node.SelectSingleNode ("Magazine").InnerText);
				((Rifle)weapon).usingBullet = Resources.Load ("Prefabs/Bullets/" + node.SelectSingleNode ("UsingBullet").InnerText) as GameObject;
			} 
			else if (weapon.weaponType == WeaponType.Sword)
			{
				weapon = new Sword ();
				SetGeneralWeaponInfo (weapon, node);
				//아직 Sword에 대한 정의 부족으로 추후 세부적인 데이터 추가 필요
			}
			weaponList.Add (weapon);
		}
	}

	public T GetWeapon<T>(int weaponId) where T : Weapon
	{
		var savedWeapon = weaponList [weaponId];
		var newWeapon = new Weapon ();
		if (savedWeapon.weaponType == WeaponType.Rifle)
		{
			newWeapon = new Rifle ();
			newWeapon.id = savedWeapon.id;
			newWeapon.name = savedWeapon.name;
			newWeapon.weaponType = savedWeapon.weaponType;
			newWeapon.damage = savedWeapon.damage;
			newWeapon.attackDelay = savedWeapon.damage;
			((Rifle)newWeapon).maxAmmo = ((Rifle)savedWeapon).maxAmmo;
			((Rifle)newWeapon).ammo = ((Rifle)savedWeapon).ammo;
			((Rifle)newWeapon).magazine = ((Rifle)savedWeapon).magazine;
			((Rifle)newWeapon).rifleType = ((Rifle)savedWeapon).rifleType;
			((Rifle)newWeapon).usingBullet = ((Rifle)savedWeapon).usingBullet;
		}
		return (T)(newWeapon);
	}
}
