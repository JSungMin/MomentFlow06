using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikeAttack : SkillBase {

	public bool inProgressed = false;

	public StrikeAttack (HumanInfo ownerInfo ,KeyCode keyCode)
	{
		this.keyCode = keyCode;
		this.ownerInfo = ownerInfo;
	}

	#region implemented abstract members of SkillBase

	public override bool IsTryUseSKill ()
	{
		return Input.GetKeyDown (keyCode) && TimeManager.isTimePaused;
	}

	public override bool IsTryCancelSkill ()
	{
		return true;
	}

	protected override bool CanUseSkill ()
	{
		if (ownerInfo.mana.ManaPoint != 0)
			return true;
		return false;
	}

	protected override bool CanCancelSkill ()
	{
		return true;
	}

	protected override void UseSkill ()
	{
		if (ownerInfo.CompareTag ("Player"))
		{
			ownerInfo.GetComponentInChildren<PlayerAction> ().GetSkill<TimePause> ().TryCancelSkill ();
			ownerInfo.GetComponentInChildren<PlayerAction> ().playerAnimator.SetTrigger ("TriggerStrike");
		}
		inProgressed = true;
	}

	protected override void CancelSkill ()
	{
		
	}

	#endregion

	// Use this for initialization
	void Update () {

	}
}
