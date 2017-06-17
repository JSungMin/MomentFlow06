using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideInfo : MonoBehaviour {

	public List<GameObject> obstacleList;
	public List<GameObject> stairList;
	public Collider cutSceneTrigger;
	public List<GameObject> interactableObject;

	public static GameObject nearestStair = null;

	public void OnTriggerEnter(Collider col)
	{
		if (col.CompareTag("Obstacle"))
		{
			obstacleList.Add (col.gameObject);
		}
		if (col.CompareTag ("Stair"))
		{
			if (!stairList.Contains(col.gameObject))
				stairList.Add (col.gameObject);
		}
		if (col.CompareTag ("InteractableObject"))
		{
			if (!interactableObject.Contains (col.gameObject))
				interactableObject.Add (col.gameObject);
		}
		if (col.CompareTag ("CutSceneTrigger"))
		{
			cutSceneTrigger = col;
			cutSceneTrigger.GetComponent<CutSceneEventTrigger> ().StartTrigger ();
		}
	}

	public void OnTriggerStay (Collider col)
	{
		if (col.CompareTag ("Stair")) {
			if (!stairList.Contains (col.gameObject))
			{
				stairList.Add (col.gameObject);
			}
		}
		if (col.CompareTag ("InteractableObject"))
		{
			if (!interactableObject.Contains (col.gameObject))
				interactableObject.Add (col.gameObject);
		}
	}

	public void OnTriggerExit(Collider col)
	{
		if (col.CompareTag("Obstacle"))
		{
			obstacleList.Remove (col.gameObject);
		}

		if (col.CompareTag("Stair"))
		{
			if (stairList.Contains(col.gameObject))
				stairList.Remove (col.gameObject);
		}
		if (col.CompareTag ("InteractableObject"))
		{
			if (interactableObject.Contains (col.gameObject)) 
			{
				interactableObject.Remove (col.gameObject);
			}
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

	public GameObject GetNearestStairObject()
	{
		if (stairList.Count != 0)
		{
			var tmpDis = Vector3.Distance (transform.position, stairList [0].transform.position);
			var index = 0;
			for (int i = 0 ; i < obstacleList.Count; i++)
			{
				var prevDis = tmpDis;
				tmpDis = Mathf.Min(tmpDis, Vector3.Distance (transform.position, stairList[i].transform.position));

				if (prevDis != tmpDis)
					index = i;
			}
			nearestStair = stairList [index];
			return stairList [index];
		}
		return null;
	}
}
