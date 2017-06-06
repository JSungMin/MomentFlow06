using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionChecker : MonoBehaviour
{
    protected EnemyInfo enemyInfo;
    protected Animator animator;
    protected SpriteRenderer[] spriteRenderers;

    protected void Awake()
    {
        enemyInfo = GetComponentInParent<EnemyInfo>();
        animator = GetComponent<Animator>();
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    protected string GetCurrentStateName()
    {
        for (int i = 0; i < 3; i++)
        {
            if (Animator.StringToHash(((StateType)((int)StateType.Die + i)).ToString()) == 
                animator.GetCurrentAnimatorStateInfo(0).shortNameHash)
            {
                return (StateType.Die + i).ToString();
            }
        }
        return "NONE";
    }
}
