using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossState : IStateBehaviour
{
    public AnimationCurve heightCurve;

    private Rigidbody rigid;
    private Collider obstacle;
    private Vector3 direction = Vector3.zero;

    private float originY;

    private float obstacleHeight = 0;

    private float distance;

    float selectedDirection;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
		if (null == dynamicObject)
		{
			dynamicObject = animator.GetComponentInParent<DynamicObject> ();
		}

        rigid = enemyInfo.rigidBody;
        originY = rigid.transform.position.y;

        obstacle = enemyInfo.FindNearest(enemyInfo.sameRawObstacles).GetComponent<Collider>();
        obstacleHeight = obstacle.bounds.size.y;
        direction = new Vector3(obstacle.transform.position.x - enemyInfo.transform.position.x, 0).normalized;
        selectedDirection = Mathf.Sign(enemyInfo.transform.position.x - obstacle.transform.position.x);

        animator.GetComponentInChildren<EnemyShoulderAction>().HideArm();

        rigid.isKinematic = true;
        distance = Vector3.Distance(obstacle.bounds.center, animator.transform.position) * 2;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
		rigid.transform.Translate(direction * (distance / 0.66f) * dynamicObject.customDeltaTime);
        rigid.transform.position = new Vector3(
            rigid.transform.position.x,
            originY + heightCurve.Evaluate(stateInfo.normalizedTime) * obstacleHeight,
            rigid.transform.position.z);
        rigid.transform.localScale = new Vector3(
            selectedDirection,
            rigid.transform.localScale.y,
            rigid.transform.localScale.z);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rigid.isKinematic = false;
        enemyInfo.SetDirectionTo(enemyInfo.attackTarget);
        animator.GetComponentInChildren<EnemyShoulderAction>().ActiveArm();
    }
}
