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
    
	protected void Start ()
	{
		parentAnimator = transform.parent.GetComponent<Animator> ();
		shoulderAnimator = GetComponent<Animator> ();

		nowEquiptWeapon = GetComponentInParent<EquiptInfo> ().nowEquiptWeapon;
	}

	public void Shot ()
	{
		var usingBullet = ((Rifle)nowEquiptWeapon).usingBullet;
		var borrowedBullet = BulletPool.Instance.BorrowBullet (usingBullet);
		borrowedBullet.transform.position = shotPosition.position;
		borrowedBullet.GetComponent<Bullet>().originVelocity = (shotPosition.position - transform.position).normalized * borrowedBullet.maxSpeed;
	}
}
