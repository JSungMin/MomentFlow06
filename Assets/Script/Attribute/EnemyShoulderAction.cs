using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoulderAction : Shoulder {

	// Use this for initialization
	void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	void Update () {
		if (parentAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Shot")) 
		{
			isShooting = true;
			shoulderAnimator.SetTrigger ("TriggerShot");
		}
		else
		{
			isShooting = false;
			shoulderAnimator.ResetTrigger ("TriggerShot");
		}

		if (parentAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle")) 
		{
			shoulderAnimator.SetTrigger ("TriggerIdle");
		}

		if (parentAnimator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
		{
			shoulderAnimator.SetTrigger ("TriggerWalk");
		}

		if (parentAnimator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
		{
			shoulderAnimator.SetTrigger ("TriggerRun");
		}
		shoulderAnimator.SetBool ("IsShooting", isShooting);
	}
}
