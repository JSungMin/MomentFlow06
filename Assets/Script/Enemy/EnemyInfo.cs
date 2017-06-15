using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
	public int teamId;
	[SerializeField]
    private int hp;
    private Vector3 aimPos;
    // attack 하기 전 aim을 하고 있는 시간
    private float attackDelayTimer;
    public float attackDelay { private set; get; }
    public float attackRange { private set; get; }
    public float findRange { private set; get; }

    private void Awake()
    {
        hp = 100;
        attackDelay = 0.5f;
        attackRange = 3.0f;
        findRange = attackRange + 3.0f;
    }

    public Vector3 AimPos
    {
        set { aimPos = value; }
        get { return aimPos; }
    }

    public int Hp
    {
        set
        {
            hp = value;
            if (hp < 0)
                hp = 0;
            if (hp > 100)
                hp = 100;
        }

        get { return hp; }
    }

    public float AttackDelayTimer
    {
        set { attackDelayTimer = value; }
        get { return attackDelayTimer; }
    }

}