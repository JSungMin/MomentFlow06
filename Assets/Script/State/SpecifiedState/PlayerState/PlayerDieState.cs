using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDieState : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
		animator.GetComponentInChildren<PlayerShoulderAction>().HideArm();
        animator.transform.GetChild(0).GetComponent<Animator>().enabled = false;

    }
    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
		animator.GetComponentInParent<PlayerAction> ().enabled = false;
        animator.enabled = false;
    }
}
