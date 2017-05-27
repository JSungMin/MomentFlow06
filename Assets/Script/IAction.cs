using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAction : StateMachineBehaviour {

	static void Walk (Animator animator, AnimatorStateInfo stateInfo)
	{
		if (stateInfo.IsName ("Walk"))
		{
			var aimScr = animator.GetComponentInParent<AimTarget> ();
			if (null != aimScr) {
				aimScr.isActive = false;
			}

			if (animator.GetFloat ("HorizontalInput") != 0)
				animator.SetFloat ("IsForwardWalk", Mathf.Sign (animator.GetFloat ("HorizontalInput") * animator.transform.parent.localScale.x));
			else {
				animator.ResetTrigger ("TriggerWalk");
				//animator.Play ("Idle");
			}
		
			var accel = animator.GetFloat ("MoveAccel");
			var newVelocity = animator.GetComponentInParent<Rigidbody> ().velocity;
			newVelocity += Vector3.right * animator.GetFloat ("HorizontalInput") * accel * Time.deltaTime;
			newVelocity.x = Mathf.Clamp (newVelocity.x, -animator.GetFloat("MoveSpeed"), animator.GetFloat("MoveSpeed"));
			animator.GetComponentInParent<Rigidbody> ().velocity = newVelocity;
		}
	}
	static void Run (Animator animator, AnimatorStateInfo stateInfo)
	{
		if (stateInfo.IsName ("Run"))
		{
			var aimScr = animator.GetComponentInParent<AimTarget> ();
			if (null != aimScr) {
				aimScr.isActive = false;
			}

			if (animator.GetFloat ("HorizontalInput") == 0) {
				animator.ResetTrigger ("TriggerRun");
				//animator.Play ("Idle");
			}

			var accel = animator.GetFloat ("MoveAccel");

			var newVelocity = animator.GetComponentInParent<Rigidbody> ().velocity;
			newVelocity += Vector3.right * animator.GetFloat ("HorizontalInput") * accel * Time.deltaTime;
			newVelocity.x = Mathf.Clamp (newVelocity.x, -animator.GetFloat("MaxMoveSpeed"), animator.GetFloat("MaxMoveSpeed"));
			animator.GetComponentInParent<Rigidbody> ().velocity = newVelocity;
		}
	}
	static void Jump (Animator animator, AnimatorStateInfo stateInfo)
	{
		if (stateInfo.IsName ("Jump"))
		{
			Debug.Log ("Jump");
			animator.GetComponentInParent<Rigidbody> ().AddForce (Vector3.up * 25 * Time.deltaTime, ForceMode.Impulse);
		}
	}
		
	static void Falling (Animator animator, AnimatorStateInfo stateInfo)
	{
		if (stateInfo.IsName ("Falling"))
		{
			Debug.Log ("Falling");
		}
	}

	static void Landing (Animator animator, AnimatorStateInfo stateInfo)
	{
		if (stateInfo.IsName ("Landing"))
		{
			Debug.Log ("Landing");
		}
	}

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		Jump (animator, stateInfo);
		Falling (animator, stateInfo);
		Landing (animator, stateInfo);
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		Walk (animator, stateInfo);
		Run (animator, stateInfo);
	}

	//OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		var aimScr = animator.GetComponentInParent<AimTarget> ();
		if (null != aimScr) {
			aimScr.isActive = true;
		}
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
