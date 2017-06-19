using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyStunStateChecker : StateCheckerBase
{
    public BodyStunStateChecker(EnemyInfo enemyInfo, BodyStateType stateType)
    {
        this.enemyInfo = enemyInfo;
        this.bodyStateType = stateType;
    }

    public override bool IsSatisfied()
    {
        return enemyInfo.IsStun();
    }
}
