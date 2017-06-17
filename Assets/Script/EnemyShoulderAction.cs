using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoulderAction : Shoulder
{
    private AimTarget enemyAimTarget;

    void Start()
    {
        base.Start();
        enemyAimTarget = GetComponentInParent<AimTarget>();
    }

    public void HideArm()
    {
        enemyAimTarget.hideShoulder = true;
        shoulderAnimator.enabled = false;
    }

    public void ActiveArm()
    {
        enemyAimTarget.hideShoulder = false;
        shoulderAnimator.enabled = true;
    }
}
