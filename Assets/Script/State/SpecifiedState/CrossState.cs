using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossState : StateMachineBehaviour {
	private Rigidbody pRigid;


	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		pRigid = animator.GetComponentInParent<Rigidbody> ();
		animator.GetComponentInChildren<PlayerShoulderAction> ().HideArm ();
		pRigid.useGravity = false;

	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		pRigid.AddForce (-pRigid.transform.localScale.x * Vector3.right * 10, ForceMode.Force);
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		animator.GetComponentInChildren<PlayerShoulderAction> ().ActiveArm ();
		pRigid.useGravity = true;
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
