using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleActionChecker : ActionCheckerBase {

	#region implemented abstract members of ActionCheckerBase

	protected override bool IsSatisfied ()
	{
		return true;
	}

	protected override void DoAction ()
	{
		enemyAction.bodyAnimator.SetTrigger ("TriggerIdle");
		enemyAction.shoulderAnimator.SetTrigger ("TriggerIdle");
	}

	#endregion
}
