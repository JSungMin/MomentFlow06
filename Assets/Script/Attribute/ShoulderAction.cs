using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoulderAction : MonoBehaviour {

	bool isShooting = false;
	bool isAimming = false;

	private Animator parentAnimator;
	private Animator shoulderAnimator;

	private Weapon nowEquiptWeapon = null;

	public Transform shotPosition;
    
	void Start ()
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
		borrowedBullet.GetComponent<Rigidbody> ().velocity = (shotPosition.position - transform.position).normalized * borrowedBullet.maxSpeed;
	}

    void Update()
    {
        if (!parentAnimator.GetBool("HoldOnWeapon"))
        {
            return;
        }

        isShooting = Input.GetMouseButton(0);
        isAimming = Input.GetMouseButton(1);

        parentAnimator.SetBool("IsShooting", isShooting);
        shoulderAnimator.SetBool("IsShooting", isShooting);

        parentAnimator.SetBool("IsAimming", isAimming);
        shoulderAnimator.SetBool("IsAimming", isAimming);

        if (isShooting)
        {
            parentAnimator.SetTrigger("TriggerShot");
            shoulderAnimator.SetTrigger("TriggerShot");
        }
    }
}
