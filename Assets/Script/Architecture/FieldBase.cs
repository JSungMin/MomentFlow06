using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FieldBase : MonoBehaviour {

	public int segmentNum;
	public LayerMask collisionMask;
	public LayerMask dynamicObjectMask;

	public Vector3 max, min;
	public float fieldRange;
	[Range(0,1)]
	public float reductionAmount;
	protected Vector3 centerPosition;

	public float lifeTime;
	public float playTime = 0f;

	public List<GameObject> affectedList;

	public void Start()
	{
		playTime = 0;
		affectedList = new List<GameObject> ();
	}

	protected void CalculateRect ()
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

	protected void RenderRect()
	{
		#if UNITY_EDITOR
		Debug.DrawLine (new Vector3 (min.x,max.y), new Vector3 (max.x,max.y), Color.blue);
		Debug.DrawLine (new Vector3 (min.x,min.y), new Vector3 (max.x,min.y), Color.blue);
		Debug.DrawLine (new Vector3 (min.x,max.y), new Vector3 (min.x, min.y), Color.blue);
		Debug.DrawLine (new Vector3 (max.x, max.y), new Vector3 (max.x, min.y), Color.blue);
		#endif
	}

	protected RaycastHit[] BoxCastAllInRect ()
	{
		var halfExtend = new Vector3 (
			Mathf.Abs(max.x - min.x) * 0.5f,
			Mathf.Abs(max.y - min.y) * 0.5f,
			5
		);

		var objects = Physics.BoxCastAll (centerPosition, halfExtend, Vector3.forward, Quaternion.identity, fieldRange * 0.5f, dynamicObjectMask);

		return objects;
	}

	protected DynamicObject[] FindDynamicObjectsInRect ()
	{
		var dynamicObjects = new List<DynamicObject> ();
		for (int i = 0; i < TimeManager.GetInstance().dynamicObjectList.Count; i++)
		{
			var element = TimeManager.GetInstance ().dynamicObjectList [i];
			dynamicObjects.Add (element);
		}
			
		for (int i = 0; i < dynamicObjects.Count; i++)
		{
			var col = dynamicObjects [i].GetComponent<Collider> ();
			if (null == col)
				col = dynamicObjects [i].GetComponentInChildren<Collider> ();
			if (null == col) {
				dynamicObjects.RemoveAt (i);
				i -= 1;
				continue;
			}

			if (col.bounds.min.x >= max.x ||
				col.bounds.max.x <= min.x ||
				col.bounds.min.y >= max.y ||
				col.bounds.max.y <= min.y)
			{
				dynamicObjects.RemoveAt (i);
				i -= 1;
			}
		}
		Debug.Log ("Elements Count : " + dynamicObjects.Count);
		return dynamicObjects.ToArray ();
	}

	protected abstract void DoActionForEachObject ();

	protected void DeleteOutOfRectDynamicObject ()
	{
		for (int i = 0; i < affectedList.Count; i++)
		{
			var obj = affectedList [i];
			if (null == obj)
			{
				affectedList.RemoveAt (i);
				return;
			}
			var col = obj.GetComponent<Collider> ();
			if (null == col)
				col = obj.GetComponentInChildren<Collider> ();
			var bounds = col.bounds;
			if (bounds.min.x >= max.x ||
				bounds.max.x <= min.x ||
				bounds.min.y >= max.y ||
				bounds.max.y <= min.y)
			{
				var dynamicObject = affectedList [i].GetComponentInParent<DynamicObject> ();
				if (affectedList[i].activeSelf)
					dynamicObject.BackToPreviousTimeScale();
				affectedList.RemoveAt (i);
			}
		}
	}

	protected abstract bool CheckDestroy ();
	protected abstract void DoDestroy ();
}
