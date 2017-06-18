using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoulder : MonoBehaviour {

	protected bool isShooting = false;
	protected bool isAimming = false;

	protected Animator parentAnimator;
	protected Animator shoulderAnimator;

	protected Weapon nowEquiptWeapon = null;

	public Transform shotPosition;

    protected void Start()
    {
        parentAnimator = transform.parent.GetComponent<Animator>();
        shoulderAnimator = GetComponent<Animator>();

        nowEquiptWeapon = GetComponentInParent<EquiptInfo>().nowEquiptWeapon;
    }

	public void Shot ()
	{
		if (((Gun)nowEquiptWeapon).ammo - 1 >= 0) {
			((Gun)nowEquiptWeapon).ammo -= 1;
			var usingBullet = ((Gun)nowEquiptWeapon).usingBullet;
			var borrowedBullet = BulletPool.Instance.BorrowBullet (usingBullet, this.gameObject);
			borrowedBullet.transform.position = shotPosition.position;
			borrowedBullet.GetComponent<Bullet> ().originVelocity = (shotPosition.position - transform.position).normalized * borrowedBullet.maxSpeed;
		}
	}

	public void Reload()
	{
		int emptyAmount = ((Gun)nowEquiptWeapon).maxAmmo - ((Gun)nowEquiptWeapon).ammo;

		if (((Gun)nowEquiptWeapon).magazine - emptyAmount >= 0)
		{
			((Gun)nowEquiptWeapon).ammo = ((Gun)nowEquiptWeapon).maxAmmo;
			((Gun)nowEquiptWeapon).magazine -= emptyAmount;
		}
		else 
		{
			((Gun)nowEquiptWeapon).ammo += ((Gun)nowEquiptWeapon).magazine;
			((Gun)nowEquiptWeapon).magazine = 0;
		}
	}
}
