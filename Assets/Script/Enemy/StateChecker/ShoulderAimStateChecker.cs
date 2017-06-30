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
        if (enemyInfo.IsInAttackRange(enemyInfo.attackTarget) &&
            enemyInfo.IsObjectInViewWithoutObstacle(enemyInfo.attackTarget) &&
            enemyInfo.AttackDelayTimer < enemyInfo.attackDelay)
        {
            return true;
        }
        else
            return false;
    }
}