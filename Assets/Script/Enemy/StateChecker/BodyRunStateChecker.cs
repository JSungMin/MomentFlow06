﻿using System.Collections;
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
            if (!enemyInfo.IsObstacleCloseToHide())
                return true;
            else
                return false;
        }
        else
        {
            if (Vector3.Distance(GameSceneData.player.transform.position, enemyInfo.transform.position) > enemyInfo.attackRange &&
                Vector3.Distance(GameSceneData.player.transform.position, enemyInfo.transform.position) < enemyInfo.findRange)
            {
                if (enemyInfo.IsPlayerInView())
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
    }
}
