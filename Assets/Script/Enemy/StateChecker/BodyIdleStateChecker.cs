using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyIdleStateChecker : StateCheckerBase
{
	public BodyIdleStateChecker(EnemyInfo enemyInfo, BodyStateType stateType)
    {
        this.enemyInfo = enemyInfo;
		this.bodyStateType = stateType;
    }

    public override bool IsSatisfied()
    {
        return true;
    }
}