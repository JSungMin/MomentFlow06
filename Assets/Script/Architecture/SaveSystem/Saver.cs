using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saver {
	public static string GetSaveKey (string name, string type)
	{
		return SavePoint.sceneName + ":" + name + ":" + type;
	}

	public static void SaveTransform (string name, Transform t)
	{
		PlayerPrefs.SetString (GetSaveKey(name,typeof(Transform).Name), t.position.x + "," + t.position.y + "," + t.position.z + "," +
			t.localRotation.x + "," + t.localRotation.y + "," + t.localRotation.z + "," +
			t.localScale.x + "," + t.localScale.y + "," + t.localScale.z);
	}

	public static void SaveInt (string name, string typeTag, int value)
	{
		PlayerPrefs.SetInt (GetSaveKey (name, typeTag), value);
	}

	public static void SaveFloat (string name, string typeTag, float value)
	{
		PlayerPrefs.SetFloat (GetSaveKey (name, typeTag), value);
	}

	public static void SaveBool (string name, string typeTag, bool value)
	{
		PlayerPrefs.SetString (GetSaveKey (name, typeTag), value.ToString());
	}

	public static void SaveSpriteEnabled(string name, SpriteRenderer sr)
	{
		PlayerPrefs.SetString (GetSaveKey(name,typeof (SpriteRenderer).Name),sr.enabled.ToString());
	}

	public static void SaveEquiptInfo(string name, EquiptInfo eInfo)
	{
		SaveNowEquiptWeaponIndex (name, eInfo.nowEquiptWeaponIndex);

		for (int i = 0; i < eInfo.weapons.Count; i++)
		{
			SaveWeapon (name, eInfo.weapons [i], i);
		}

		SaveWeaponCount (name, eInfo.weapons.Count);

		for (int i = 0; i < eInfo.itemInfoList.Count; i++)
		{
			SaveItemInfo (name, eInfo.itemInfoList[i], i);
		}

		SaveItemCount (name, eInfo.itemInfoList.Count);
	}

	private static void SaveNowEquiptWeaponIndex(string name, int index)
	{
		SaveInt (name, "NowEquiptWeaponIndex", index);
	}

	private static void SaveWeapon (string name, Weapon weapon, int equiptIndex)
	{
		if (weapon == null)
			return;
		if (weapon.name != null) {
			SaveInt (name, "WeaponID : " + equiptIndex, weapon.id);
		}
		else {
			SaveInt (name, "WeaponID : " + equiptIndex, -1);
		}
		if (weapon.weaponType == WeaponType.Rifle)
		{
			var gun = ((Gun)weapon);
			SaveInt (name,"GunType : " + equiptIndex, ((int)gun.rifleType));
			SaveInt (name,"GunAmmo : " +equiptIndex,((int)gun.ammo));
			SaveInt (name,"GunMagazine : " + equiptIndex, ((int)gun.magazine));
			//기제하지 않은 데이터는 Load 할 시 WeaponFactory에서 가져 올 것
		}
	}

	private static void SaveWeaponCount (string name, int count)
	{
		SaveInt (name, "WeaponCount", count);
	}

	private static void SaveItemInfo (string name, ItemInfoStruct itemInfo, int equiptIndex)
	{
		SaveInt (name, "ItemType : " + equiptIndex, (int)itemInfo.itemType);
		SaveInt (name, "ItemId : " + equiptIndex, itemInfo.itemId);
	}

	private static void SaveItemCount (string name, int count)
	{
		SaveInt (name, "ItemCount", count);
	}
}
