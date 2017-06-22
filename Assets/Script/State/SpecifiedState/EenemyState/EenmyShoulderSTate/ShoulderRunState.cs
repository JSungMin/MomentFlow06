using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoulderRunState : IStateBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (enemyInfo.isHaveToHide())
            enemyInfo.SetDirectionOppositeTo(enemyInfo.attackTarget);
        else
            enemyInfo.SetDirectionTo(enemyInfo.attackTarget);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
}