using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReleaseGrabState : StateMachineBehaviour {
	public EnemyAction grabbedEnemy;
	private EnemyInfo grabbedEnemyInfo;

	private PlayerInfo playerInfo;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		playerInfo = animator.GetComponentInParent<PlayerInfo> ();
		grabbedEnemyInfo = grabbedEnemy.GetComponentInParent<EnemyInfo> ();
		grabbedEnemy.bodyAnimator.SetTrigger ("TriggerGrabRelease");
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (playerInfo.transform.localScale.x > 0) {
			grabbedEnemyInfo.transform.position = playerInfo.transform.position + Vector3.left * 0.15f;
		} else {
			grabbedEnemyInfo.transform.position = playerInfo.transform.position + Vector3.right * 0.15f;
		}
		grabbedEnemyInfo.transform.localScale = playerInfo.transform.localScale;
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		
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
