using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectState : IStateBehaviour {
	public DynamicObject dynamicObject;
	private float detectTimer = 0;
	public float detectDuration = 1.5f; 
	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		detectTimer = 0;
		enemyInfo = animator.GetComponentInParent<EnemyInfo> ();
		dynamicObject = animator.GetComponentInParent<DynamicObject> ();
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (detectTimer >= detectDuration) {
			animator.SetTrigger ("TriggerDetectEnd");
			enemyInfo.enemyAction.shoulderAnimator.SetTrigger ("TriggerDetectEnd");
		} else {
			detectTimer += dynamicObject.customDeltaTime;
		}
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		detectTimer = 0;
	}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
