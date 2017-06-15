using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoulderAimStateChecker : StateCheckerBase
{
	public ShoulderAimStateChecker(EnemyInfo enemyInfo, ShoulderStateType stateType)
    {
        this.enemyInfo = enemyInfo;
		this.shoulderStateType = stateType;
    }

    public override bool IsSatisfied()
    {
		if (Vector3.Distance(GameSceneData.player.transform.position, enemyInfo.transform.position) < enemyInfo.attackRange)
        {
            if (enemyInfo.AttackDelayTimer < enemyInfo.attackDelay)
            {
                return true;
            }
            else
                return false;
        }
        else
            return false;
    }
}