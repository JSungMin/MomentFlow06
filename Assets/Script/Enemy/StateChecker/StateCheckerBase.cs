using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate bool BoolReturnDelegate();

public abstract class StateCheckerBase : MonoBehaviour
{
    protected EnemyInfo enemyInfo;
	protected SelfConditionChecker selfChecker;

	public BodyStateType bodyStateType { protected set; get; }
	public ShoulderStateType shoulderStateType { protected set; get; }

    protected BoolReturnDelegate isSatisfied;

    public abstract bool IsSatisfied();
}