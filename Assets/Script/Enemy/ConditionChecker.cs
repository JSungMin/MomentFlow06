using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

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
    }

    protected string GetCurrentStateName()
    {
		return "";
       // return AnimatorStateController.GetCurrentStateName(EnumType.StateEnum, animator);
    }
}
