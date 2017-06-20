using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDummyAnimation : MonoBehaviour {

	private Animator dummyAnimator;

	// Use this for initialization
	void Start () {
		dummyAnimator = GetComponent<Animator> ();
	}

	public void SetAnimationTrigger (string triggerName)
	{
		dummyAnimator.SetTrigger (triggerName);
	}

	public void ResetAnimationTrigger (string triggerName)
	{
		dummyAnimator.ResetTrigger (triggerName);
	}
}
