using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoulderAimStateChecker : StateCheckerBase
{
	public ShoulderAimStateChecker(EnemyInfo enemyInfo, ShoulderStateType stateType)
    {
        this.enemyInfo = enemyInfo;
		this.shoulderStateType = stateType;
    }

    public override bool IsSatisfied()
    {
		Debug.Log ("Shoulder Aim, " + enemyInfo.AttackDelayTimer + " , " + enemyInfo.attackDelay);
        if (Vector3.Distance(EnemyInfo.player.transform.position, enemyInfo.transform.position) < enemyInfo.attackRange)
        {
			if (enemyInfo.AttackDelayTimer < enemyInfo.attackDelay) {
				Debug.Log ("AIm True");
				return true;
			}
            else
                return false;
        }
        else
            return false;
    }
}