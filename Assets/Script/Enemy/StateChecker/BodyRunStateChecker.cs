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
            // 오브젝트와 적 케릭터 사이에 방해물이 있다면
            if (enemyInfo.IsNearestObjectBetween(enemyInfo.attackTarget, enemyInfo.sameRawObstacles))
                return false;
            else
            {
                if (enemyInfo.IsCloseToOneOf(enemyInfo.sameRawWalls))
                    return false;
                else
                    return true;
            }
        }
        else
        {
            if (enemyInfo.IsObjectInViewWithoutObstacle(enemyInfo.attackTarget))
                Debug.Log("is in object view range");

            if (!enemyInfo.IsInAttackRange(enemyInfo.attackTarget) &&
                enemyInfo.IsInFindRange(enemyInfo.attackTarget) &&
                enemyInfo.IsObjectInViewWithoutObstacle(enemyInfo.attackTarget))
            {
                return true;
            }
            else
                return false;
        }
    }
}
