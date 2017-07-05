using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDragState : StateMachineBehaviour {
	public bool isDrag;
	PlayerInfo playerInfo;
	public GameObject draggedObject;
	public EnemyInfo draggedInfo;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		isDrag = true;
		animator.SetBool ("IsCrouching", false);
		animator.ResetTrigger ("TriggerStandUp");

		playerInfo = animator.GetComponentInParent<PlayerInfo> ();

		draggedInfo = draggedObject.GetComponent<EnemyInfo> ();
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		isDrag = false;
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
