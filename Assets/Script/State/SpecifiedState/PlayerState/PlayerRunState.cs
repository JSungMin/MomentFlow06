﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : StateMachineBehaviour {

	DynamicObject dynamicObject;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		dynamicObject = animator.GetComponentInParent <DynamicObject> ();
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (animator.GetFloat ("HorizontalInput") == 0) {
			animator.ResetTrigger ("TriggerRun");
		}

		var accel = animator.GetFloat ("MoveAccel");

		var newVelocity = animator.GetComponentInParent<Rigidbody> ().velocity;
		newVelocity += Vector3.right * animator.GetFloat ("HorizontalInput") * accel * Time.unscaledDeltaTime;
		newVelocity.x = Mathf.Clamp (newVelocity.x, -animator.GetFloat("MaxMoveSpeed"), animator.GetFloat("MaxMoveSpeed"));

		animator.GetComponentInParent<Rigidbody> ().velocity = newVelocity * dynamicObject.customTimeScale;
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
