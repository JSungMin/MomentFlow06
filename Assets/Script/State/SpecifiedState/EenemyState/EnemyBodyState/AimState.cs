using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimState : IStateBehaviour
{
    private AimTarget aimTarget;
    private Vector3 targetPos;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (null == aimTarget)
        {
            aimTarget = animator.GetComponentInParent<AimTarget>();
        }

        enemyInfo.AttackDelayTimer = 0;

        Collider targetCollider = enemyInfo.attackTarget.GetComponent<Collider>();
        if (targetCollider == null)
            targetCollider = enemyInfo.attackTarget.GetComponentInChildren<Collider>();

        targetPos = targetCollider.bounds.center + Vector3.up * Random.Range(0, targetCollider.bounds.extents.y);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (TimeManager.GetInstance().IsTimePaused())
            return;

        aimTarget.AimToObject(targetPos);

		var point01 = aimTarget.GetComponentInChildren<EnemyShoulderAction> ().shotPosition.position;
		var point02 = targetPos;
		RaycastHit hit;
		if (Physics.Raycast (
			aimTarget.GetComponentInChildren<EnemyShoulderAction>().transform.position,
			(point02 - point01).normalized,
			out hit,
			(point02 - point01).magnitude,
			1 << LayerMask.NameToLayer ("Collision"))
		)
		{
			point02 = hit.point;
		}

		AimLineRenderer.instance.startPoint.Add (point01);
		AimLineRenderer.instance.endPoint.Add (point02);
		AimLineRenderer.instance.pointAlpha.Add (animator.GetComponentInParent<EnemyInfo>().AttackDelayTimer);

        enemyInfo.AttackDelayTimer += TimeManager.GetInstance().customDeltaTime;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyInfo.AttackDelayTimer = 0.0f;

        enemyInfo.alertSituation.GetInSituation(3.0f);
    }
}