using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleStateChecker : StateCheckerBase
{
    public IdleStateChecker(EnemyInfo enemyInfo, StateType stateType)
    {
        this.enemyInfo = enemyInfo;
        this.stateType = stateType;
    }

    public override bool IsSatisfied()
    {
        if (Vector3.Distance(EnemyInfo.player.transform.position, enemyInfo.transform.position) < 1.0f && 
            enemyInfo.AttackDelayTimer > 3.0f)
        {
            return true;
        }
        else
            return false;
    }
}