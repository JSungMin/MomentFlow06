﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDownStair : StateMachineBehaviour {

	private DynamicObject dynamicObject;

	private Collider stairCol = null;
	private Collider playerCol = null;

	private Rigidbody pRigid;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		pRigid = animator.GetComponentInParent<Rigidbody> ();
		playerCol = animator.GetComponent<Collider> ();
		dynamicObject = animator.GetComponentInParent <DynamicObject> ();
		if (null != PlayerAction.nearestStair) {
			stairCol = PlayerAction.nearestStair.GetComponent<Collider> ();
		}
	}

	void DownStair (Animator animator, AnimatorStateInfo stateInfo)
	{
		if (playerCol.bounds.min.y <= stairCol.bounds.min.y)
		{
			animator.SetBool ("IsOnStair",false);
			return;
		}

		var moveSpeed = animator.GetFloat ("MoveSpeed");
		var newVelocity = Vector3.zero;

		newVelocity = Vector3.up * animator.GetFloat ("VerticalInput") * moveSpeed * Time.unscaledDeltaTime;

		animator.GetComponentInParent<Rigidbody>().transform.position += newVelocity * dynamicObject.customTimeScale;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		DownStair (animator, stateInfo);
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
