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

	public static void SaveSpriteEnabled(string name, SpriteRenderer sr)
	{
		PlayerPrefs.SetString (GetSaveKey(name,typeof (SpriteRenderer).Name),sr.enabled.ToString());
	}

	public static void SaveEquiptInfo(string name, EquiptInfo eInfo)
	{
		for (int i = 0; i < eInfo.weapons.Count; i++)
		{
			SaveWeapon (name, eInfo.weapons [i], i);
		}

		for (int i = 0; i < eInfo.itemPocketList.Count; i++)
		{
			SaveItem (name, eInfo.itemPocketList[i], i);
		}
	}

	private static void SaveWeapon (string name, Weapon weapon, int equiptIndex)
	{
		PlayerPrefs.SetString (GetSaveKey(name,"WeaponID"),equiptIndex+":"+weapon.id);

		if (weapon.weaponType == WeaponType.Rifle)
		{
			var gun = ((Gun)weapon);
			PlayerPrefs.SetString (GetSaveKey (name,"GunType"),equiptIndex + ":" + ((int)gun.rifleType));
			PlayerPrefs.SetString (GetSaveKey (name,"GunAmmo"),equiptIndex + ":" + ((int)gun.ammo));
			PlayerPrefs.SetString (GetSaveKey (name,"GunMagazine"),equiptIndex + ":" + ((int)gun.magazine));
			//기제하지 않은 데이터는 Load 할 시 WeaponFactory에서 가져 올 것
		}
	}

	private static void SaveItem (string name, GameObject itemObj, int equiptIndex)
	{
		var item = itemObj.GetComponent<ItemBase> ();
		PlayerPrefs.SetString (GetSaveKey(name,item.itemId.ToString()), item.isConnected + ","+ item.isInteracted +","+item.isUsed);
	}
}
