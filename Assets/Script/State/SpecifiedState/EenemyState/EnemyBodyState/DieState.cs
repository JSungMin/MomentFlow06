using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : IStateBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponentInChildren<EnemyShoulderAction>().HideArm();
		enemyInfo.hp = 0;
		enemyInfo.boxCollider.center = new Vector3(enemyInfo.boxCollider.center.x, 0.0f, 0.0f);
        enemyInfo.boxCollider.size = new Vector3(enemyInfo.boxCollider.size.x, 0.1f, 0.0f);
        animator.transform.GetChild(0).GetComponent<Animator>().enabled = false;
        enemyInfo.isUpdatable = false;
    }
    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.enabled = false;
    }
}
