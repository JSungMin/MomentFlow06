using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStairState : StateMachineBehaviour {
	private Collider stairCol = null;
	private Collider playerCol = null;

	private Rigidbody pRigid;
    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		animator.GetComponentInParent<Rigidbody> ().isKinematic = true;


		pRigid = animator.GetComponentInParent<Rigidbody> ();
		playerCol = animator.GetComponent<Collider> ();

		if (null != PlayerAction.nearestStair) {
			stairCol = PlayerAction.nearestStair.GetComponent<Collider> ();
		}
	}
		
	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (playerCol.GetComponentInParent<OutsideInfo>().stairList.Count == 0)
		{
			animator.ResetTrigger ("TriggerStair");
			animator.SetBool ("IsOnStair", false);
		}
	}

	// OnStateExit is called before OnStateExit is called on any state inside this state machine
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		animator.GetComponentInParent<Rigidbody> ().isKinematic = false;

		Debug.Log ("Exit");
	}

	// OnStateMove is called before OnStateMove is called on any state inside this state machine
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called before OnStateIK is called on any state inside this state machine
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateMachineEnter is called when entering a statemachine via its Entry Node
	//override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash){
	//
	//}

	// OnStateMachineExit is called when exiting a statemachine via its Exit Node
	//override public void OnStateMachineExit(Animator animator, int stateMachinePathHash) {
	//
	//}
}
