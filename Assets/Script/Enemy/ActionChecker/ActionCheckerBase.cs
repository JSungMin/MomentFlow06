using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class ActionCheckerBase : MonoBehaviour {
	protected EnemyInfo enemyInfo;
	public EnemyAction enemyAction;
	public UnityAction additionalActionInStart;
	public UnityAction additionalActionInExit;

	public Vector3 originPoint;
	public bool canUpdateOriginPoint;

	// Use this for initialization
	protected void Start () {
		enemyAction = GetComponentInParent <EnemyAction> ();
		enemyInfo = enemyAction.enemyInfo;
		originPoint = enemyInfo.transform.position;
	}

	public virtual void TryAction ()
	{
		if (IsSatisfied ()) {
			DoAction ();
		} else
			CancelAction ();
	}
	protected abstract bool IsSatisfied ();
	protected abstract void DoAction ();
	public abstract void CancelAction ();

	protected void CheckAndAddTargetZList (float targetZ)
	{
		if (enemyAction.targetZList.Count == 0) {
			
			enemyAction.targetZList.Add (Mathf.RoundToInt(targetZ));
			return;
		}
		if (enemyAction.targetZList [enemyAction.targetZList.Count - 1] != Mathf.RoundToInt(targetZ) )
		{
			enemyAction.targetZList.Add (Mathf.RoundToInt(targetZ));
		}
	}

	public void UpdateOriginPoint (Vector3 newPoint)
	{
		if (canUpdateOriginPoint)
		{
			originPoint = newPoint;
		}
	}

	protected void InitTargetZListOffset (float myZ)
	{
		if (enemyAction.targetZListOffet != enemyAction.targetZList.Count)
		{
			enemyAction.targetZListOffet = enemyAction.targetZList.LastIndexOf (Mathf.RoundToInt (myZ)) + 1;
		}
	}

	protected float GetTargetZIndexAtOffset ()
	{
		return enemyAction.targetZList [Mathf.Min(enemyAction.targetZList.Count - 1, enemyAction.targetZListOffet)];
	}

	protected bool CompareEnemyAndTargetOnSameZ ()
	{
		if (enemyAction.targetZListOffet == enemyAction.targetZList.Count &&
			enemyInfo.transform.position.z == enemyAction.targetZList[enemyAction.targetZListOffet - 1])
		{
			return true;
		}
		else if(enemyAction.targetZListOffet == enemyAction.targetZList.Count &&
			enemyInfo.transform.position.z != enemyAction.targetZList[enemyAction.targetZListOffet - 1])
		{
			enemyAction.targetZListOffet -= 1;
		}
		return false;
	}

	public TeleportYAxis FindNearestYTeleporter (List<TeleportYAxis> teleporters)
	{
		if (teleporters.Count != 0) {
			Vector3 pos01 = enemyInfo.transform.position;
			pos01.z = 0;
			Vector3 pos02 = teleporters [0].transform.position;
			pos02.z = 0;
			var dis = Vector3.Distance (pos01, pos02);
			var index = 0;
			for (int i = 0; i < teleporters.Count; i++)
			{
				pos02 = teleporters [i].transform.position;
				pos02.z = 0;
				var tmpDis = Vector3.Distance (pos01,pos02);
				if (tmpDis < dis)
				{
					index = i;
					dis = tmpDis;
				}
			}
			return teleporters [index];
		}
		return null;
	}

	public TeleportZAxis FindNearestZTeleporter (List<TeleportZAxis> teleporters)
	{
		if (teleporters.Count != 0) {
			Vector3 pos01 = enemyInfo.transform.position;
			Vector3 pos02 = teleporters [0].transform.position;

			var dis = Vector3.Distance (pos01, pos02);
			var index = 0;
			for (int i = 0; i < teleporters.Count; i++)
			{
				pos02 = teleporters [i].transform.position;
				var tmpDis = Vector3.Distance (pos01,pos02);
				if (tmpDis < dis)
				{
					index = i;
					dis = tmpDis;
				}
			}
			return teleporters [index];
		}
		return null;
	}
}
