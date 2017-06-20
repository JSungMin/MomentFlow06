using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader {
	public static string GetSaveKey (string name, string type)
	{
		return SavePoint.sceneName + ":" + name + ":" + type;
	}

	public static Transform LoadTransform (string name, Transform targetTrans)
	{
		var data = PlayerPrefs.GetString (GetSaveKey (name, typeof(Transform).Name));
		string[] sData = data.Split ("," [0]);

		targetTrans.position = new Vector3 (float.Parse(sData[0]),float.Parse(sData[1]),float.Parse(sData[2]));
		targetTrans.localRotation = Quaternion.Euler (new Vector3 (float.Parse(sData[3]),float.Parse(sData[4]),float.Parse(sData[5])));
		targetTrans.localScale = new Vector3 (float.Parse(sData[6]),float.Parse(sData[7]),float.Parse(sData[8]));
		return targetTrans;
	}

	public static int LoadInt (string name, string typeTag)
	{
		return PlayerPrefs.GetInt(GetSaveKey(name,typeTag));
	}

	public static float LoadFloat (string name, string typeTag)
	{
		return PlayerPrefs.GetFloat(GetSaveKey(name,typeTag));
	}

	public static bool LoadSpriteEnabled(string name)
	{
		var data = PlayerPrefs.GetString (GetSaveKey (name,typeof(SpriteRenderer).Name));
		if (bool.TrueString  == data) {
			return true;
		}
		return false;
	}

	public static EquiptInfo LoadEquiptInfo (string name, EquiptInfo equiptInfo)
	{
		int weaponCount = LoadWeaponCount (name);
		int itemCount = LoadItemCount (name);


		equiptInfo.weapons.RemoveRange (0, weaponCount);
		equiptInfo.itemPocketList.RemoveRange (0, itemCount);


		for (int i = 0; i < weaponCount; i++)
		{
			equiptInfo.weapons.Add (null);
			equiptInfo.weapons[i] = LoadWeapon (name, i);
		}

		for (int i = 0; i < itemCount; i++)
		{
			equiptInfo.itemPocketList.Add (null);
			LoadItem (name, i, equiptInfo.itemPocketList[i]);
		}

		return equiptInfo;
	}

	private static int LoadNowEquiptWeaponIndex (string name)
	{
		return LoadInt (name, "NowEquiptWeaponIndex");
	}

	private static Weapon LoadWeapon (string name, int equiptIndex)
	{
		var loadedWeapon = new Weapon ();
		var weaponId = LoadInt (name, "WeaponID : " + equiptIndex);
		if (weaponId == -1) {
			return null;
		}
		loadedWeapon = WeaponFactory.Instance.GetWeapon<Weapon> (weaponId);

		if (loadedWeapon.weaponType == WeaponType.Rifle)
		{
			((Gun)loadedWeapon).rifleType = ((GunType)LoadInt (name, "GunType : " + equiptIndex));
			((Gun)loadedWeapon).ammo = LoadInt (name, "GunAmmo : " + equiptIndex);
			((Gun)loadedWeapon).magazine = LoadInt (name, "GunMagazine : " + equiptIndex);
		}
		return loadedWeapon;
	}

	private static int LoadWeaponCount (string name)
	{
		return LoadInt (name, "WeaponCount");
	}

	private static GameObject LoadItem (string name, int equiptIndex, GameObject item)
	{
		var itemBase = item.GetComponent<ItemBase> ();
		var data = PlayerPrefs.GetString (GetSaveKey(name,itemBase.itemId.ToString()));
		var splitedDatas = data.Split ("," [0]);

		bool.TryParse(splitedDatas [0],out itemBase.isConnected);
		bool.TryParse(splitedDatas [1],out itemBase.isInteracted);
		bool.TryParse(splitedDatas [2],out itemBase.isUsed);

		return item;
	}

	private static int LoadItemCount (string name)
	{
		return LoadInt (name, "ItemCount");
	}
}
