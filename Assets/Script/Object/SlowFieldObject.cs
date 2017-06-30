using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowFieldObject : MonoBehaviour {

	public int segmentNum;
	public LayerMask collisionMask;
	public LayerMask dynamicObjectMask;

	public Vector3 max, min;
	public float fieldRange;
	[Range(0,1)]
	public float reductionAmount;
	private Vector3 centerPosition;

	public float lifeTime;
	public float playTime = 0f;

	public List<Collider> affectedList;

	// Use this for initialization
	void Start () {
		playTime = 0;
		affectedList = new List<Collider> ();
	}
	
	// Update is called once per frame
	void Update () {
		CalculateRect ();
		ApplyTimeReduction ();
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

			var hits = Physics.RaycastAll (centerPosition, dir, fieldRange, collisionMask);

			if (hits.Length == 0) {
				var emptyPoint = centerPosition + dir * fieldRange;
				Debug.DrawLine (centerPosition, emptyPoint, Color.blue);
				max = new Vector2 (Mathf.Max (max.x, emptyPoint.x), Mathf.Max (max.y, emptyPoint.y));
				min = new Vector2 (Mathf.Min (min.x, emptyPoint.x), Mathf.Min (min.y, emptyPoint.y));
			} else {
				var hitPoint = hits[0].point;
				hitPoint.z = 0;
				float dis = Vector3.Distance (centerPosition, hitPoint);
				for (int j = 0; j < hits.Length; j++)
				{
					var hit = hits [j].point;
					hit.z = 0;
					if (Vector3.Distance (centerPosition, hit) < dis)
					{
						hitPoint = hit;
						hitPoint.z = transform.position.z;
						dis = Vector3.Distance (centerPosition, hit);
					}
				}
				Debug.DrawLine (centerPosition, hitPoint, Color.blue);
		
				max = new Vector2 (Mathf.Max (max.x, hitPoint.x), Mathf.Max (max.y, hitPoint.y));
				min = new Vector2 (Mathf.Min (min.x, hitPoint.x), Mathf.Min (min.y, hitPoint.y));
			}
		}
	}

	private void ApplyTimeReduction ()
	{
		var halfExtend = new Vector3 (
			Mathf.Abs(max.x - min.x) * 0.5f,
			Mathf.Abs(max.y - min.y) * 0.5f,
			5
		);

		#if UNITY_EDITOR
		Debug.DrawLine (new Vector3 (min.x,max.y), new Vector3 (max.x,max.y), Color.blue);
		Debug.DrawLine (new Vector3 (min.x,min.y), new Vector3 (max.x,min.y), Color.blue);
		Debug.DrawLine (new Vector3 (min.x,max.y), new Vector3 (min.x, min.y), Color.blue);
		Debug.DrawLine (new Vector3 (max.x, max.y), new Vector3 (max.x, min.y), Color.blue);
		#endif

		var objects = Physics.BoxCastAll (centerPosition, halfExtend, Vector3.forward, Quaternion.identity, fieldRange * 0.5f, dynamicObjectMask);

		for (int i = 0; i < affectedList.Count; i++)
		{
			var obj = affectedList [i].bounds;
			if (obj.min.x >= max.x ||
				obj.max.x <= min.x ||
				obj.min.y >= max.y ||
				obj.max.y <= min.y)
			{
				var dynamicObject = affectedList [i].GetComponentInParent<DynamicObject> ();
				if (affectedList[i].gameObject.activeSelf)
					dynamicObject.BackToPreviousTimeScale();
				affectedList.RemoveAt (i);
			}
		}

		for (int i = 0; i < objects.Length; i++)
		{
			Debug.Log (objects[i].collider.name);
			if (affectedList.Contains (objects [i].collider)) {
				return;
			}
			var col = objects [i].collider.GetComponentInParent <DynamicObject>();
			if (null != col) {
				col.ChangeTimeScale (col.customTimeScale * reductionAmount);
				affectedList.Add (objects [i].collider);
			}
		}
	}
}
