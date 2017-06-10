using System.Collections;
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
        if (Vector3.Distance(EnemyInfo.player.transform.position, enemyInfo.transform.position) < enemyInfo.attackRange)
        {
            if (enemyInfo.AttackDelayTimer >= enemyInfo.attackDelay)
                return true;
            else
                return false;
        }
        else
            return false;
    }
}