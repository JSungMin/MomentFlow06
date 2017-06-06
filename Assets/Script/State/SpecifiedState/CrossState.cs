using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossState : StateMachineBehaviour {
	public AnimationCurve heightCurve;

	private Rigidbody pRigid;
	private Collider obstacle;
	private Vector3 direction = Vector3.zero;

	private float originY;

	private float obstacleHeight = 0;

	private float distance;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		pRigid = animator.GetComponentInParent<Rigidbody> ();
		originY = pRigid.transform.position.y;
		obstacle = animator.GetComponentInParent<OutsideInfo> ().GetNearestObstacleObject ().GetComponent<Collider>();
		obstacleHeight = obstacle.bounds.size.y;
		direction = new Vector3(obstacle.transform.position.x - animator.transform.position.x,0).normalized;

		pRigid.transform.localScale = new Vector3 (-direction.x * Mathf.Abs(pRigid.transform.localScale.x),pRigid.transform.localScale.y);

		animator.GetComponentInChildren<PlayerShoulderAction> ().HideArm ();
		pRigid.isKinematic = true;

		distance = Vector3.Distance (obstacle.bounds.center, animator.transform.position) * 2;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		pRigid.transform.Translate (direction * (distance / 0.66f) * Time.deltaTime);
		pRigid.transform.position = new Vector3 (
			pRigid.transform.position.x,
			originY + heightCurve.Evaluate(stateInfo.normalizedTime) * obstacleHeight,
			pRigid.transform.position.z);
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		animator.GetComponentInChildren<PlayerShoulderAction> ().ActiveArm ();
		pRigid.isKinematic = false;

		animator.ResetTrigger ("TriggerCross");
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
