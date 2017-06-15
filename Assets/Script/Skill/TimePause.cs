using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePause : SkillBase
{
    public bool isTimePaused { private set; get; }

    public TimePause(KeyCode keyCode,float manaCost)
    {
        isTimePaused = false;
        this.keyCode = keyCode;
        this.manaCost = manaCost;
    }

    public override bool IsTryUseSKill()
    {
        return Input.GetKeyDown(keyCode) && !isTimePaused;
    }

    public override bool IsTryCancelSkill()
    {
        return Input.GetKeyUp(keyCode) && isTimePaused;
    }

    protected override bool CanUseSkill()
    {
        if (playerAction.mana.ManaPoint >= manaCost)
            return true;
        return false;
    }

    protected override bool CanCancelSkill()
    {
        return true;
    }

    protected override void UseSkill()
    {
        isTimePaused = true;
        playerAction.mana.ConsumeMana(manaCost);
        TimeManager.GetInstance().SetTimeScale(0.0f);

		var animators = GameObject.FindObjectsOfType<Animator> ();
		for (int i = 0; i < animators.Length; i++)
		{
			if (!animators[i].CompareTag("Player"))
				animators [i].speed = TimeManager.GetInstance ().customTimeScale;
		}
    }

    protected override void CancelSkill()
    {
        isTimePaused = false;
        TimeManager.GetInstance().SetTimeScale(1.0f);

		var animators = GameObject.FindObjectsOfType<Animator> ();
		for (int i = 0; i < animators.Length; i++)
		{
			animators [i].speed = TimeManager.GetInstance ().customTimeScale;
		}
    }
}
