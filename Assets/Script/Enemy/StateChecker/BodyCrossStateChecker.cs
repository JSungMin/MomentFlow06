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
            if (enemyInfo.IsObstacleClose())
            {
                // 적 케릭터가 숨는 곳과 플레이어 사이에 있다면
                if ((enemyInfo.FindNearestObstaclePosX() > enemyInfo.transform.position.x &&
                    enemyInfo.transform.position.x > GameSceneData.player.transform.position.x) ||
                    (enemyInfo.FindNearestObstaclePosX() < enemyInfo.transform.position.x &&
                    enemyInfo.transform.position.x < GameSceneData.player.transform.position.x))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
        else
        {
            if (enemyInfo.IsObstacleClose() && enemyInfo.IsAlerting())
                return true;
            else
                return false;
        }
    }
}
