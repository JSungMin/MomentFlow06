using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO idle, shoot, dead
public class SelfConditionChecker : ConditionChecker
{
    private void Update()
    {
        if (enemyInfo.hp <= 0)
            animator.SetTrigger("SetDie");
    }
}