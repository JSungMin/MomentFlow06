using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDragStartState : StateMachineBehaviour {
	PlayerInfo playerInfo;
	EnemyInfo draggedInfo;
	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		playerInfo = animator.GetComponentInParent<PlayerInfo> ();
		playerInfo.GetComponentInChildren<PlayerAction> ().isReverseLook = true;

		draggedInfo = animator.GetBehaviour <PlayerDragState> ().draggedInfo;
		draggedInfo.enemyAction.bodyAnimator.SetTrigger ("TriggerDrag");

		if (playerInfo.transform.localScale.x > 0) {
			draggedInfo.transform.position = new Vector3 (
				playerInfo.transform.position.x - draggedInfo.bodyCollider.bounds.extents.x,
				playerInfo.transform.position.y,
				playerInfo.transform.position.z
			);
		} else {
			draggedInfo.transform.position = new Vector3 (
				playerInfo.transform.position.x + draggedInfo.bodyCollider.bounds.extents.x,
				playerInfo.transform.position.y,
				playerInfo.transform.position.z
			);
		}
		draggedInfo.transform.localScale = playerInfo.transform.localScale;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		//playerInfo.GetComponentInChildren<PlayerAction> ().isReverseLook = false;
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
