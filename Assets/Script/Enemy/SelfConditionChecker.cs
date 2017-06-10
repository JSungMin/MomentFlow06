using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO idle, shot, dead
public class SelfConditionChecker : ConditionChecker
{
	private StateCheckerBase[] bodyStateCheckers;
	private StateCheckerBase[] shoulderStateCheckers;

    private new void Awake()
    {
        base.Awake();
        // 인자 중요함
        // refactoring 하고 싶은데 어떻게 해야할지 모르겠다
        bodyStateCheckers = new StateCheckerBase[] 
        {
			new BodyDeadStateChecker(enemyInfo, BodyStateType.Die),
			new BodyShotStateChecker(enemyInfo, BodyStateType.Shot),
			new BodyIdleStateChecker(enemyInfo, BodyStateType.Idle)
        };

		shoulderStateCheckers = new StateCheckerBase[] 
		{
			new ShoulderIdleStateChecker (enemyInfo, ShoulderStateType.Idle),
			new ShoulderAimStateChecker (enemyInfo, ShoulderStateType.Aim),
			new ShoulderShotStateChecker (enemyInfo, ShoulderStateType.Shot)
		};

    }

    private void Update()
    {
        for (int i = 0; i < bodyStateCheckers.Length; i++)
        {
            if (bodyStateCheckers[i].IsSatisfied())
            {
				animator.SetTrigger("Trigger" + bodyStateCheckers[i].bodyStateType.ToString());
            }
        }
		for (int i = 0 ; i < shoulderStateCheckers.Length; i++)
		{
			if (shoulderStateCheckers [i].IsSatisfied ())
			{
				shoulderAnimator.SetTrigger ("Trigger" + shoulderStateCheckers[i].shoulderStateType.ToString());	
			}
		}
    }
}