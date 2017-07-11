using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleActionChecker : ActionCheckerBase {

	public int index = 0;
	public string[] idleBodyTriggerArray;
	public string[] idleHandTriggerArray;

	public void SetIdleAnimationIndex (int newIndex)
	{
		index = newIndex;
	}

	#region implemented abstract members of ActionCheckerBase

	protected override bool IsSatisfied ()
	{
		return true;
	}

	protected override void DoAction ()
	{
		enemyAction.targetZList.Clear ();
		enemyAction.targetZListOffet = 0;
		enemyAction.bodyAnimator.SetTrigger (idleBodyTriggerArray[index]);
		enemyAction.shoulderAnimator.SetTrigger (idleHandTriggerArray[index]);
	}
	public override void CancelAction()
	{

	}
	#endregion
}
