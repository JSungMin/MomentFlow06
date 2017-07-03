using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class ActionCheckerBase : MonoBehaviour {
	protected EnemyInfo enemyInfo;
	public EnemyAction enemyAction;
	public UnityAction additionalActionInStart;
	public UnityAction additionalActionInExit;

	// Use this for initialization
	protected void Start () {
		enemyAction = GetComponentInParent <EnemyAction> ();
		enemyInfo = enemyAction.enemyInfo;
	}

	public virtual void TryAction ()
	{
		if (IsSatisfied())
		{
			DoAction ();
		}
	}
	protected abstract bool IsSatisfied ();
	protected abstract void DoAction ();

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

	protected void InitTargetZListOffset (float myZ)
	{
		if (enemyAction.targetZListOffet != enemyAction.targetZList.Count)
		{
			Debug.Log ("Init : " + Mathf.RoundToInt (myZ));
			enemyAction.targetZListOffet = enemyAction.targetZList.LastIndexOf (Mathf.RoundToInt (myZ)) + 1;
		}
	}

	protected float GetTargetZIndexAtOffset ()
	{
		return enemyAction.targetZList [Mathf.Min(enemyAction.targetZList.Count - 1, enemyAction.targetZListOffet)];
	}

	protected bool CompareEnemyAndTargetOnSameZ ()
	{
		if (enemyAction.targetZListOffet == enemyAction.targetZList.Count)
		{
			return true;
		}
		return false;
	}
}
