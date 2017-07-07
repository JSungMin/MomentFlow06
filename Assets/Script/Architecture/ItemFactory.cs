using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System;
using UnityEngine;

public class ItemFactory : MonoBehaviour {
	private static ItemFactory instance;

	public static ItemFactory Instance {
		get {
			if (instance == null)
			{
				var newItemFactory = new GameObject ("ItemFactory");
				instance = newItemFactory.AddComponent<ItemFactory> ();
			}
			return instance;
		}
	}

	public List<Potion> potionList;
	public List<GunAmmoItem> gunAmmoItemList;
	public List<GunItem> gunItemList;
	public List<Key> keyItemList;

	// Use this for initialization
	void Awake () {
		instance = this;
		LoadItemsInfo ();
	}

	private void LoadItemsInfo ()
	{
		var doc = XmlManager.Instance.GetXmlDocument ("ItemXml");
		if (null == doc) {
			Debug.Log ("Doc is Null");
			return;
		}
		var potionNodes = doc.SelectNodes ("ItemSet/Potion");
		var keyNodes = doc.SelectNodes ("ItemSet/Key");
		var gunAmmoItemNodes = doc.SelectNodes ("ItemSet/GunAmmoItem");
		var gunItemNodes = doc.SelectNodes ("ItemSet/GunItem");

		InitPotionList (potionNodes);
		InitKeyList (keyNodes);
		InitGunAmmoItemList (gunAmmoItemNodes);
		InitGunItemList (gunItemNodes);
	}

	void InitPotionList (XmlNodeList potionNodes)
	{
		foreach (XmlNode node in potionNodes) {
			var potion = new Potion ();
			potion.itemId = int.Parse(node.SelectSingleNode ("Id").InnerText);
			potion.itemName = node.SelectSingleNode ("Name").InnerText;
			potion.sprite = Resources.Load<Sprite> ("ItemImage/" + node.SelectSingleNode ("SpriteName").InnerText);
			potion.hpHealAmount = float.Parse(node.SelectSingleNode ("HpHealAmount").InnerText);
			potion.mpHealAmount = float.Parse(node.SelectSingleNode ("MpHealAmount").InnerText);
			potionList.Add (potion);
		}
	}

	void InitKeyList (XmlNodeList keyNodes)
	{
		foreach (XmlNode node in keyNodes) {
			var key = new Key ();
			key.itemId = int.Parse(node.SelectSingleNode ("Id").InnerText);
			key.itemName = node.SelectSingleNode ("Name").InnerText;
			key.sprite = Resources.Load<Sprite> ("ItemImage/" + node.SelectSingleNode ("SpriteName").InnerText);
			keyItemList.Add (key);
		}
	}

	void InitGunAmmoItemList (XmlNodeList gunAmmoItemNodes)
	{
		foreach (XmlNode node in gunAmmoItemNodes) {
			var gunAmmoItem = new GunAmmoItem ();
			gunAmmoItem.itemId = int.Parse(node.SelectSingleNode ("Id").InnerText);
			gunAmmoItem.itemName = node.SelectSingleNode ("Name").InnerText;
			gunAmmoItem.sprite = Resources.Load<Sprite> ("ItemImage/" + node.SelectSingleNode ("SpriteName").InnerText + ".png");
			gunAmmoItem.usingBulletName = node.SelectSingleNode ("UsingBullet").InnerText;
			gunAmmoItem.ammoAmount = int.Parse(node.SelectSingleNode ("Amount").InnerText);
			gunAmmoItemList.Add (gunAmmoItem);
		}
	}


	void InitGunItemList (XmlNodeList gunItemNodes)
	{
		foreach (XmlNode node in gunItemNodes) {
			var gunItem = new GunItem ();
			gunItem.itemId = int.Parse(node.SelectSingleNode ("Id").InnerText);
			gunItem.gunId = int.Parse(node.SelectSingleNode ("WeaponId").InnerText);
			gunItem.itemInfo = new ItemInfoStruct (gunItem.itemType, gunItem.itemId);
			gunItem.sprite = Resources.Load<Sprite> ("ItemImage/" + node.SelectSingleNode ("SpriteName").InnerText + ".png");
			gunItemList.Add (gunItem);
		}
	}

	public GameObject MakePotion (int potionId, Vector3 spawnPosition)
	{
		Debug.Log ("MakePotion");
		GameObject newPotion = GameObject.Instantiate ((GameObject)Resources.Load ("Prefabs/Items/Potion"));
		newPotion.name = "NewPotion";
		newPotion.transform.position = spawnPosition;

		var potion = newPotion.GetComponent<Potion> ();
		potion = GetItemInfo<Potion> (potionId);

		return newPotion;
	}

	public T GetItemInfo<T> (int itemId) where T : ItemBase
	{
		var newItemInfo = new ItemBase ();
		if (typeof(T) == typeof(Potion)) 
		{
			newItemInfo = new Potion ();
			newItemInfo = GetPotionInfo (itemId);
		}
		else if(typeof (T) == typeof (GunAmmoItem))
		{
			newItemInfo = new GunAmmoItem ();
			newItemInfo = GetGunAmmoItemInfo (itemId);
		}
		else if (typeof (T) == typeof (GunItem))
		{
			newItemInfo = new GunItem ();
			newItemInfo = GetGunItemInfo (itemId);
		}
		else if (typeof (T) == typeof (Key))
		{
			newItemInfo = new Key ();
			newItemInfo = GetKeyInfo (itemId);
		}
		return (T)newItemInfo;
	}

	private Potion GetPotionInfo (int potionId)
	{
		var savedPotion = potionList [potionId];
		var newPotionInfo = new Potion ();
		newPotionInfo.itemId = savedPotion.itemId;
		newPotionInfo.itemName = savedPotion.itemName;
		newPotionInfo.sprite = savedPotion.sprite;
		newPotionInfo.hpHealAmount = savedPotion.hpHealAmount;
		newPotionInfo.mpHealAmount = savedPotion.mpHealAmount;

		return newPotionInfo;
	}

	private Key GetKeyInfo (int keyId)
	{
		var savedKey = keyItemList [keyId];
		var newKeyInfo = new Key ();
		newKeyInfo.itemId = savedKey.itemId;
		newKeyInfo.itemName = savedKey.itemName;
		newKeyInfo.sprite = savedKey.sprite;

		return newKeyInfo;
	}

	private GunAmmoItem GetGunAmmoItemInfo (int gunAmmoItemIndex)
	{
		var savedGunAmmoItem = gunAmmoItemList [gunAmmoItemIndex];
		var newGunAmmoItemInfo = new GunAmmoItem ();
		newGunAmmoItemInfo.itemId = savedGunAmmoItem.itemId;
		newGunAmmoItemInfo.itemName = savedGunAmmoItem.itemName;
		newGunAmmoItemInfo.sprite = savedGunAmmoItem.sprite;
		newGunAmmoItemInfo.ammoAmount = savedGunAmmoItem.ammoAmount;

		return newGunAmmoItemInfo;
	}

	private GunItem GetGunItemInfo (int gunItemIndex)
	{
		var savedGunItem = gunItemList [gunItemIndex];
		var newGunItemInfo = new GunItem ();
		newGunItemInfo.itemId = savedGunItem.itemId;
		newGunItemInfo.gunId = savedGunItem.gunId;
		newGunItemInfo.sprite = savedGunItem.sprite;

		newGunItemInfo.Start ();

		return newGunItemInfo;
	}
}
