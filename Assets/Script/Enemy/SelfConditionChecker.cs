using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO idle, shot, dead
public class SelfConditionChecker : ConditionChecker
{
    private StateCheckerBase[] stateCheckers;

    private new void Awake()
    {
        base.Awake();
        // 인자 중요함
        // refactoring 하고 싶은데 어떻게 해야할지 모르겠다
        stateCheckers = new StateCheckerBase[] 
        {
            new DeadStateChecker(enemyInfo, StateType.Die),
            new ShotStateChecker(enemyInfo, StateType.Shot),
            new IdleStateChecker(enemyInfo, StateType.Idle),
            new AimStateChecker(enemyInfo, StateType.Aim)
        };
    }

    private void Update()
    {
        for (int i = 0; i < stateCheckers.Length; i++)
        {
            if (stateCheckers[i].IsSatisfied())
            {
                animator.SetTrigger("Trigger" + stateCheckers[i].stateType.ToString());
            }
        }
    }
}