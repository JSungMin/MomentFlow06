using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateCheckerBase : MonoBehaviour
{
    protected EnemyInfo enemyInfo;
    public StateType stateType { protected set; get; }

    public abstract bool IsSatisfied();
}