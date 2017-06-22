﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
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

    private GameObject[] walls;
    public List<GameObject> sameRawWalls { private set; get; }
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

    // TODO 시연용으로 만들어진 것임 나중에 지우던가 할 것
    public bool isEnemyAttackableEnemy = false;
    // TODO 시연용으로 만들어진 것
    public GameObject attackTarget;

    private void Awake()
    {
        if (attackTarget == null)
            attackTarget = GameSceneData.playerAction;

        alertSituation = new SituationTimer(this);
        behindObstacleShotingSituation = new SituationTimer(this);
        stunSituation = new SituationTimer(this);
        attackDelaySituation = new SituationTimer(this);

        hp = 100;
        attackDelay = 0.5f;
        attackRange = 3.0f;
        crouchDelay = 2.0f;
        
        findRange = attackRange + 3.0f;
        rigidBody = GetComponent<Rigidbody>();
        viewCollider = GetComponent<BoxCollider>();
        boxCollider = GetComponentInChildren<BoxCollider>();
        aimTarget = GetComponent<AimTarget>();
        
        sameRawObstacles = AllocateSameRawObjectListByTag("Obstacle");
        sameRawWalls = AllocateSameRawObjectListByTag("Wall");
    }

    public void SetAttackTarget(GameObject target)
    {
        this.attackTarget = target;
    }

    private List<GameObject> AllocateSameRawObjectListByTag(string tag)
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(tag);
        List<GameObject> ret = new List<GameObject>();

        for (int i = 0; i < objs.Length; i++)
        {
            if (IsInSameRow(objs[i], gameObject))
                ret.Add(objs[i]);
        }

        return ret;
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

    public bool IsInAttackRange(GameObject obj)
    {
        return Vector3.Distance(obj.transform.position, transform.position) < attackRange;
    }

    public bool IsInFindRange(GameObject obj)
    {
        return Vector3.Distance(obj.transform.position, transform.position) < findRange;
    }

    public bool IsObjectInView(GameObject obj)
    {
        if (!IsInSameRow(obj, gameObject))
            return false;

        if (Vector3.Distance(obj.transform.position, transform.position) < 1.0f)
        {
            if (Mathf.Abs(obj.transform.position.y - transform.position.y) < sameRawDis)
                return true;
        }

        Vector3 origin = transform.position + Vector3.up * boxCollider.size.y * viewHeightScale;
        Vector3 direction = GetDirection();

        // TODO 이것도 바뀌어야함
        int layermask = (1 << LayerMask.NameToLayer("Collision")) | (1 << LayerMask.NameToLayer("Enemy")) | (1 << LayerMask.NameToLayer("Player"));
        
        int rayNum = 5;
        float deltaY = 0.03f;
        float minY = -(rayNum / 2) * deltaY;
        for (int i = 0; i < rayNum; i++)
        {
            if (IsObjectInViewWithoutObstacle(obj, origin, Vector3.Normalize(direction + Vector3.up * (minY + deltaY)), findRange, layermask))
                return true;
        }

        return false;
    }

    private bool IsObjectInViewWithoutObstacle(GameObject obj, Vector3 origin, Vector3 direction, float length, int layermask)
    {
        RaycastHit[] rayCastHits = Physics.RaycastAll(origin, direction, length, layermask);
        List<Collider> obstacles = new List<Collider>();

        bool IsPlayerInRay = false;
        float collisionPointDist = float.MaxValue;
        for (int i = 0; i < rayCastHits.Length; i++)
        {
            if (rayCastHits[i].collider != null)
            {
                if (rayCastHits[i].collider.gameObject == obj)
                {
                    IsPlayerInRay = true;
                    collisionPointDist = Vector3.Distance(rayCastHits[i].collider.ClosestPoint(origin), origin);
                }
                else if (rayCastHits[i].collider.CompareTag("Obstacle"))
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

    public void SetDirectionTo(GameObject obj)
    {
        SetDirection(obj.transform.position.x < transform.position.x);
    }

    public void SetDirectionOppositeTo(GameObject obj)
    {
        SetDirection(obj.transform.position.x > transform.position.x);
    }

    // 체력이 3할 이하일 경우
    public bool isHaveToHide()
    {
        return Hp <= 30.0f;
    }
    
    public GameObject FindNearest(List<GameObject> objs)
    {
        GameObject nearestObj = null;
        float nearestDist = float.MaxValue;
        for (int i = 0; i < objs.Count; i++)
        {
            float distX = Mathf.Abs(transform.position.x - objs[i].transform.position.x);
            if (distX < nearestDist)
            {
                nearestDist = distX;
                nearestObj = objs[i];
            }
        }
        return nearestObj;
    }

    public GameObject FindNearestObjectAtDirection(List<GameObject> objs)
    {
        GameObject nearestObj = null;
        float nearestDist = float.MaxValue;
        for (int i = 0; i < objs.Count; i++)
        {
            if (GetDirection().x * (objs[i].transform.position.x - transform.position.x) < 0.0f)
                continue;

            float distX = Mathf.Abs(transform.position.x - objs[i].transform.position.x);
            if (distX < nearestDist)
            {
                nearestDist = distX;
                nearestObj = objs[i];
            }
        }
        return nearestObj;
    }

    private const float nearObstacleDist = 0.6f;
    public bool IsCloseToOneOf(List<GameObject> objs)
    {
        if (objs.Count == 0)
            return false;

        if (Mathf.Abs(transform.position.x - FindNearest(objs).transform.position.x) < nearObstacleDist)
            return true;
        return false;
    }

    public bool IsNearestObjectBetween(GameObject outObj, List<GameObject> inObj)
    {
        if ((transform.position.x < FindNearest(inObj).transform.position.x && FindNearest(inObj).transform.position.x < outObj.transform.position.x) ||
            (transform.position.x > FindNearest(inObj).transform.position.x && FindNearest(inObj).transform.position.x > outObj.transform.position.x))
            return true;
        else
            return false;
    }

    public SituationTimer alertSituation { private set; get; }
    public SituationTimer behindObstacleShotingSituation { private set; get; }
    public SituationTimer stunSituation { private set; get; }
    public SituationTimer attackDelaySituation { private set; get; }

    public bool IsInSameRow(GameObject a, GameObject b)
    {
        Collider aCol = a.GetComponent<Collider>();
        if (aCol == null)
            aCol = a.GetComponentInParent<Collider>();
        if (aCol == null)
            aCol = a.GetComponentInChildren<Collider>();

        Collider bCol = b.GetComponent<Collider>();
        if (bCol == null)
            bCol = b.GetComponentInParent<Collider>();
        if (bCol == null)
            bCol = b.GetComponentInChildren<Collider>();
        return IsInSameRow(aCol, bCol);
    }

    public bool IsInSameRow(Collider a, Collider b)
    {
        return a.bounds.max.y > b.bounds.min.y && a.bounds.min.y < b.bounds.max.y;
    }
}