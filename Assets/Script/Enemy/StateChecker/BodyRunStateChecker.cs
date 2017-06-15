using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyRunStateChecker : StateCheckerBase
{
    public BodyRunStateChecker(EnemyInfo enemyInfo, BodyStateType stateType)
    {
        this.enemyInfo = enemyInfo;
        this.bodyStateType = stateType;
    }

    public override bool IsSatisfied()
    {
        if (Vector3.Distance(GameSceneData.player.transform.position, enemyInfo.transform.position) > enemyInfo.attackRange &&
            Vector3.Distance(GameSceneData.player.transform.position, enemyInfo.transform.position) < enemyInfo.findRange)
        {
            if (IsInView())
                return true;
            else
                return false;
        }
        else
            return false;
    }

    private bool IsInView()
    {
        Debug.DrawRay(enemyInfo.transform.position + Vector3.up * 0.1f, Vector3.left);
        RaycastHit rayCastHit;
        if (Physics.Raycast(
            enemyInfo.transform.position + Vector3.up * 0.1f,
            Vector3.Normalize(Vector3.left),
            out rayCastHit,
            10.0f,
            (1 << LayerMask.NameToLayer("Collision")) | (1 << LayerMask.NameToLayer("Player"))
            ))
        {
            if (rayCastHit.collider.CompareTag("Player"))
                return true;
            else
                return false;
        }
        else
            return false;
    }
}
