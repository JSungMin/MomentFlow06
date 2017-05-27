using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeRevert : SkillBase
{
    public bool isInTimeRevertPhase { private set; get; }

    public TimeRevert(KeyCode keyCode)
    {
        isInTimeRevertPhase = false;
        this.keyCode = keyCode;
    }

    public override bool IsTryUseSKill()
    {
        return Input.GetKeyDown(keyCode) && !isInTimeRevertPhase;
    }

    public override bool IsTryCancelSkill()
    {
        return Input.GetKeyDown(keyCode) && isInTimeRevertPhase;
    }

    protected override bool CanUseSkill()
    {
        return true;
    }

    protected override bool CanCancelSkill()
    {
        return true;
    }

    // 원래 기획은 스킬을 사용하면 시간이 멈추고
    // 마우스로 타겟을 설정한 뒤
    // 다시 스킬을 사용해서 실행시키는 거였던거 같음
    protected override void UseSkill()
    {
        Time.timeScale = 0.3f;
        isInTimeRevertPhase = true;
        // TODO mouse cursor로부터 받아올 것, MouseCursor 객체를 이 클래스에서 만들고 활용해야 할 듯
        Revert(GameObject.FindObjectOfType<TimeRevertable>());
    }

    protected override void CancelSkill()
    {
        Time.timeScale = 1.0f;
        isInTimeRevertPhase = false;
    }

    private void Revert(TimeRevertable obj)
    {
        obj.StartRevertFor(30);
    }
}
