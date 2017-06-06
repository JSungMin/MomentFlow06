using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    private int hp;
    private Vector3 aimPos;
    // attack 하기 전 aim을 하고 있는 시간
    private float attackDelayTimer;
    public float attackDelay { private set; get; }
    public float attackRange { private set; get; }

    public static GameObject player { private set; get; }

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

    private void Awake()
    {
        hp = 100;
        player = GameObject.FindWithTag("Player");
        attackDelay = 3.0f;
        attackRange = 1.0f;
    }
}