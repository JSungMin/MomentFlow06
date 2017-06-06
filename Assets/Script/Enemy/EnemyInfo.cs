using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    private int hp;
    private Vector3 aimPos;
    public static GameObject player { private set; get; }

    public Vector3 AimPos
    {
        set
        {
            aimPos = value;
        }

        get
        {
            return aimPos;
        }
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

        get
        {
            return hp;
        }
    }

    private void Awake()
    {
        hp = 100;
        player = GameObject.FindWithTag("Player");
    }
}