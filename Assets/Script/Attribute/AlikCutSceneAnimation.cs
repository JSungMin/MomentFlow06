using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlikCutSceneAnimation : MonoBehaviour {
	public GameObject playerAction;
	public Animator bodyAnimator;
	public Animator shoulderAnimator;

	public void AlikStartCutSceneAnimation ()
	{
		playerAction.SetActive (false);
		playerAction.GetComponentInParent<AimTarget> ().enabled = false;
		playerAction.GetComponentInParent<Rigidbody> ().isKinematic = true;
		bodyAnimator.gameObject.SetActive (true);
	}

	public void AlikEndCutSceneAnimation ()
	{
		playerAction.SetActive (true);
		playerAction.GetComponentInParent<AimTarget> ().enabled = true;
		playerAction.GetComponentInParent<Rigidbody> ().isKinematic = false;
		bodyAnimator.gameObject.SetActive (false);
	}

	public void AlikSetHoldOnWeapon (bool holdValue)
	{
		if (holdValue) {
			bodyAnimator.SetBool ("HoldOnWeapon", true);
			shoulderAnimator.enabled = true;
			shoulderAnimator.GetComponent<SpriteRenderer> ().enabled = true;
		}
		else {
			bodyAnimator.SetBool ("HoldOnWeapon", false);
			shoulderAnimator.enabled = false;
			shoulderAnimator.GetComponent<SpriteRenderer> ().enabled = false;
		}
	}

	public void AlikSetDirection (bool isLeft)
	{
		if (isLeft) {
			bodyAnimator.transform.parent.localScale = new Vector3 (1, 1, 1);
		} else {
			bodyAnimator.transform.parent.localScale = new Vector3 (-1, 1, 1);
		}
	}

	public void AlikIdle ()
	{
		bodyAnimator.Play ("Idle");
		shoulderAnimator.Play ("Walk");
	}

	public void AlikWalk ()
	{
		bodyAnimator.SetTrigger ("TriggerWalk");
		shoulderAnimator.SetTrigger ("TriggerWalk");
	}

	public void AlikShoot ()
	{
		bodyAnimator.Play ("Shoot");
	}
}
