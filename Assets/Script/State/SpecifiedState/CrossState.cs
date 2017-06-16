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

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rigid = enemyInfo.rigidBody;
        originY = rigid.transform.position.y;

        obstacle = enemyInfo.FindNearestObstacle().GetComponent<Collider>();
        obstacleHeight = obstacle.bounds.size.y;
        direction = new Vector3(obstacle.transform.position.x - animator.transform.position.x, 0).normalized;

        rigid.transform.localScale = new Vector3(-direction.x * Mathf.Abs(rigid.transform.localScale.x), rigid.transform.localScale.y);
        rigid.isKinematic = true;
        distance = Vector3.Distance(obstacle.bounds.center, animator.transform.position) * 2;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rigid.transform.Translate(direction * (distance / 0.66f) * Time.deltaTime);
        rigid.transform.position = new Vector3(
            rigid.transform.position.x,
            originY + heightCurve.Evaluate(stateInfo.normalizedTime) * obstacleHeight,
            rigid.transform.position.z);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rigid.isKinematic = false;
    }
}
