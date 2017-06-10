using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ConditionChecker : MonoBehaviour
{
    private int stateTypeNum;

    protected EnemyInfo enemyInfo;
    protected Animator animator;
    protected Animator shoulderAnimator;
    protected SpriteRenderer[] spriteRenderers;

    protected void Awake()
    {
        enemyInfo = GetComponentInParent<EnemyInfo>();
        animator = GetComponent<Animator>();
        shoulderAnimator = GetComponentInChildren<Animator>();
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        stateTypeNum = Enum.GetNames(typeof(StateType)).Length;
    }

    protected string GetCurrentStateName()
    {
        return AnimatorStateController.GetCurrentStateName(EnumType.StateEnum, animator);
    }
}
