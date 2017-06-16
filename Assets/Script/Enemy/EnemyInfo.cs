using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : HumanInfo
{
	public int teamId;
	[SerializeField]
    private Vector3 aimPos;
    [HideInInspector]
    public BoxCollider boxCollider;
    [HideInInspector]
    public BoxCollider viewCollider;
    private Obstacle[] obstacles;
    public List<Obstacle> sameRawObstacles { private set; get; }
    // attack 하기 전 aim을 하고 있는 시간
    private float attackDelayTimer;
    //private 
    public float attackDelay { private set; get; }
    public float attackRange { private set; get; }
    public float findRange { private set; get; }

    private void Awake()
    {
        hp = 100;
        attackDelay = 0.5f;
        attackRange = 3.0f;
        findRange = attackRange + 3.0f;
        viewCollider = GetComponent<BoxCollider>();
        boxCollider = GetComponentInChildren<BoxCollider>();

        float sameRawDis = 3.0f;
        obstacles = GameObject.FindObjectsOfType<Obstacle>();
        sameRawObstacles = new List<Obstacle>();
        for (int i = 0; i < obstacles.Length; i++)
        {
            if (obstacles[i].transform.position.y < transform.position.y + sameRawDis &&
                obstacles[i].transform.position.y > transform.position.y - sameRawDis)
                sameRawObstacles.Add(obstacles[i]);
        }
    }

    private void Update()
    {
        Debug.Log(hp);
    }

    public Vector3 AimPos
    {
        set { aimPos = value; }
        get { return aimPos; }
    }

    public float Hp
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

    public bool IsPlayerInView()
    {
        Debug.DrawRay(transform.position + Vector3.up * boxCollider.bounds.size.y * 0.5f,
            Vector3.Normalize(Vector3.left + Vector3.up * 0.05f));
        RaycastHit rayCastHit;
        if (Physics.Raycast(
            transform.position + Vector3.up * boxCollider.bounds.size.y * 0.5f,
            Vector3.Normalize(Vector3.left + Vector3.up * 0.05f),
            out rayCastHit,
            findRange,
            (1 << LayerMask.NameToLayer("Collision")) | (1 << LayerMask.NameToLayer("Player"))
            ))
        {
            if (rayCastHit.collider.CompareTag("Player"))
                return true;
            else
                return false;
        }
        else
            return false;
    }

    public void SetDirection(bool toLeft)
    {
        if (toLeft)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    // 체력이 3할 이하일 경우
    public bool isHaveToHide()
    {
        return Hp <= 30.0f;
    }
    
    public Obstacle FindNearestObstacle()
    {
        Obstacle obstacle = new Obstacle();
        float nearestDist = float.MaxValue;
        for (int i = 0; i < sameRawObstacles.Count; i++)
        {
            float dist = Vector3.Distance(transform.position, sameRawObstacles[i].transform.position);
            if (dist < nearestDist)
            {
                nearestDist = dist;
                obstacle = sameRawObstacles[i];
            }
        }
        return obstacle;
    }

    private const float nearObstacleDist = 0.5f;
    public bool IsObstacleCloseToHide()
    {
        if (Vector3.Distance(transform.position, FindNearestObstacle().transform.position) < nearObstacleDist)
        {
            return true;
        }
        return false;
    }
}