using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyCrossStateChecker : StateCheckerBase
{
    public BodyCrossStateChecker(EnemyInfo enemyInfo, BodyStateType stateType)
    {
        this.enemyInfo = enemyInfo;
        this.bodyStateType = stateType;
    }

    public override bool IsSatisfied()
    {
        if (enemyInfo.isHaveToHide())
        {
            if (enemyInfo.IsCloseToOneOf(enemyInfo.sameRawObstacles))
            {
                // 적 케릭터가 숨는 곳과 플레이어 사이에 있다면
                if ((enemyInfo.FindNearest(enemyInfo.sameRawObstacles).transform.position.x > enemyInfo.transform.position.x &&
                    enemyInfo.transform.position.x > enemyInfo.attackTarget.transform.position.x) ||
                    (enemyInfo.FindNearest(enemyInfo.sameRawObstacles).transform.position.x < enemyInfo.transform.position.x &&
                    enemyInfo.transform.position.x < enemyInfo.attackTarget.transform.position.x))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
        else
        {
            if (enemyInfo.IsCloseToOneOf(enemyInfo.sameRawObstacles) &&
                !enemyInfo.IsInAttackRange(enemyInfo.attackTarget) &&
                enemyInfo.IsInFindRange(enemyInfo.attackTarget) &&
                enemyInfo.IsNearestObjectBetween(enemyInfo.attackTarget, enemyInfo.sameRawObstacles) &&
                enemyInfo.IsObjectInViewWithoutObstacle(enemyInfo.attackTarget))
                return true;
            else
                return false;
        }
    }
}
