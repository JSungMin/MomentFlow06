using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoulderDieStateChecker : StateCheckerBase
{
    public ShoulderDieStateChecker(EnemyInfo enemyInfo, ShoulderStateType stateType, BoolReturnDelegate isSatisfied)
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
