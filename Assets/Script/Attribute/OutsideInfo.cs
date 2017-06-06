using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideInfo : MonoBehaviour {

	public List<GameObject> obstacleList;

	public void OnTriggerEnter(Collider col)
	{
		if (col.CompareTag("Obstacle"))
		{
			obstacleList.Add (col.gameObject);
		}
	}

	public void OnTriggerExit(Collider col)
	{
		if (col.CompareTag("Obstacle"))
		{
			obstacleList.Remove (col.gameObject);
		}
	}

	public GameObject GetNearestObstacleObject()
	{
		if (obstacleList.Count != 0)
		{
			var tmpDis = Vector3.Distance (transform.position, obstacleList [0].transform.position);
			var index = 0;
			for (int i = 0 ; i < obstacleList.Count; i++)
			{
				var prevDis = tmpDis;
				tmpDis = Mathf.Min(tmpDis, Vector3.Distance (transform.position, obstacleList[i].transform.position));

				if (prevDis != tmpDis)
					index = i;
			}
			return obstacleList [index];
		}
		return null;
	}
}
