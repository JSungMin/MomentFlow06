using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimStateChecker : StateCheckerBase
{
    public AimStateChecker(EnemyInfo enemyInfo, StateType stateType)
    {
        this.enemyInfo = enemyInfo;
        this.stateType = stateType;
    }

    public override bool IsSatisfied()
    {
        return false;

        if (Vector3.Distance(EnemyInfo.player.transform.position, enemyInfo.transform.position) < enemyInfo.attackRange)
        {
            if (enemyInfo.AttackDelayTimer < enemyInfo.attackDelay)
                return true;
            else
                return false;
        }
        else
            return false;
    }
}
