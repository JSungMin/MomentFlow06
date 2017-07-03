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
}
