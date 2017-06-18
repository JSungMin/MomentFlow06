using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoulderReloadStateChecker : StateCheckerBase
{
	public ShoulderReloadStateChecker(EnemyInfo enemyInfo, ShoulderStateType stateType)
    {
        this.enemyInfo = enemyInfo;
		this.shoulderStateType = stateType;
    }

    public override bool IsSatisfied()
    {
		if (((Gun)enemyInfo.GetComponent<EquiptInfo>().nowEquiptWeapon).ammo <= 0)
		{
			return true;
		}
		return false;
    }
}