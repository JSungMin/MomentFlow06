using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunStayState : IStateBehaviour {

	public float stunDuration = 1.5f;
	private float stunTimer;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (null == enemyInfo)
		{
			enemyInfo = animator.GetComponentInParent <EnemyInfo> ();
		}
		dynamicObject = enemyInfo.GetComponent<DynamicObject> ();

	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		stunTimer += dynamicObject.customDeltaTime;

		if (stunTimer >= stunDuration)
		{
			animator.SetTrigger ("TriggerStunEnd");
		}
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		stunTimer = 0;
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
