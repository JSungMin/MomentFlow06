using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateCheckerBase : MonoBehaviour
{
    protected EnemyInfo enemyInfo;
	public BodyStateType bodyStateType { protected set; get; }
	public ShoulderStateType shoulderStateType { protected set; get; }

    public abstract bool IsSatisfied();
}