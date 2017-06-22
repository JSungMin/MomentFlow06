﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyShotStateChecker : StateCheckerBase
{
	public BodyShotStateChecker(EnemyInfo enemyInfo, BodyStateType stateType)
    {
        this.enemyInfo = enemyInfo;
		this.bodyStateType = stateType;
    }

    public override bool IsSatisfied()
    {
        if (Vector3.Distance(GameSceneData.player.transform.position, enemyInfo.transform.position) < enemyInfo.attackRange &&
            enemyInfo.IsObjectInView(GameSceneData.player) &&
            enemyInfo.AttackDelayTimer >= enemyInfo.attackDelay)
        {
            return true;
        }
        else
            return false;
    }
}