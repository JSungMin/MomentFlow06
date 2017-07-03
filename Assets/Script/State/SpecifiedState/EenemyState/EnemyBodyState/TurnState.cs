using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnState : IStateBehaviour {

	public bool isTurnLeft;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (null == dynamicObject)
		{
			dynamicObject = animator.GetComponentInParent<DynamicObject> ();
		}

		if (null == enemyInfo)
		{
			enemyInfo = animator.GetComponentInParent <EnemyInfo> ();
		}
		enemyInfo.GetComponent<AimTarget> ().AimToForward ();
	}

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{

	}


	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (isTurnLeft) {
			enemyInfo.transform.localScale = new Vector3 (1, 1, 1);
			animator.ResetTrigger ("TriggerTurnLeft");
		}
		else {
			enemyInfo.transform.localScale = new Vector3 (-1, 1, 1);
			animator.ResetTrigger ("TriggerTurnRight");
		}
	}
}
