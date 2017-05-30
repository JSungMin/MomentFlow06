using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour {
	private static BulletPool instance;

	public static BulletPool Instance {
		get {
			if (instance == null)
			{
				var newWeaponFactory = new GameObject ("BulletFactory");
				instance = newWeaponFactory.AddComponent<BulletPool> ();
			}
			return instance;
		}
	}

	public Transform bulletPoolParent;
	public List<Transform> bulletPools;
	public List<int> bulletPoolIndexList;

	public void Awake()
	{
		instance = this;
	}
		
	public void InitBullets()
	{
		var rifles = FindRifleWeapons ();
		Debug.Log (rifles.Count);
		for (int i = 0; i < rifles.Count; i++) {
			var rifle = rifles [i];
			GameObject pool = GameObject.Find (rifle.usingBullet.name + "Pool");

			if (null != pool)
			{
				AddAmmoToPool (rifle, pool.transform);
				return;
			}
			// Can't Find Bullet Pool
			GameObject newPool = new GameObject (rifle.usingBullet.name + "Pool");
			newPool.transform.SetParent (bulletPoolParent);
			newPool.transform.position = Vector3.zero;
			bulletPools.Add (newPool.transform);

			AddAmmoToPool (rifle, newPool.transform);
		}
	}

	public void AddAmmoToPool (Rifle rifle, Transform pool)
	{
		for (int i = 0; i < rifle.maxAmmo; i++)
		{
			var newBullet = Instantiate (rifle.usingBullet, pool) as GameObject;
			newBullet.transform.position = Vector3.zero;
			newBullet.SetActive (false);
		}
	}

	public Bullet BorrowBullet (GameObject usingBullet)
	{
		for (int i = 0; i < bulletPools.Count; i++)
		{
			if (bulletPools[i].name.Contains (usingBullet.name))
			{
				var pool = bulletPools [i];

			}
		}
		return null;
	}

	private List<Rifle> FindRifleWeapons()
	{
		var equiptments = GameObject.FindObjectsOfType<EquiptInfo> ();
		List<Rifle> rifles = new List<Rifle> ();
		foreach (EquiptInfo equiptment in equiptments) {
			foreach (Weapon weapon in equiptment.weapons)
			{
				if (weapon.weaponType == WeaponType.Rifle)
				{
					rifles.Add ((Rifle)weapon);
				}
			}
		}
		return rifles;
	}
}
