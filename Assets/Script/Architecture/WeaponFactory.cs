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
				weapon = new Gun ();
				SetGeneralWeaponInfo (weapon, node);
				((Gun)weapon).rifleType = (GunType)int.Parse (node.SelectSingleNode("RifleType").InnerText);
				((Gun)weapon).maxAmmo = int.Parse(node.SelectSingleNode ("MaxAmmo").InnerText);
				((Gun)weapon).ammo = ((Gun)weapon).maxAmmo;
				((Gun)weapon).magazine = int.Parse(node.SelectSingleNode ("Magazine").InnerText);
				((Gun)weapon).usingBullet = Resources.Load ("Prefabs/Bullets/" + node.SelectSingleNode ("UsingBullet").InnerText) as GameObject;
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
			newWeapon = new Gun ();
			newWeapon.id = savedWeapon.id;
			newWeapon.name = savedWeapon.name;
			newWeapon.weaponType = savedWeapon.weaponType;
			newWeapon.damage = savedWeapon.damage;
			newWeapon.attackDelay = savedWeapon.damage;
			((Gun)newWeapon).maxAmmo = ((Gun)savedWeapon).maxAmmo;
			((Gun)newWeapon).rifleType = ((Gun)savedWeapon).rifleType;
			((Gun)newWeapon).ammo = ((Gun)savedWeapon).ammo;
			((Gun)newWeapon).magazine = ((Gun)savedWeapon).magazine;
			((Gun)newWeapon).rifleType = ((Gun)savedWeapon).rifleType;
			((Gun)newWeapon).usingBullet = ((Gun)savedWeapon).usingBullet;
		}
		return (T)(newWeapon);
	}

	public GunItem MakeNewGunItem (Gun rifle, Transform madeFromObject)
	{
		GameObject newItem = GameObject.Instantiate ((GameObject)Resources.Load("Prefabs/Items/GunItem"));
		var item = newItem.GetComponent<GunItem> ();
		item.gunId = rifle.id;
		newItem.name = "PrevGun";
		newItem.transform.position = madeFromObject.transform.position + Vector3.up * 0.3f;
		item.owner = madeFromObject.gameObject;
		item.Start ();
		return item;
	}
}
