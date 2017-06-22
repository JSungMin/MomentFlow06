using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchState : IStateBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyInfo.CrouchDelayTimer = 0.0f;
        enemyInfo.viewHeightScale = 0.5f;
        enemyInfo.SetDirectionTo(enemyInfo.attackTarget);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyInfo.CrouchDelayTimer += TimeManager.GetInstance().customDeltaTime;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyInfo.CrouchDelayTimer = 0.0f;
        enemyInfo.viewHeightScale = 1.0f;
        enemyInfo.behindObstacleShotingSituation.GetInSituation(1.0f);
    }
}