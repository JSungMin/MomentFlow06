using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoulderStunStateChecker : StateCheckerBase
{
    public ShoulderStunStateChecker(EnemyInfo enemyInfo, ShoulderStateType stateType, BoolReturnDelegate isSatisfied)
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
