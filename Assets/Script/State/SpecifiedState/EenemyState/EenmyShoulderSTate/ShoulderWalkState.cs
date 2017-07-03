using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoulderWalkState : IStateBehaviour {
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		enemyInfo = animator.GetComponentInParent<EnemyInfo> ();
		animator.GetComponentInParent<AimTarget> ().AimToForward ();
	}

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		
	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
	}
}
