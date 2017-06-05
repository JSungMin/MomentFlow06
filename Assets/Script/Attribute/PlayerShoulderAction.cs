using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoulderAction : Shoulder {

	private AimTarget playerAimTarget;

	// Use this for initialization
	protected void Start () {
		playerAimTarget = GetComponentInParent<AimTarget> ();
		base.Start ();
	}

	protected void Update()
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

	public void HideArm()
	{
		playerAimTarget.hideShoulder = true;
	}

	public void ActiveArm()
	{
		playerAimTarget.hideShoulder = false;
	}
}
