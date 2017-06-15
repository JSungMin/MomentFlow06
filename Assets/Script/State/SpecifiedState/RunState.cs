using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : IStateBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (enemyInfo.transform.position.x < GameSceneData.player.transform.position.x)
            enemyInfo.transform.Translate(Vector3.right * Time.deltaTime);
        else
            enemyInfo.transform.Translate(Vector3.left * Time.deltaTime);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
}
