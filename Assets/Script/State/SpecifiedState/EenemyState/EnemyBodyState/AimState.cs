using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimState : IStateBehaviour
{
    private AimTarget aimTarget;
    private Vector3 targetPos;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (null == aimTarget)
        {
            aimTarget = animator.GetComponentInParent<AimTarget>();
        }

        animator.GetComponentInParent<EnemyInfo>().AttackDelayTimer = 0;

        Collider targetCollider = enemyInfo.attackTarget.GetComponent<Collider>();
        if (targetCollider == null)
            targetCollider = enemyInfo.attackTarget.GetComponentInChildren<Collider>();

		targetPos = targetCollider.bounds.center + Vector3.up * Random.Range (0,targetCollider.bounds.extents.y);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (TimeManager.GetInstance().IsTimePaused())
            return;

        aimTarget.AimToObject(targetPos);
        animator.GetComponentInParent<EnemyInfo>().AttackDelayTimer += TimeManager.GetInstance().customDeltaTime;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponentInParent<EnemyInfo>().AttackDelayTimer = 0;
    }
}