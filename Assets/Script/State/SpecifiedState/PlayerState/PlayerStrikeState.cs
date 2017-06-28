using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStrikeState : StateMachineBehaviour {

	private PlayerAction playerAction;
	private Rigidbody playerRigidbody;

	public float power;

	private Vector3 velocity;

    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		playerAction = animator.GetComponentInParent<PlayerAction> ();
		playerRigidbody = animator.GetComponentInParent<Rigidbody> ();
		var dir = -playerAction.transform.localScale.x * Vector3.right;
		playerRigidbody.AddForce (dir * power, ForceMode.Impulse);
		playerRigidbody.useGravity = false;
		Debug.Log ("Add to player");
	}

	// OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		playerRigidbody.velocity = Vector3.Lerp (playerRigidbody.velocity, Vector3.zero, stateInfo.normalizedTime);
	}

	// OnStateExit is called before OnStateExit is called on any state inside this state machine
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		Debug.Log ("State Exit");
		playerRigidbody.useGravity = true;
		playerAction.GetSkill<StrikeAttack> ().inProgressed = false;
	}

	// OnStateMove is called before OnStateMove is called on any state inside this state machine
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called before OnStateIK is called on any state inside this state machine
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
