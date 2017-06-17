﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeRecall : SkillBase
{
    public static bool isInTimeRevertPhase { private set; get; }

    private List<TimeRecallable> timeRevertables = new List<TimeRecallable>();

    public TimeRecall(KeyCode keyCode, float manaCost)
    {
        isInTimeRevertPhase = false;
        this.keyCode = keyCode;
        this.manaCost = manaCost;

        TimeRecallable[] tmpArray = GameObject.FindObjectsOfType<TimeRecallable>();

        // delegate 셋팅
        for (int i = 0; i < tmpArray.Length; i++)
        {
            tmpArray[i].AddOrRemoveTimeRevertable = AddOrRemoveTimeRevertableObj;
        }
    }

    public override bool IsTryUseSKill()
    {
		return Input.GetKeyDown(keyCode) && !isInTimeRevertPhase;
    }

    public override bool IsTryCancelSkill()
    {
        return Input.GetKeyUp(keyCode) && isInTimeRevertPhase;
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
        isInTimeRevertPhase = true;
		Time.timeScale = 0.25f;
		var animators = GameObject.FindObjectsOfType<Animator> ();
		for (int i = 0; i < animators.Length; i++)
		{
			animators [i].speed = TimeManager.GetInstance ().customTimeScale;
		}
    }

    // 키가 떼어졌을 때
    protected override void CancelSkill()
    {
        playerInfo.mana.ConsumeMana(manaCost);
        RevertObjs(timeRevertables);

        isInTimeRevertPhase = false;
        timeRevertables.Clear();

		Time.timeScale = 1f;
		var animators = GameObject.FindObjectsOfType<Animator> ();
		for (int i = 0; i < animators.Length; i++)
		{
			animators [i].speed = TimeManager.GetInstance ().customTimeScale;
		}
    }

    // 다른곳에서도 호출할 수 있지 않을까? 하는 생각 때문에 인자로 둠
    public void RevertObjs(List<TimeRecallable> objs)
    {
        for (int i = 0; i < objs.Count; i++)
        {
            objs[i].StartRevert();
            objs[i].StartRevertAni();
        }
    }

    private void AddOrRemoveTimeRevertableObj(TimeRecallable obj)
    {
        if (!isInTimeRevertPhase)
            return;

        if (timeRevertables.Contains(obj))
            RemoveTimeRevertableObj(obj);
        else
            AddTimeRevertableObj(obj);
    }

    private void AddTimeRevertableObj(TimeRecallable obj)
    {
        timeRevertables.Add(obj);
        obj.IndicateRevert();
    }

    private void RemoveTimeRevertableObj(TimeRecallable obj)
    {
        timeRevertables.Remove(obj);
        obj.NotIndicateRevert();
    }
}
