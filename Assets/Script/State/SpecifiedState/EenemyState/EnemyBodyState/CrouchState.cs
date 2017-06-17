using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchState : IStateBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyInfo.StandAttackDelayTimer = 0.0f;
        enemyInfo.viewHeightScale = 0.5f;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyInfo.StandAttackDelayTimer += TimeManager.GetInstance().customDeltaTime;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyInfo.StandAttackDelayTimer = 0.0f;
        enemyInfo.viewHeightScale = 1.0f;
    }
}