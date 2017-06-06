using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionChecker : MonoBehaviour
{
    protected EnemyInfo enemyInfo;
    protected Animator animator;

    private void Awake()
    {
        enemyInfo = GetComponent<EnemyInfo>();
        animator = GetComponent<Animator>();
        Debug.Log("!!");
    }
}
