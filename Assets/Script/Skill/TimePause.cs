using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePause : SkillBase
{
<<<<<<< HEAD
	public TimePause(HumanInfo oInfo ,KeyCode keyCode,float manaCost)
=======
    public bool isTimePaused { private set; get; }
    private float manaCostPer;
    private float manaCostDelTm;
    private Coroutine addManaForCoroutine;

    public TimePause(KeyCode keyCode, float manaCost, float manaCostPer, float manaCostDelTm)
>>>>>>> origin/master
    {
        soundPlayer = GameObject.FindObjectOfType<SoundPlayer>();
		this.ownerInfo = oInfo;
		this.keyCode = keyCode;
        this.manaCost = manaCost;
        this.manaCostPer = manaCostPer;
        this.manaCostDelTm = manaCostDelTm;
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
		TimeManager.isTimePaused = false;
        TimeManager.GetInstance().SetTimeScale(1.0f);
		TimeManager.isTimePaused = false;

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
