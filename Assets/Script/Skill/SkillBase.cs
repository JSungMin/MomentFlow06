using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum SKILL
{
    TimePause = 0 ,
    TimeRevert = 1
}

public abstract class SkillBase : MonoBehaviour
{
    public int id;
    public KeyCode keyCode { protected set; get; }

	protected PlayerInfo playerInfo = GameObject.FindObjectOfType<PlayerInfo>();
    protected float manaCost;
    
    // 알아서 자식의 CanUseSkill을 호출 함
    // 외부에서는 UseSkill을 바로 호출할 수 없고 TryUseSkill을 호출해야 한다
    // TryUseSkill은 간단하게 CanUseSkill일 경우 UseSkill을 호출한다
    public void TryUseSkill()
    {
        if (CanUseSkill())
            UseSkill();
    }

    // CancelSkill도 위와 같다
    // bool 변수를 인자로 UseSkill과 통합할까 고민을 했지만 특수한 상황을 예방하기 위해 지금 방식이 더 좋을 것이라 생각했다
    // toggle 방식은 한 번 씹히게 되면 위험하기 때문이다
    public void TryCancelSkill()
    {
        if (CanCancelSkill())
            CancelSkill();
    }

    // 플레이어가 스킬을 사용하려하는가를 검사하는 함수이다
    // 스킬을 사용하고 있지 않은 상황에서 스킬 버튼을 누르면 true가 된다
    public abstract bool IsTryUseSKill();
    public abstract bool IsTryCancelSkill();

    // 스킬을 사용할 수 있는 상황인가를 리턴하는 함수이다
    protected abstract bool CanUseSkill();
    protected abstract bool CanCancelSkill();

    // 스킬을 사용해서 처리되는 루틴이다
    protected abstract void UseSkill();
    protected abstract void CancelSkill();
}
