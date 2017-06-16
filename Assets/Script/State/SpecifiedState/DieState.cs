using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : IStateBehaviour
{

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        shoulderSpriteRenderer.enabled = false;
        enemyInfo.boxCollider.center = new Vector3(enemyInfo.boxCollider.center.x, 0.0f, 0.0f);
        enemyInfo.boxCollider.size = new Vector3(enemyInfo.boxCollider.size.x, 0.1f, 0.0f);
        animator.transform.GetChild(0).GetComponent<Animator>().enabled = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.enabled = false;
    }
}
