using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotState : StateMachineBehaviour {
	public Transform shotPosition;
	public Weapon nowEquiptWeapon;

	void Shot (Animator animator)
	{
		var usingBullet = ((Rifle)nowEquiptWeapon).usingBullet;
		var borrowedBullet = BulletPool.Instance.BorrowBullet (usingBullet);
		borrowedBullet.transform.position = shotPosition.position;
		borrowedBullet.GetComponent<Rigidbody> ().velocity = (shotPosition.position - animator.transform.position).normalized * borrowedBullet.maxSpeed;
	}


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (null == nowEquiptWeapon)
        {
            nowEquiptWeapon = animator.GetComponentInParent<EquiptInfo>().nowEquiptWeapon;
        }
        if (nowEquiptWeapon.weaponType == WeaponType.Rifle)
        {
            if (shotPosition == null)
                shotPosition = animator.transform.GetChild(0);
            Shot(animator);
        }
    }

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

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
