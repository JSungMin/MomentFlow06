using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO
// Key pressing 한 상태에서는 시간이 느려지고, 오브젝트들을 클릭할 수 있게 된다
// 오브젝트들을 클릭하게 되면 list에 담아두고 key를 unpress하면 스킬이 시전된다.
public class TimeRevert : SkillBase
{
    public bool isInTimeRevertPhase { private set; get; }

    private List<TimeRevertable> timeRevertables = new List<TimeRevertable>();

    public TimeRevert(KeyCode keyCode)
    {
        isInTimeRevertPhase = false;
        this.keyCode = keyCode;

        TimeRevertable[] tmpArray = GameObject.FindObjectsOfType<TimeRevertable>();

        // delegate 셋팅
        for (int i = 0; i < tmpArray.Length; i++)
        {
            tmpArray[i].AddTimeRevertable = AddTimeRevertableObj;
            tmpArray[i].RemoveTimeRevertable = RemoveTimeRevertableObj;
        }
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
    
    protected override void UseSkill()
    {
        Time.timeScale = 0.1f;
        isInTimeRevertPhase = true;
        Revert(timeRevertables);
    }

    protected override void CancelSkill()
    {
        Time.timeScale = 1.0f;
        isInTimeRevertPhase = false;
    }

    // 다른곳에서도 호출할 수 있지 않을까? 하는 생각 때문에 인자로 둠
    public void Revert(List<TimeRevertable> objs)
    {
        for (int i = 0; i < objs.Count; i++)
        {
            objs[i].StartRevertFor(300);
        }
    }

    private void AddTimeRevertableObj(TimeRevertable obj)
    {
        timeRevertables.Add(obj);
    }

    private void RemoveTimeRevertableObj(TimeRevertable obj)
    {
        timeRevertables.Remove(obj);
    }
}
