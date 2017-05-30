using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IBodyAction : StateMachineBehaviour {		
	static void SetStepMode (Animator animator)
	{
		if (animator.GetFloat ("HorizontalInput") != 0)
			animator.SetFloat ("IsForwardWalk", Mathf.Sign (animator.GetFloat ("HorizontalInput") * animator.transform.parent.localScale.x));
		else {
			animator.ResetTrigger ("TriggerWalk");
		}
	}

	static void Walk (Animator animator, AnimatorStateInfo stateInfo)
	{
		if (stateInfo.IsName ("Walk"))
		{
			SetStepMode (animator);

			var accel = animator.GetFloat ("MoveAccel");
			var newVelocity = animator.GetComponentInParent<Rigidbody> ().velocity;

			newVelocity += Vector3.right * animator.GetFloat ("HorizontalInput") * accel * Time.deltaTime;

			if (animator.GetBool("IsAimming"))
			{
				newVelocity.x = Mathf.Clamp (newVelocity.x, -animator.GetFloat("MoveSpeed"), animator.GetFloat("MoveSpeed")) * 0.8f;
			}
			else
				newVelocity.x = Mathf.Clamp (newVelocity.x, -animator.GetFloat("MoveSpeed"), animator.GetFloat("MoveSpeed"));

			animator.GetComponentInParent<Rigidbody> ().velocity = newVelocity;
		}
	}

	static void Run (Animator animator, AnimatorStateInfo stateInfo)
	{
		if (stateInfo.IsName ("Run"))
		{
			if (animator.GetFloat ("HorizontalInput") == 0) {
				animator.ResetTrigger ("TriggerRun");
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
			var rigid = animator.GetComponentInParent<Rigidbody> ();
			rigid.velocity = new Vector3 (rigid.velocity.x, 0, 0);
			rigid.AddForce (Vector3.up * 10 * Time.fixedDeltaTime, ForceMode.Impulse);
		}
	}
		
	static void Falling (Animator animator, AnimatorStateInfo stateInfo)
	{
		if (stateInfo.IsName ("Falling"))
		{
			
		}
	}

	static void Landing (Animator animator, AnimatorStateInfo stateInfo)
	{
		if (stateInfo.IsName ("Landing"))
		{
			
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
