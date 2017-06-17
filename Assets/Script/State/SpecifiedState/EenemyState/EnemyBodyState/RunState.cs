using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : IStateBehaviour
{
    private const float speed = 1.0f;
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // hide
        if (enemyInfo.isHaveToHide())
        {
            // 엄폐물이 오른쪽에 있다면
            if (enemyInfo.transform.position.x < enemyInfo.FindNearestObstacle().transform.position.x)
            {
                enemyInfo.transform.Translate(Vector3.right * TimeManager.GetInstance().customDeltaTime * speed);
                enemyInfo.SetDirection(false);
            }
            else
            {
                enemyInfo.transform.Translate(Vector3.left * TimeManager.GetInstance().customDeltaTime * speed);
                enemyInfo.SetDirection(true);
            }
        }
        // chase player
        else
        {
            if (enemyInfo.transform.position.x < GameSceneData.player.transform.position.x)
            {
                enemyInfo.transform.Translate(Vector3.right * TimeManager.GetInstance().customDeltaTime * speed);
                enemyInfo.SetDirectionTowardPlayer();
            }
            else
            {
                enemyInfo.transform.Translate(Vector3.left * TimeManager.GetInstance().customDeltaTime * speed);
                enemyInfo.SetDirectionTowardPlayer();
            }
        }
    }
}
