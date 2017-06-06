using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleStateChecker : StateCheckerBase
{
    public IdleStateChecker(EnemyInfo enemyInfo, StateType stateType)
    {
        this.enemyInfo = enemyInfo;
        this.stateType = stateType;
    }

    public override bool IsSatisfied()
    {
        return true;
    }
}