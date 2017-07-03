using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackChecker : ActionCheckerBase {
	public float attackDelay;
	public float attackTimer;

	#region implemented abstract members of ActionCheckerBase

	public override void TryAction ()
	{
		Debug.Log ("Attack");
		if (IsSatisfied ()) {
			Shoot ();
		}
		else {
			Aim ();
		}
	}

	protected void Shoot ()
	{
		enemyAction.bodyAnimator.SetTrigger ("TriggerShot");
		enemyAction.shoulderAnimator.SetTrigger ("TriggerShot");
	}

	protected void Aim ()
	{
		enemyAction.bodyAnimator.SetTrigger ("TriggerIdle");
		enemyAction.shoulderAnimator.SetTrigger ("TriggerAim");
	}

	protected override bool IsSatisfied ()
	{
		return (attackTimer >= attackDelay) ? true : false;
	}

	protected override void DoAction ()
	{
		Debug.Log ("암것도 없다.");
	}

	#endregion


}
