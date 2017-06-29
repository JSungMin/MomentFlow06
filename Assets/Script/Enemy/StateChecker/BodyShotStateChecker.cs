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
        if (enemyInfo.IsInAttackRange(enemyInfo.attackTarget) &&
            enemyInfo.IsObjectInView(enemyInfo.attackTarget) &&
			enemyInfo.AttackDelayTimer >= enemyInfo.attackDelay)
        {
            return true;
        }
        else
            return false;
    }
}