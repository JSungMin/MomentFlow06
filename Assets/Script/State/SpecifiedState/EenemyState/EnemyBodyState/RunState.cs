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
            enemyInfo.SetDirectionOppositeToPlayer();
            // enemyInfo.FindNearestObstacleAtDirection();
        }
        // chase player
        else
        {
            enemyInfo.SetDirectionToPlayer();
        }

        enemyInfo.transform.Translate(enemyInfo.GetDirection() * TimeManager.GetInstance().customDeltaTime * speed);
    }
}
