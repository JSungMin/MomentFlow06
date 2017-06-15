using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

// TODO 이름을 바꿔야 할거 같다
public class ConditionChecker : MonoBehaviour
{
	private int bodyStateTypeNum;
	private int shoulderStateTypeNum;

    protected EnemyInfo enemyInfo;
    protected Animator animator;
    protected Animator shoulderAnimator;
    protected SpriteRenderer[] spriteRenderers;

    protected void Awake()
    {
        enemyInfo = GetComponentInParent<EnemyInfo>();
        animator = GetComponent<Animator>();
		shoulderAnimator = animator.transform.GetChild(0).GetComponentInChildren<Animator>();
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
		bodyStateTypeNum = Enum.GetNames(typeof(BodyStateType)).Length;
		shoulderStateTypeNum = Enum.GetNames (typeof(ShoulderStateType)).Length;

        IStateBehaviour[] stateBehaviours = animator.GetBehaviours<IStateBehaviour>();
        for (int i = 0; i < stateBehaviours.Length; i++)
        {
            stateBehaviours[i].LoadData(transform.GetChild(0).GetComponent<SpriteRenderer>(), enemyInfo);
        }

        stateBehaviours = shoulderAnimator.GetBehaviours<IStateBehaviour>();
        for (int i = 0; i < stateBehaviours.Length; i++)
        {
            stateBehaviours[i].LoadData(transform.GetChild(0).GetComponent<SpriteRenderer>(), enemyInfo);
        }
    }

    protected string GetCurrentBodyStateName()
    {
        return AnimatorStateController.GetCurrentStateName(EnumType.BodyStateEnum, animator);
    }

    protected string GetCurrentShoulderStateName()
    {
        return AnimatorStateController.GetCurrentStateName(EnumType.ShoulderStateEnum, shoulderAnimator);
    }

    protected BodyStateType GetCurrentBodyState()
    {
        return AnimatorStateController.GetCurrentBodyState(animator);
    }

    protected ShoulderStateType GetCurrentShoulderState()
    {
        return AnimatorStateController.GetCurrentShoulderState(animator);
    }
}
