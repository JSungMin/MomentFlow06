using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadState : IStateBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Shoulder>().Reload();
        animator.ResetTrigger("TriggerReload");
    }
}
