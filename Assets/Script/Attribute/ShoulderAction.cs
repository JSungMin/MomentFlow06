using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoulderAction : MonoBehaviour {

	bool isShooting = false;
	bool isAimming = false;

	private Animator parentAnimator;
	private Animator shoulderAnimator;

	// Use this for initialization
	void Start ()
	{
		parentAnimator = transform.parent.GetComponent<Animator> ();
		shoulderAnimator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!parentAnimator.GetBool ("HoldOnWeapon")) {
			return;
		}

		isShooting = Input.GetMouseButton (0);
		isAimming = Input.GetMouseButton (1);

		parentAnimator.SetBool ("IsShooting", isShooting);
		shoulderAnimator.SetBool ("IsShooting", isShooting);

		parentAnimator.SetBool ("IsAimming", isAimming);
		shoulderAnimator.SetBool ("IsAimming", isAimming);

		if (isShooting)
		{
			parentAnimator.SetTrigger ("TriggerShot");
			shoulderAnimator.SetTrigger ("TriggerShot");
		}
	}
}
