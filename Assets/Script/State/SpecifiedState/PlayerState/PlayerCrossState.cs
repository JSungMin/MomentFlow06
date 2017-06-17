using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrossState : StateMachineBehaviour
{
    public AnimationCurve heightCurve;

    private Rigidbody pRigid;
    private Collider obstacle;
    private Vector3 direction = Vector3.zero;

    private float originY;

    private float obstacleHeight = 0;

    private float distance;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        pRigid = animator.GetComponentInParent<Rigidbody>();
        originY = pRigid.transform.position.y;
        obstacle = animator.GetComponentInParent<OutsideInfo>().GetNearestObstacleObject().GetComponent<Collider>();
        obstacleHeight = obstacle.bounds.size.y;
        direction = new Vector3(obstacle.transform.position.x - animator.transform.position.x, 0).normalized;

        pRigid.transform.localScale = new Vector3(-direction.x * Mathf.Abs(pRigid.transform.localScale.x), pRigid.transform.localScale.y);

        animator.GetComponentInChildren<PlayerShoulderAction>().HideArm();
        pRigid.isKinematic = true;

        distance = Vector3.Distance(obstacle.bounds.center, animator.transform.position) * 2;
    }
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        pRigid.transform.Translate(direction * (distance / 0.66f) * Time.deltaTime);
        pRigid.transform.position = new Vector3(
            pRigid.transform.position.x,
            originY + heightCurve.Evaluate(stateInfo.normalizedTime) * obstacleHeight,
            pRigid.transform.position.z);
    }
    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponentInChildren<PlayerShoulderAction>().ActiveArm();
        pRigid.isKinematic = false;

        animator.ResetTrigger("TriggerCross");
    }
}
