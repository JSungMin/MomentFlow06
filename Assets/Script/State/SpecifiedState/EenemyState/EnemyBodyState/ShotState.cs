﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotState : IStateBehaviour
{
    public Transform shotPosition;
    private Weapon nowEquiptWeapon = null;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
		enemyInfo = animator.GetComponentInParent<EnemyInfo> ();
    }
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
		enemyInfo.GetComponentInChildren<EnemyAttackChecker> ().attackTimer = 0;
    }
}
