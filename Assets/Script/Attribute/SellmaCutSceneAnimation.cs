using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellmaCutSceneAnimation : MonoBehaviour {
	public GameObject playerAction;
	public Animator bodyAnimator;
	public Animator shoulderAnimator;

	public void SellmaStartCutSceneAnimation ()
	{
		playerAction.SetActive (false);
		playerAction.GetComponentInParent<AimTarget> ().enabled = false;
		playerAction.GetComponentInParent<Rigidbody> ().isKinematic = true;
		bodyAnimator.gameObject.SetActive (true);
	}

	public void SellmaEndCutSceneAnimation ()
	{
		playerAction.SetActive (true);
		playerAction.GetComponentInParent<AimTarget> ().enabled = true;
		playerAction.GetComponentInParent<Rigidbody> ().isKinematic = false;
		bodyAnimator.gameObject.SetActive (false);
	}

	public void SellmaSetHoldOnWeapon(bool holdValue)
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

	public void SellmaSetDirection (bool isLeft)
	{
		if (isLeft) {
			bodyAnimator.transform.parent.localScale = new Vector3 (1, 1, 1);
		} else {
			bodyAnimator.transform.parent.localScale = new Vector3 (-1, 1, 1);
		}
	}

	public void SellmaWalk ()
	{
		Debug.Log ("SellmaWalk");
		bodyAnimator.SetTrigger ("TriggerWalk");
		shoulderAnimator.SetTrigger ("TriggerWalk");
	}

	public void SellmaRun ()
	{
		bodyAnimator.Play ("Run");
		shoulderAnimator.Play ("Run");
	}

	public void SellmaCrouch ()
	{
		bodyAnimator.SetBool ("IsCrouch", true);
		bodyAnimator.SetTrigger ("TriggerCrouch");
	}

	public void SellmaStandUp()
	{
		bodyAnimator.SetTrigger ("TriggerStandUp");
	}

	public void SellmaShot ()
	{
		bodyAnimator.Play ("Shot");
		shoulderAnimator.Play ("Shot");
	}

	public void SellmaCross ()
	{
		bodyAnimator.Play ("Cross");
	}

	public void SellmaIdle ()
	{
		bodyAnimator.Play ("Idle");
		shoulderAnimator.Play ("Walk");
	}
}
