using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyDieChecker : ActionCheckerBase {

	public GameObject[] dropItem;
	public bool isDropItem = false;

	public UnityEvent dieEvents;

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
		dieEvents.Invoke ();
		isDropItem = true;
	}

	public override void CancelAction()
	{

	}
	#endregion
}
