using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDragMoveState : StateMachineBehaviour {
	DynamicObject dynamicObject;
	GameObject draggedObject;
	EnemyInfo draggedInfo;
	PlayerInfo playerInfo;

	void Walk (Animator animator, AnimatorStateInfo stateInfo)
	{

		var accel = animator.GetFloat ("MoveAccel");
		var newVelocity = animator.GetComponentInParent<Rigidbody> ().velocity;

		newVelocity += Vector3.right * animator.GetFloat ("HorizontalInput") * accel * Time.unscaledDeltaTime;

		newVelocity.x = Mathf.Clamp (newVelocity.x, -animator.GetFloat ("MoveSpeed"), animator.GetFloat ("MoveSpeed")) * 0.9f;

		//animator.GetComponentInParent<PlayerInfo>().transform.localScale = new Vector3 (-Mathf.Sign (animator.GetFloat ("HorizontalInput")), 1, 1);
		animator.GetComponentInParent<Rigidbody> ().velocity = newVelocity * dynamicObject.customTimeScale;
		if (playerInfo.transform.localScale.x > 0) {
			draggedObject.transform.position = new Vector3 (
				playerInfo.transform.position.x - draggedInfo.bodyCollider.bounds.extents.x,
				playerInfo.transform.position.y,
				playerInfo.transform.position.z
			);
		} else {
			draggedObject.transform.position = new Vector3 (
				playerInfo.transform.position.x + draggedInfo.bodyCollider.bounds.extents.x,
				playerInfo.transform.position.y,
				playerInfo.transform.position.z
			);
		}
		draggedObject.transform.localScale = playerInfo.transform.localScale;
	}

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		dynamicObject = animator.GetComponentInParent <DynamicObject> ();
		draggedObject = animator.GetBehaviour<PlayerDragState> ().draggedObject;
		playerInfo = animator.GetComponentInParent<PlayerInfo> ();
		draggedInfo = draggedObject.GetComponent<EnemyInfo> ();
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
