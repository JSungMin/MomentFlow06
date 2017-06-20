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
    [HideInInspector]
    public Rigidbody rigidBody;
    private GameObject[] obstacles;
    public List<GameObject> sameRawObstacles { private set; get; }
    // attack 하기 전 aim을 하고 있는 시간
    private float attackDelayTimer;

    //private 
    public float attackDelay { private set; get; }
    public float attackRange { private set; get; }
    public float findRange { private set; get; }
    
    private float crouchDelayTimer;
    public float crouchDelay { private set; get; }

    private AimTarget aimTarget;

    [HideInInspector]
    public float viewHeightScale = 1.0f;

    public bool isUpdatable = true;
    float sameRawDis = 0.6f;

    private void Awake()
    {
        alertSituation = new SituationTimer(this);
        behindObstacleShotingSituation = new SituationTimer(this);
        stunSituation = new SituationTimer(this);

        hp = 100;
        attackDelay = 0.5f;
        attackRange = 3.0f;
        crouchDelay = 2.0f;
        
        findRange = attackRange + 3.0f;
        rigidBody = GetComponent<Rigidbody>();
        viewCollider = GetComponent<BoxCollider>();
        boxCollider = GetComponentInChildren<BoxCollider>();

        obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        sameRawObstacles = new List<GameObject>();
        aimTarget = GetComponent<AimTarget>();

        for (int i = 0; i < obstacles.Length; i++)
        {
            if (isInSameRow(obstacles[i], gameObject))
            {
                sameRawObstacles.Add(obstacles[i]);
            }
        }
    }

    private void Update()
    {
		if (null != aimTarget)
        	aimTarget.CheckCanVisibleShoulder();
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

    public float CrouchDelayTimer
    {
        set { crouchDelayTimer = value; }
        get { return crouchDelayTimer; }
    }

    public bool IsPlayerInView()
    {
        if (!isInSameRow(GameSceneData.player, gameObject))
            return false;

        if (Vector3.Distance(GameSceneData.player.transform.position, transform.position) < 1.0f)
        {
            if (Mathf.Abs(GameSceneData.player.transform.position.y - transform.position.y) < sameRawDis)
                return true;
        }

        Vector3 origin = transform.position + Vector3.up * boxCollider.size.y * viewHeightScale;
        Vector3 direction = GetDirection();

        int layermask = (1 << LayerMask.NameToLayer("Collision")) | (1 << LayerMask.NameToLayer("Player"));

        bool isViewPlayer = IsPlayerInViewWithoutObstacle(origin, Vector3.Normalize(direction + Vector3.up * 0.05f), findRange, layermask) |
            IsPlayerInViewWithoutObstacle(origin, Vector3.Normalize(direction + Vector3.up * -0.05f), findRange, layermask);

        Debug.DrawRay(origin, Vector3.Normalize(direction + Vector3.up * 0.05f) * 10.0f);
        Debug.DrawRay(origin, Vector3.Normalize(direction + Vector3.up * (-0.05f)) * 10.0f);

        if (isViewPlayer)
            return true;
        else
            return false;
    }

    private bool IsPlayerInViewWithoutObstacle(Vector3 origin, Vector3 direction, float length, int layermask)
    {
        RaycastHit[] rayCastHits = Physics.RaycastAll(origin, direction, length, layermask);
        List<Collider> obstacles = new List<Collider>();

        bool IsPlayerInRay = false;
        float collisionPointDist = float.MaxValue;
        for (int i = 0; i < rayCastHits.Length; i++)
        {
            if (rayCastHits[i].collider != null)
            {
                if (rayCastHits[i].collider.CompareTag("Player"))
                {
                    if (rayCastHits[i].collider.name == "PlayerAction")
                    {
                        IsPlayerInRay = true;
                        collisionPointDist = Vector3.Distance(rayCastHits[i].collider.ClosestPoint(origin), origin);
                    }
                }
                else if(rayCastHits[i].collider.CompareTag("Obstacle"))
                {
                    obstacles.Add(rayCastHits[i].collider);
                }
            }
        }

        if (!IsPlayerInRay)
            return false;

        for (int i = 0; i < obstacles.Count; i++)
        {
            if (collisionPointDist > Vector3.Distance(obstacles[i].ClosestPoint(origin), origin))
                return false;
        }
        
        return true;
    }

    public bool GetDirectionIsLeft()
    {
        return transform.localScale == new Vector3(1, 1, 1);
    }

    public Vector3 GetDirection()
    {
        return GetDirectionIsLeft() == true ? Vector3.left : Vector3.right;
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

    public void SetDirectionToPlayer()
    {
        SetDirection(GameSceneData.player.transform.position.x < transform.position.x);
    }

    public void SetDirectionOppositeToPlayer()
    {
        SetDirection(GameSceneData.player.transform.position.x > transform.position.x);
    }

    // 체력이 3할 이하일 경우
    public bool isHaveToHide()
    {
        return Hp <= 30.0f;
    }
    
    public GameObject FindNearestObstacle()
    {
        GameObject nearestObj = null;
        float nearestDist = float.MaxValue;
        for (int i = 0; i < sameRawObstacles.Count; i++)
        {
            float distX = Mathf.Abs(transform.position.x - sameRawObstacles[i].transform.position.x);
            if (distX < nearestDist)
            {
                nearestDist = distX;
                nearestObj = sameRawObstacles[i];
            }
        }
        return nearestObj;
    }

    public GameObject FindNearestObstacleAtDirection()
    {
        GameObject nearestObj = null;
        float nearestDist = float.MaxValue;
        for (int i = 0; i < sameRawObstacles.Count; i++)
        {
            if (GetDirection().x * (sameRawObstacles[i].transform.position.x - transform.position.x) < 0.0f)
                continue;

            float distX = Mathf.Abs(transform.position.x - sameRawObstacles[i].transform.position.x);
            if (distX < nearestDist)
            {
                nearestDist = distX;
                nearestObj = sameRawObstacles[i];
            }
        }
        return nearestObj;
    }

    public float FindNearestObstaclePosX()
    {
        if (FindNearestObstacle() == null)
            return float.MaxValue;
        else
            return FindNearestObstacle().transform.position.x;
    }

    private const float nearObstacleDist = 0.55f;
    public bool IsObstacleClose()
    {
        if (Mathf.Abs(transform.position.x - FindNearestObstaclePosX()) < nearObstacleDist)
            return true;
        return false;
    }

    public bool IsNearestObstacleBetweenPlayer()
    {
        if ((transform.position.x < FindNearestObstaclePosX() &&
            FindNearestObstaclePosX() < GameSceneData.player.transform.position.x) ||
            (transform.position.x > FindNearestObstaclePosX() &&
            FindNearestObstaclePosX() > GameSceneData.player.transform.position.x))
            return true;
        else
            return false;
    }

    public SituationTimer alertSituation { private set; get; }
    public SituationTimer behindObstacleShotingSituation { private set; get; }
    public SituationTimer stunSituation { private set; get; }

    public bool isInSameRow(GameObject a, GameObject b)
    {
        return isInSameRow(a.GetComponent<Collider>(), b.GetComponent<Collider>());
    }

    public bool isInSameRow(Collider a, Collider b)
    {
        return a.bounds.max.y > b.bounds.min.y && a.bounds.min.y < b.bounds.max.y;
    }
}