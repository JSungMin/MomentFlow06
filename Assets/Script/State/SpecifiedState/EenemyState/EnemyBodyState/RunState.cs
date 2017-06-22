using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : IStateBehaviour
{
    private const float speed = 1.0f;
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // hide
        if (enemyInfo.isHaveToHide())
        {
            enemyInfo.SetDirectionOppositeTo(enemyInfo.attackTarget);
        }
        // chase player
        else
        {
            enemyInfo.SetDirectionTo(enemyInfo.attackTarget);
        }

        enemyInfo.transform.Translate(enemyInfo.GetDirection() * TimeManager.GetInstance().customDeltaTime * speed);
    }


    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (enemyInfo.isHaveToHide())
            enemyInfo.SetDirectionTo(enemyInfo.attackTarget);
    }
}
