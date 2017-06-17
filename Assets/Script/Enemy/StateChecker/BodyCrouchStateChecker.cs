using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyCrouchStateChecker : StateCheckerBase
{
    public BodyCrouchStateChecker(EnemyInfo enemyInfo, BodyStateType stateType)
    {
        this.enemyInfo = enemyInfo;
        this.bodyStateType = stateType;
    }

    public override bool IsSatisfied()
    {
        if (enemyInfo.isHaveToHide() &&
            enemyInfo.IsObstacleClose() &&
            enemyInfo.CrouchDelayTimer < enemyInfo.crouchDelay &&
            !enemyInfo.IsBehindObstacleShoting())
        {
            return true;
        }
        else
            return false;
    }
}
