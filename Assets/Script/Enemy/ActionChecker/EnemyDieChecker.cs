using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDieChecker : ActionCheckerBase {

	public GameObject[] dropItem;
	public bool isDropItem = false;

	#region implemented abstract members of ActionCheckerBase
	protected override bool IsSatisfied ()
	{
		if (enemyInfo.hp <= 0 && !isDropItem)
		{
			return true;
		}
		return false;
	}
	protected override void DoAction ()
	{
		for (int i = 0; i < dropItem.Length; i++)
		{
			var newDropItem = Instantiate (dropItem[i], transform.position, Quaternion.identity) as GameObject;
		}
		isDropItem = true;
	}
	#endregion
}
