using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyRunStateChecker : StateCheckerBase
{
    public BodyRunStateChecker(EnemyInfo enemyInfo, BodyStateType stateType)
    {
        this.enemyInfo = enemyInfo;
        this.bodyStateType = stateType;
    }

    public override bool IsSatisfied()
    {
        if (enemyInfo.isHaveToHide())
        {
            if (!enemyInfo.IsNearestObstacleBetween(GameSceneData.player))
                return true;

            if (!enemyInfo.IsObstacleClose())
                return true;
            else
                return false;
        }
        else
        {
            if (Vector3.Distance(GameSceneData.player.transform.position, enemyInfo.transform.position) > enemyInfo.attackRange &&
                Vector3.Distance(GameSceneData.player.transform.position, enemyInfo.transform.position) < enemyInfo.findRange &&
                enemyInfo.IsObjectInView(GameSceneData.player))
            {
                return true;
            }
            else
                return false;
        }
    }
}
