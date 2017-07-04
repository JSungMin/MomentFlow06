using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : IStateBehaviour
{
	private const float speed = 0.5f;

	public Vector3 targetPos;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (null == dynamicObject)
		{
			dynamicObject = animator.GetComponentInParent<DynamicObject> ();
		}

		if (null == enemyInfo)
		{
			enemyInfo = animator.GetComponentInParent <EnemyInfo> ();
		}
	}

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		// hide
		if (enemyInfo.isHaveToHide())
		{
			enemyInfo.SetDirectionOppositeTo(targetPos);
		}
		// chase player
		else
		{
			enemyInfo.SetDirectionTo(targetPos);
		}

		enemyInfo.transform.Translate(enemyInfo.GetDirection() * Time.deltaTime * Mathf.Pow(dynamicObject.customTimeScale, 3) * speed);
	}


	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (enemyInfo.isHaveToHide())
			enemyInfo.SetDirectionTo(enemyInfo.attackTarget);
	}
}