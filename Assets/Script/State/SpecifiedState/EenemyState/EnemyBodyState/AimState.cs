using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimState : StateMachineBehaviour
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
        targetPos = GameObject.FindObjectOfType<PlayerAction>().transform.GetChild(0).GetComponent<Collider>().bounds.center;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (TimeManager.GetInstance().IsTimePaused())
            return;

        aimTarget.AimToObject(targetPos = GameObject.FindObjectOfType<PlayerAction>().transform.GetChild(0).GetComponent<Collider>().bounds.center);
        animator.GetComponentInParent<EnemyInfo>().AttackDelayTimer += TimeManager.GetInstance().customDeltaTime;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponentInParent<EnemyInfo>().AttackDelayTimer = 0;
    }
}