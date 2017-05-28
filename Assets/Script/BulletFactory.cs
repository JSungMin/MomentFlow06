using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFactory : MonoBehaviour {
	private static BulletFactory instance;

	public static BulletFactory Instance {
		get {
			if (instance == null)
			{
				var newWeaponFactory = new GameObject ("BulletFactory");
				instance = newWeaponFactory.AddComponent<BulletFactory> ();
			}
			return instance;
		}
	}

	public Transform bulletPoolParent;
	public List<Transform> bulletPools;

	public void Awake()
	{
		instance = this;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
