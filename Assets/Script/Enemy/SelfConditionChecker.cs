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
            new BodyCrossStateChecker(enemyInfo, BodyStateType.Cross),
            new BodyRunStateChecker(enemyInfo, BodyStateType.Run),
            new BodyCrouchStateChecker(enemyInfo, BodyStateType.Crouch),
			new BodyIdleStateChecker(enemyInfo, BodyStateType.Idle)
        };

		shoulderStateCheckers = new StateCheckerBase[]
        {
            new ShoulderDieStateChecker(enemyInfo, ShoulderStateType.Aim, bodyStateCheckers[(int)BodyStateType.Die].IsSatisfied),
			new ShoulderReloadStateChecker (enemyInfo, ShoulderStateType.Reload),
            new ShoulderShotStateChecker (enemyInfo, ShoulderStateType.Shot, bodyStateCheckers[(int)BodyStateType.Shot].IsSatisfied),
            new ShoulderRunStateChecker (enemyInfo, ShoulderStateType.Run, bodyStateCheckers[(int)BodyStateType.Run].IsSatisfied),
            new ShoulderAimStateChecker (enemyInfo, ShoulderStateType.Aim),
            new ShoulderIdleStateChecker (enemyInfo, ShoulderStateType.Idle)
		};

    }

    private void Update()
    {
        for (int i = 0; i < bodyStateCheckers.Length; i++)
        {
            if (bodyStateCheckers[i].IsSatisfied())
            {
                if (bodyStateCheckers[i].bodyStateType.ToString() == GetCurrentBodyStateName())
                    break;
                else
                {
                    animator.SetTrigger("Trigger" + bodyStateCheckers[i].bodyStateType.ToString());
                    break;
                }
            }
        }

		for (int i = 0 ; i < shoulderStateCheckers.Length; i++)
		{
			if (shoulderStateCheckers [i].IsSatisfied ())
            {
                if (shoulderStateCheckers[i].shoulderStateType.ToString() == GetCurrentShoulderStateName())
                    break;
                else
                {
                    shoulderAnimator.SetTrigger("Trigger" + shoulderStateCheckers[i].shoulderStateType.ToString());
                    break;
                }
			}
		}
    }
}