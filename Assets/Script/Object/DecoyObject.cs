using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecoyObject : MonoBehaviour {
	public DynamicObject dynamicObject;

	public int segmentNum;
	public Vector3 max, min;

	public float aggroRange;

	private Vector3 centerPosition;

	public LayerMask collisionMask;
	public LayerMask enemyMask;

	public float lifeTime;
	public float playTime = 0f;

	// Use this for initialization
	void Start () {
		playTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
		CalculateRect ();
		CheckEnemyInAggroRect ();

		if (CheckDestroy())
		{
			DoDestroy ();
		}
		else
			playTime += dynamicObject.customDeltaTime;
	}

	private void CalculateRect ()
	{
		centerPosition = GetComponent<Collider> ().bounds.center;
		max = centerPosition;
		min = centerPosition;

		var deltaAngle = 360 / segmentNum;

		for (int i = 0; i < segmentNum; i++)
		{
			var dir = Quaternion.Euler (0, 0, deltaAngle * i) * Vector3.right;

			var hits = Physics.RaycastAll (centerPosition, dir, aggroRange, collisionMask);

			if (hits.Length == 0) {
				var emptyPoint = centerPosition + dir * aggroRange;
				max = new Vector2 (Mathf.Max (max.x, emptyPoint.x), Mathf.Max (max.y, emptyPoint.y));
				min = new Vector2 (Mathf.Min (min.x, emptyPoint.x), Mathf.Min (min.y, emptyPoint.y));
			} else {
				var hitPoint = hits[hits.Length - 1].point;
				hitPoint.z = 0;
				float dis = Vector3.Distance (centerPosition, hitPoint);
				for (int j = 0; j < hits.Length; j++)
				{
					var hit = hits [j].point;
					hit.z = 0;
					if (Vector3.Distance (centerPosition, hit) < dis)
					{
						hitPoint = hits [j].point;
						hitPoint.z = transform.position.z;
						dis = Vector3.Distance (centerPosition, hit);
					}
				}
				max = new Vector2 (Mathf.Max (max.x, hitPoint.x), Mathf.Max (max.y, hitPoint.y));
				min = new Vector2 (Mathf.Min (min.x, hitPoint.x), Mathf.Min (min.y, hitPoint.y));
			}
		}
	}

	private void CheckEnemyInAggroRect ()
	{
		var halfExtend = new Vector3 (
			Mathf.Abs(max.x - min.x) * 0.5f,
			Mathf.Abs(max.y - min.y) * 0.5f,
			5
		);

		#if UNITY_EDITOR
		Debug.DrawLine (new Vector3 (min.x,max.y), new Vector3 (max.x,max.y), Color.red);
		Debug.DrawLine (new Vector3 (min.x,min.y), new Vector3 (max.x,min.y), Color.red);
		Debug.DrawLine (new Vector3 (min.x,max.y), new Vector3 (min.x, min.y), Color.red);
		Debug.DrawLine (new Vector3 (max.x, max.y), new Vector3 (max.x, min.y), Color.red);
		#endif

		var enemies = Physics.BoxCastAll (centerPosition, halfExtend, Vector3.right, Quaternion.identity, aggroRange, enemyMask);

		for (int i = 0; i < enemies.Length; i++)
		{
			enemies [i].collider.GetComponentInParent<EnemyInfo> ().attackTarget = gameObject;
		}
	}

	private bool CheckDestroy() 
	{
		if (playTime >= lifeTime)
		{
			return true;
		}
		return false;
	}
	float destroyTimer = 0;
	private void DoDestroy ()
	{
		if (destroyTimer <= 1f) {
			GetComponent<_2dxFX_Hologram2> ()._Alpha = Mathf.Lerp (destroyTimer, 1f, Time.deltaTime);
			destroyTimer += dynamicObject.customDeltaTime;
		} else {
			DestroyObject (this.gameObject);
		}
	}
}
