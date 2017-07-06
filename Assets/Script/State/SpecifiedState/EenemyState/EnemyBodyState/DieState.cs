using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : IStateBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
		if (null == enemyInfo)
		{
			enemyInfo = animator.GetComponentInParent <EnemyInfo> ();
		}
		enemyInfo.isDead = true;
		animator.GetComponentInChildren<EnemyShoulderAction>().HideArm();

		enemyInfo.hp = 0;
		enemyInfo.boxCollider.center = new Vector3(enemyInfo.boxCollider.center.x, 0.0f, 0.0f);
        enemyInfo.boxCollider.size = new Vector3(enemyInfo.boxCollider.size.x, 0.1f, 0.0f);
        
        enemyInfo.isUpdatable = false;
		enemyInfo.hp = 0;
    }
    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
		animator.GetComponent<SpriteRenderer>().sortingLayerName = "Enemy";
		animator.GetComponent<SpriteRenderer> ().sortingOrder = 0;
    }
}
