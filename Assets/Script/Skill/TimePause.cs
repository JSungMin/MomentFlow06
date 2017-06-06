using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePause : SkillBase
{
    public bool isTimePaused { private set; get; }

    public TimePause(KeyCode keyCode)
    {
        isTimePaused = false;
        this.keyCode = keyCode;
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
        return true;
    }

    protected override bool CanCancelSkill()
    {
        return true;
    }

    protected override void UseSkill()
    {
        isTimePaused = true;
        TimeManager.GetInstance().SetTimeScale(0.0f);
        Time.timeScale = 0.0f;
    }

    protected override void CancelSkill()
    {
        isTimePaused = false;
        TimeManager.GetInstance().SetTimeScale(1.0f);
        Time.timeScale = 1.0f;
    }
}
