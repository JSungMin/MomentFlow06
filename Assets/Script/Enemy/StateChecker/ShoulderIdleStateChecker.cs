using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoulderIdleStateChecker : StateCheckerBase
{
	public ShoulderIdleStateChecker(EnemyInfo enemyInfo, ShoulderStateType stateType)
    {
        this.enemyInfo = enemyInfo;
		this.shoulderStateType = stateType;
    }

    public override bool IsSatisfied()
    {
		return true;
	}
}