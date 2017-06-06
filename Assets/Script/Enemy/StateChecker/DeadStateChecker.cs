using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadStateChecker : StateCheckerBase
{
    public DeadStateChecker(EnemyInfo enemyInfo, StateType stateType)
    {
        this.enemyInfo = enemyInfo;
        this.stateType = stateType;
    }

    public override bool IsSatisfied()
    {
        if (enemyInfo.Hp <= 0)
            return true;
        else
            return false;
    }
}