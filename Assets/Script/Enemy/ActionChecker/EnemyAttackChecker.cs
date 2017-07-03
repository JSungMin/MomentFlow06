using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackChecker : ActionCheckerBase {
	public float attackDelay;
	public float attackTimer;

	RunState runState;

	public bool needChaseZ = false;

	#region implemented abstract members of ActionCheckerBase

	public override void TryAction ()
	{
		runState = enemyAction.bodyAnimator.GetBehaviour<RunState> ();

		if (IsSatisfied ()) {
			Shoot ();
		}
		else {
			Aim ();
		}
	}

	protected void Shoot ()
	{
		if (((Gun)enemyInfo.GetComponentInChildren<EquiptInfo> ().nowEquiptWeapon).ammo <= 0) {
			enemyInfo.GetComponentInChildren<EnemyShoulderAction> ().Reload ();
			enemyAction.shoulderAnimator.SetTrigger ("TriggerReload");
		} else {
			enemyAction.bodyAnimator.SetTrigger ("TriggerShot");
			enemyAction.shoulderAnimator.SetTrigger ("TriggerShot");

		}
	}

	protected void Aim ()
	{
		enemyAction.bodyAnimator.SetTrigger ("TriggerAim");
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
