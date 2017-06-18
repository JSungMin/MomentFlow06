using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoulderAction : Shoulder {

	private AimTarget playerAimTarget;
    
	protected new void Start () {
		playerAimTarget = GetComponentInParent<AimTarget> ();
		base.Start ();
	}

	protected void Update()
	{
//		if (CutSceneManager.isCutScenePlaing)
//			return;
		
		if (!parentAnimator.GetBool("HoldOnWeapon"))
			return;

        isShooting = Input.GetMouseButton(0);
		isAimming = Input.GetMouseButton(1);

		if (((Gun)nowEquiptWeapon).ammo <= 0)
			isShooting = false;

		parentAnimator.SetBool("IsShooting", isShooting);
		shoulderAnimator.SetBool("IsShooting", isShooting);

		parentAnimator.SetBool("IsAimming", isAimming);
		shoulderAnimator.SetBool("IsAimming", isAimming);

		shoulderAnimator.SetBool ("IsOnStair", parentAnimator.GetBool("IsOnStair"));

		if (Input.GetKeyDown (KeyCode.R) ||
			(((Gun)nowEquiptWeapon).ammo <= 0 && ((Gun)nowEquiptWeapon).magazine != 0))
		{
			shoulderAnimator.SetTrigger ("TriggerReload");
		}

		if (TimeRecall.isInTimeRevertPhase) 
		{
			parentAnimator.ResetTrigger("TriggerShot");
			shoulderAnimator.ResetTrigger("TriggerShot");
			parentAnimator.SetBool ("IsShooting", false);
			shoulderAnimator.SetBool ("IsShooting", false);

			return;
		}

		if (isShooting)
        {
            parentAnimator.SetTrigger("TriggerShot");
			shoulderAnimator.SetTrigger("TriggerShot");
		}
	}

	public void HideArm()
	{
		playerAimTarget.hideShoulder = true;
		shoulderAnimator.enabled = false;
	}

	public void ActiveArm()
	{
		playerAimTarget.hideShoulder = false;
		shoulderAnimator.enabled = true;
	}
}
