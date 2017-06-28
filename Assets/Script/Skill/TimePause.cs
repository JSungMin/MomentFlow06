using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePause : SkillBase
{
    public bool isTimePaused { private set; get; }
    private float manaCostPer;
    private float manaCostDelTm;
    private Coroutine addManaForCoroutine;

    public TimePause(KeyCode keyCode, float manaCost, float manaCostPer, float manaCostDelTm)
    {
        soundPlayer = GameObject.FindObjectOfType<SoundPlayer>();
        isTimePaused = false;
        this.keyCode = keyCode;
        this.manaCost = manaCost;
        this.manaCostPer = manaCostPer;
        this.manaCostDelTm = manaCostDelTm;
    }

    public override bool IsTryUseSKill()
    {
        //return Input.GetKeyDown(keyCode) && !isTimePaused;
        return Input.GetKeyDown(keyCode) && !isTimePaused;
    }

    public override bool IsTryCancelSkill()
    {
        //return Input.GetKeyDown(keyCode) && isTimePaused;
        return Input.GetKeyDown(keyCode) && isTimePaused;
    }

    protected override bool CanUseSkill()
    {
        if (playerInfo.mana.ManaPoint >= manaCost)
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
        playerInfo.mana.AddMana(-manaCost);
        TimeManager.GetInstance().SetTimeScale(0.0f);

        soundPlayer.StopBGM();
        addManaForCoroutine = playerInfo.StartCoroutine(playerInfo.mana.AddManaFor(-manaCostPer, manaCostDelTm, true));

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
        isTimePaused = false;
        TimeManager.GetInstance().SetTimeScale(1.0f);

        soundPlayer.PlayBGM();
        playerInfo.StopCoroutine(addManaForCoroutine);

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
