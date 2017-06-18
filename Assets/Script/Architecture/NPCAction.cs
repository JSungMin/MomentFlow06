using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class NPCAction : MonoBehaviour {

	public int tryCountForActivate;
	public int maxActionCircle = 1;
	protected int actionCirlce = 0;

	public UnityEvent finishEvent;

	protected bool IsActionFinished {
		get;
		set;
	}

	public abstract void TryAction (ref int tryCount);
	protected abstract bool IsSatisfied (ref int tryCount);
	protected abstract void DoAction ();
	public abstract void CancelAction ();
	protected abstract void FinishedAction();
}
