using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowField : SkillBase {

	private GameObject slowFieldObject;

	public SlowField (HumanInfo ownerInfo, KeyCode keyCode, float manaCost)
	{
		this.ownerInfo = ownerInfo;
		this.keyCode = keyCode;
		this.manaCost = manaCost;
		slowFieldObject = Resources.Load ("Prefabs/Skill/SlowField") as GameObject;
	}

	#region implemented abstract members of SkillBase

	public override bool IsTryUseSKill ()
	{
		return TimeManager.isTimeSlowed &&
		PlayerAction.numberPadToggleInput [InputParsher.KeyCodeToInteger (keyCode) - 1] &&
		Input.GetMouseButtonDown (0);
	}

	public override bool IsTryCancelSkill ()
	{
		return !TimeManager.isTimeSlowed || !PlayerAction.numberPadToggleInput [InputParsher.KeyCodeToInteger (keyCode)];
	}

	protected override bool CanUseSkill ()
	{
		if (ownerInfo.mana.ManaPoint >= manaCost)
		{
			return true;
		}
		return false;
	}

	protected override bool CanCancelSkill ()
	{
		return true;
	}

	protected override void UseSkill ()
	{
		var position = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		position.z = ownerInfo.transform.position.z;
		var slowField = Instantiate (slowFieldObject, position, Quaternion.identity);
		TimeManager.GetInstance ().TimeNormalize ();
	}

	protected override void CancelSkill ()
	{

	}

	#endregion
}
