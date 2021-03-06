﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoulderShotStateChecker : StateCheckerBase
{
	public ShoulderShotStateChecker(EnemyInfo enemyInfo, ShoulderStateType stateType, BoolReturnDelegate isSatisfied)
    {
        this.enemyInfo = enemyInfo;
		this.shoulderStateType = stateType;
        this.isSatisfied = isSatisfied;
    }

    public override bool IsSatisfied()
    {
        return isSatisfied();
    }
}