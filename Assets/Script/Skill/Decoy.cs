using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decoy : SkillBase {

	private Sprite sprite;
	private GameObject decoyObject;

	public Decoy (HumanInfo ownerInfo, KeyCode keyCode, float manaCost)
	{
		this.ownerInfo = ownerInfo;
		this.sprite = ownerInfo.GetComponentInChildren<SpriteRenderer> ().sprite;
		this.keyCode = keyCode;
		this.manaCost = manaCost;
		decoyObject = Resources.Load ("Prefabs/Skill/DecoyObject") as GameObject;
	}

	#region implemented abstract members of SkillBase

	public override bool IsTryUseSKill ()
	{
		return TimeManager.isTimeSlowed &&
			PlayerAction.numberPadToggleInput[InputParsher.KeyCodeToInteger(keyCode) - 1] &&
			Input.GetMouseButtonDown (0);
	}

	public override bool IsTryCancelSkill ()
	{
		return !TimeManager.isTimeSlowed || !PlayerAction.numberPadToggleInput[InputParsher.KeyCodeToInteger(keyCode)];
	}

	protected override bool CanUseSkill ()
	{
		if (ownerInfo.mana.ManaPoint >= manaCost)
			return true;
		return false;
	}

	protected override bool CanCancelSkill ()
	{
		return true;
	}

	protected override void UseSkill ()
	{
		var position = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		position.z = 0;
		var decoy = Instantiate (decoyObject, position, Quaternion.identity);
		decoy.GetComponent<SpriteRenderer> ().sprite = sprite;

		TimeManager.GetInstance ().TimeNormalize ();
	}

	protected override void CancelSkill ()
	{
		//Debug.Log ();
	}

	#endregion
}
