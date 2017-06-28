using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePause : SkillBase
{
	public TimePause(HumanInfo oInfo ,KeyCode keyCode,float manaCost)
    {
        soundPlayer = GameObject.FindObjectOfType<SoundPlayer>();
		this.ownerInfo = oInfo;
		this.keyCode = keyCode;
        this.manaCost = manaCost;
    }

    public override bool IsTryUseSKill()
    {
        //return Input.GetKeyDown(keyCode) && !isTimePaused;
		return Input.GetKeyDown(keyCode) && !TimeManager.isTimePaused;
    }

    public override bool IsTryCancelSkill()
    {
        //return Input.GetKeyDown(keyCode) && isTimePaused;
		return Input.GetKeyDown(keyCode) && TimeManager.isTimePaused;
    }

    protected override bool CanUseSkill()
    {
		if (ownerInfo.mana.ManaPoint >= manaCost)
            return true;
        return false;
    }

    protected override bool CanCancelSkill()
    {
        return true;
    }

    protected override void UseSkill()
    {
		TimeManager.isTimePaused = true;
		ownerInfo.mana.AddMana(-manaCost);
        TimeManager.GetInstance().SetTimeScale(0.0f);
		TimeManager.isTimePaused = true;
        soundPlayer.StopBGM();

		GameObject.Find ("PauseEffect").GetComponent<ParticleSystem> ().Play ();
		var animators = GameObject.FindObjectsOfType<Animator> ();
		for (int i = 0; i < animators.Length; i++)
		{
			if (!animators[i].CompareTag("Player") &&
				!animators[i].CompareTag("InteractableObject"))
				animators [i].speed = TimeManager.GetInstance ().customTimeScale;
		}
    }

    protected override void CancelSkill()
    {
		TimeManager.isTimePaused = false;
        TimeManager.GetInstance().SetTimeScale(1.0f);
		TimeManager.isTimePaused = false;

        soundPlayer.PlayBGM();

        var animators = GameObject.FindObjectsOfType<Animator> ();
		for (int i = 0; i < animators.Length; i++)
		{
			animators [i].speed = TimeManager.GetInstance ().customTimeScale;
		}
    }

	void Update()
	{
		
	}
}
