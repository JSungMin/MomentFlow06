using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunItem : ItemBase {
	public int gunId;
	public Gun rifle;

	// Use this for initialization
	public void Start () {
		base.Start ();
		doInteractActions = new DoInteractActions (PickUpItem);
		rifle = ((Gun)(WeaponFactory.Instance.GetWeapon<Weapon> (gunId)));
	}

	public void PickUpItem()
	{
		isInteracted = true;
		transform.parent = owner.transform;

		var pocket = owner.GetComponent<EquiptInfo> ();

		var pocketRifle01 = ((Gun)pocket.weapons [0]);
		var pocketRifle02 = ((Gun)pocket.weapons [1]);
		var pocketPistol = ((Gun)pocket.weapons [2]);

		switch (rifle.rifleType) {
		case GunType.Pistol:
			if (null == pocketPistol.usingBullet) {
				pocket.weapons [2] = rifle;
				break;
			}

			if (pocketPistol.id != rifle.id) {
				var prevPistol = WeaponFactory.Instance.MakeNewGunItem (((Gun)pocketPistol), transform);
				prevPistol.DropItem ();
				pocket.weapons[2] = rifle;
			} else{
				((Gun)pocket.weapons [2]).magazine += rifle.ammo + rifle.magazine;
				rifle.ammo = 0;
				rifle.magazine = 0;
				Debug.Log ("Pistol : " + ((Gun)pocket.weapons [2]).magazine);
			}
			break;
		default :
			var nowEquipt = ((Gun)pocket.weapons [pocket.nowEquiptWeaponIndex]);

			if (nowEquipt.id != rifle.id) {

				if (null == pocketRifle01.usingBullet) {
					pocket.weapons [0] = rifle;
					break;
				} else if (null == pocketRifle02.usingBullet) {
					pocket.weapons [1] = rifle;
					break;
				}

				var prevPistol = WeaponFactory.Instance.MakeNewGunItem (((Gun)nowEquipt), owner.transform);
				prevPistol.DropItem ();
				pocket.weapons[pocket.nowEquiptWeaponIndex] = rifle;
			} else {
				nowEquipt.magazine += rifle.ammo + rifle.magazine;
				rifle.ammo = 0;
				rifle.magazine = 0;
				Debug.Log ("Now Equipt : " + nowEquipt.magazine);
			}
			break;
		}
			
		pocket.EquiptWeapon (pocket.nowEquiptWeaponIndex);

		itemRenderer.enabled = false;
		itemCollider.isTrigger = true;
		itemRigidbody.isKinematic = true;
	}

	public void DropItem()
	{
		isInteracted = false;
		transform.parent = originParent;
		var pocket = owner.GetComponent<EquiptInfo> ();

		itemRenderer.enabled = true;
		itemCollider.isTrigger = false;
		itemRigidbody.isKinematic = false;
	}
}
