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
		if (((Rifle)nowEquiptWeapon).ammo - 1 >= 0) {
			((Rifle)nowEquiptWeapon).ammo -= 1;
			var usingBullet = ((Rifle)nowEquiptWeapon).usingBullet;
			var borrowedBullet = BulletPool.Instance.BorrowBullet (usingBullet, this.gameObject);
			borrowedBullet.transform.position = shotPosition.position;
			borrowedBullet.GetComponent<Bullet> ().originVelocity = (shotPosition.position - transform.position).normalized * borrowedBullet.maxSpeed;
		}
	}

	public void Reload()
	{
		int emptyAmount = ((Rifle)nowEquiptWeapon).maxAmmo - ((Rifle)nowEquiptWeapon).ammo;
		Debug.Log ("Empty : " + emptyAmount + " Magazine : " + ((Rifle)nowEquiptWeapon).magazine);
		if (((Rifle)nowEquiptWeapon).magazine - emptyAmount >= 0)
		{
			((Rifle)nowEquiptWeapon).ammo = ((Rifle)nowEquiptWeapon).maxAmmo;
			((Rifle)nowEquiptWeapon).magazine -= emptyAmount;
		}
		else 
		{
			((Rifle)nowEquiptWeapon).ammo += ((Rifle)nowEquiptWeapon).magazine;
			((Rifle)nowEquiptWeapon).magazine = 0;
		}
	}
}
