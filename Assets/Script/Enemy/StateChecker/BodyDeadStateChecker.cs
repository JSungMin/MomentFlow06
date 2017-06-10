using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyDeadStateChecker : StateCheckerBase
{
	public BodyDeadStateChecker(EnemyInfo enemyInfo, BodyStateType stateType)
    {
        this.enemyInfo = enemyInfo;
		this.bodyStateType = stateType;
    }

    public override bool IsSatisfied()
    {
        if (enemyInfo.Hp <= 0)
            return true;
        else
            return false;
    }
}