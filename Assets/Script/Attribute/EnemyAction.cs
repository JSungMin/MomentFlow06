using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : MonoBehaviour {
	public EnemyInfo enemyInfo;
	public OutsideInfo enemyOutsideInfo;
	public BoxCollider enemyBodyCollider;

	public Animator bodyAnimator;
	public Animator shoulderAnimator;

	public SelfConditionChecker selfConditionChecker;
	
	public LayerMask findOutLayerMask;
	public LayerMask targetLayerMask;
	public float findRange;

	public List<ActionCheckerBase> actions;

	public GameObject attackTarget;
	public GameObject detectedTarget;

	public Vector3 suspiciousPoint;

	public List<int> targetZList;
	public int targetZListOffet = 0;

	// Use this for initialization
	void Start () {
		if (null == enemyInfo)
			enemyInfo.GetComponentInParent<EnemyInfo> ();
		if (null == selfConditionChecker)
			selfConditionChecker = GetComponent<SelfConditionChecker> ();
		enemyBodyCollider = enemyInfo.boxCollider;
	}
	
	// Update is called once per frame
	void Update () {
		if (!enemyInfo.isUpdatable)
			return;

		transform.position = new Vector3 (
			transform.position.x,
			transform.position.y,
			Mathf.Round(transform.position.z * 10f)/10f
		);

		if (null == enemyInfo.attackTarget)
			enemyInfo.attackTarget = GameSceneData.playerAction;

		SearchTarget ();

		SelectActionbyDetectGauge ();

		SelectActionByAttackRange ();

		actions [(int)enemyInfo.actionType].TryAction ();
	}

	public void SelectActionbyDetectGauge ()
	{
		//Detect하지 못 한 상태에서의 행동을 결정한다.
		if (enemyInfo.isDetect) {
			if (null == detectedTarget)
				detectedTarget = attackTarget;
			return;
		}



		if (enemyInfo.detectGauge == 0)
		{
			enemyInfo.actionType = EnemyActionType.Idle;
		}
		else if (enemyInfo.detectGauge > 0 && enemyInfo.detectGauge < enemyInfo.maxDetectGauge)
		{
			if (null != attackTarget) {
				suspiciousPoint = attackTarget.transform.position;
				enemyInfo.actionType = EnemyActionType.Suspicious;
			}
			else
				enemyInfo.actionType = EnemyActionType.SearchTarget;
		}
	}

	public void SelectActionByAttackRange ()
	{
		if (enemyInfo.isDetect) {
			if (null != attackTarget && 
				detectedTarget.transform.position.z == transform.position.z)
			{
				enemyInfo.actionType = EnemyActionType.Attack;
				suspiciousPoint = detectedTarget.transform.position;
			}
			else
			{
				enemyInfo.actionType = EnemyActionType.Chase;
				suspiciousPoint = detectedTarget.transform.position;
			}
		}
	}

	public Vector3 GetLookAtDirection ()
	{
		return enemyInfo.transform.localScale;
	}

	public RaycastHit[] FindObjectsInSight ()
	{
		var center = enemyBodyCollider.transform.position + enemyBodyCollider.center.y * Vector3.up - findRange * enemyInfo.transform.localScale.x * Vector3.right * 0.5f;
		var halfExtent = new Vector3 (findRange * 0.5f, enemyBodyCollider.size.y * 0.5f, 1f);
		var objects = Physics.BoxCastAll (center, halfExtent, GetLookAtDirection ().x * Vector3.right, Quaternion.identity, findRange, findOutLayerMask);

		return objects;
	}

	private void IdentifyAndApplyTarget (GameObject target)
	{
		if (target.CompareTag ("Enemy"))
		{
			if (target.GetComponentInParent<EnemyInfo> ().teamId != enemyInfo.teamId) {
				attackTarget = target;
				enemyInfo.SetAttackTarget (attackTarget);
			}
		}
		else if (target.CompareTag ("Player"))
		{
			attackTarget = target;
			enemyInfo.SetAttackTarget (attackTarget);
		}
	}

	//Enemy가 타겟을 감지하는 루틴
	public void SearchTarget()
	{
		var searchedObj = FindObjectsInSight ();

		var targets = GetTargetsInSight (searchedObj);

		attackTarget = null;
		if (null != attackTarget && !targets.Contains (attackTarget.GetComponentInChildren<Collider>()))
		{
			attackTarget = null;
		}

		for (int i = 0; i < targets.Count; i++)
		{
			var targetOutsideInfo = targets [i].GetComponentInParent <HumanInfo> ().GetComponentInChildren <OutsideInfo> ();
			if (!targetOutsideInfo.isOnObstacle) {
				IdentifyAndApplyTarget(targets [i].GetComponentInParent <HumanInfo> ().bodyCollider.gameObject);
			}
			else {
				if (targets [i].transform.position.z == enemyInfo.transform.position.z)
				{
					Debug.Log ("같은 Z축");
					IdentifyAndApplyTarget(targets [i].GetComponentInParent <HumanInfo> ().bodyCollider.gameObject);
				}
				//target이 다른 Z축에 있음
				else if (enemyOutsideInfo.switchableObstacle == null)
				{
					Debug.Log ("AI가 밖에서 지켜 봄");
					IdentifyAndApplyTarget(targets [i].GetComponentInParent <HumanInfo> ().bodyCollider.gameObject);
				}
				else if (enemyOutsideInfo.switchableObstacle != targetOutsideInfo.switchableObstacle)
				{
					Debug.Log ("다른 switchableObstacle에서 지켜 봤을 때");
					IdentifyAndApplyTarget(targets [i].GetComponentInParent <HumanInfo> ().bodyCollider.gameObject);
				}
				else if (!targets [i].GetComponentInParent <HumanInfo>().isCrouched)
				{
					Debug.Log ("캐릭터가 안 앉음");
					IdentifyAndApplyTarget(targets [i].GetComponentInParent <HumanInfo> ().bodyCollider.gameObject);
				}
			}
		}
		if (null != attackTarget) {
			enemyInfo.IncreaseDetectGauge ();

		} else {
			if (!enemyInfo.isDetect) {
				detectedTarget = null;
			}

			if (Vector3.Distance (enemyInfo.transform.position, suspiciousPoint) >= 0.05f)
			{
				return;
			}

			enemyInfo.DecreaseDetectGauge ();
		}
	}
		

	private List<Collider> GetTargetsInSight (RaycastHit[] inSightObjects) 
	{
		List<Collider> targetColliderList = new List<Collider> ();
		for (int i = 0; i < inSightObjects.Length; i++)
		{
			if ((1 << inSightObjects [i].collider.gameObject.layer & targetLayerMask) != 0)
			{
				targetColliderList.Add(inSightObjects [i].collider);
			}
		}
		return targetColliderList;
	}
}
