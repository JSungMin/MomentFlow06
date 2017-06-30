using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : IStateBehaviour
{
    private const float speed = 1.0f;
    
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (null == dynamicObject)
		{
			dynamicObject = animator.GetComponentInParent<DynamicObject> ();
		}
	}

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // hide
        if (enemyInfo.isHaveToHide())
        {
            enemyInfo.SetDirectionOppositeTo(enemyInfo.attackTarget);
        }
        // chase player
        else
        {
            enemyInfo.SetDirectionTo(enemyInfo.attackTarget);
        }

		enemyInfo.transform.Translate(enemyInfo.GetDirection() * dynamicObject.customDeltaTime * speed);
    }


    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (enemyInfo.isHaveToHide())
            enemyInfo.SetDirectionTo(enemyInfo.attackTarget);
    }
}
