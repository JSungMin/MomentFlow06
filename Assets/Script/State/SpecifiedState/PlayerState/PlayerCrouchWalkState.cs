using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchWalkState : StateMachineBehaviour {

	private DynamicObject dynamicObject;

	static void SetStepDirection (Animator animator)
	{
		if (animator.GetFloat ("HorizontalInput") != 0)
			animator.SetFloat ("IsForwardWalk", Mathf.Sign (animator.GetFloat ("HorizontalInput") * animator.transform.parent.localScale.x));
		else {
			animator.ResetTrigger ("TriggerWalk");
		}
	}

	void Walk (Animator animator, AnimatorStateInfo stateInfo)
	{
		SetStepDirection (animator);

		var accel = animator.GetFloat ("MoveAccel");
		var newVelocity = animator.GetComponentInParent<Rigidbody> ().velocity;

		newVelocity += Vector3.right * animator.GetFloat ("HorizontalInput") * accel * Time.unscaledDeltaTime;

		if (animator.GetBool ("IsAimming")) {
			newVelocity.x = Mathf.Clamp (newVelocity.x, -animator.GetFloat ("MoveSpeed"), animator.GetFloat ("MoveSpeed")) * 0.6f;
		} else
			newVelocity.x = Mathf.Clamp (newVelocity.x, -animator.GetFloat ("MoveSpeed"), animator.GetFloat ("MoveSpeed")) * 0.9f;

		animator.GetComponentInParent<Rigidbody> ().velocity = newVelocity * dynamicObject.customTimeScale;
	}

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		dynamicObject = animator.GetComponentInParent <DynamicObject> ();
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		Walk (animator, stateInfo);
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
