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

	public TimePause(HumanInfo oInfo, KeyCode keyCode, float manaCost, float manaCostPer, float manaCostDelTm)
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
		TimeManager.GetInstance ().TimePause ();
		ownerInfo.mana.AddMana (-manaCost);
        soundPlayer.StopBGM();
		addManaForCoroutine = ownerInfo.StartCoroutine(ownerInfo.mana.AddManaFor(-manaCostPer, manaCostDelTm, true));

        GameObject.Find ("PauseEffect").GetComponent<ParticleSystem> ().Play ();
    }
    
    protected override void CancelSkill()
    {
		TimeManager.GetInstance ().TimeResume ();

        soundPlayer.PlayBGM();
		ownerInfo.StopCoroutine(addManaForCoroutine);
    }
}
