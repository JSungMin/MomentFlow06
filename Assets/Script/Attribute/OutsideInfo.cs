using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideInfo : MonoBehaviour {
	public GameObject owner;

	public bool isOnObstacle;

	public List<GameObject> obstacleList;
	public GameObject switchableObstacle;
	public GameObject teleportEntrance;
	public List<GameObject> stairList;
	public Collider cutSceneTrigger;
	public List<GameObject> interactableObject;

	public List<GameObject> npcList;

	public List<GameObject> enemyList;

	public static GameObject nearestStair = null;

	public SpriteRenderer[] sprites;
	public Color stencilColor;

	public void Start ()
	{
		for (int i = 0; i < sprites.Length; i++)
		{
			var newMat = new Material (Shader.Find("Custom/CharacterShader"));
			newMat.SetColor ("_StencilColor", stencilColor);
			sprites [i].material = newMat;
		}
	}

	public void Update()
	{
		if (switchableObstacle != null) {
			if (isOnObstacle &&
				switchableObstacle.transform.position.z < owner.transform.position.z) {
				for (int i = 0; i < sprites.Length; i++) {
					sprites [i].material.SetFloat ("_Ref", 10f);
				}
			} else {
				for (int i = 0; i < sprites.Length; i++) {
					sprites [i].material.SetFloat ("_Ref", 9f);
				}
			}
		}
	}

	public void OnTriggerEnter(Collider col)
	{
		Debug.Log (col.name);
		if (col.CompareTag("Obstacle"))
		{
			obstacleList.Add (col.gameObject);
			isOnObstacle = true;
		}
		else if (col.CompareTag ("Stair"))
		{
			if (!stairList.Contains(col.gameObject))
				stairList.Add (col.gameObject);
		}
		else if (col.CompareTag ("InteractableObject"))
		{
			if (!interactableObject.Contains (col.gameObject))
				interactableObject.Add (col.gameObject);
		}
		else if (col.CompareTag ("CutSceneTrigger"))
		{
			if (gameObject.CompareTag ("Player")) {
				cutSceneTrigger = col;
				cutSceneTrigger.GetComponent<CutSceneEventTrigger> ().StartTrigger ();
			}
		}
		else if (col.CompareTag ("NPC"))
		{
			npcList.Add (col.gameObject);
		}
		else if (col.GetComponent<Collider>().CompareTag ("Enemy"))
		{
			enemyList.Add (col.GetComponent<Collider>().gameObject);
		}
		else if (col.CompareTag("SwitchableObstacle"))
		{
			switchableObstacle = col.gameObject;
			isOnObstacle = true;
		}
		else if(col.CompareTag("TeleportEntrance"))
		{
			teleportEntrance = col.gameObject;
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
		else if (col.CompareTag ("InteractableObject"))
		{
			if (!interactableObject.Contains (col.gameObject))
				interactableObject.Add (col.gameObject);
		}
		else if (col.CompareTag ("NPC"))
		{
			if (!npcList.Contains (col.gameObject))
			{
				npcList.Add (col.gameObject);
			}
		}
		else if (col.GetComponent<Collider>().CompareTag ("Enemy"))
		{
			if (!enemyList.Contains (col.GetComponent<Collider>().gameObject))
			{
				enemyList.Add (col.GetComponent<Collider>().gameObject);
			}
		}
		else if (col.CompareTag("SwitchableObstacle"))
		{
			switchableObstacle = col.gameObject;
		}
		else if(col.CompareTag("TeleportEntrance"))
		{
			teleportEntrance = col.gameObject;
		}
	}

	public void OnTriggerExit(Collider col)
	{
		if (col.CompareTag("Obstacle"))
		{
			obstacleList.Remove (col.gameObject);
			isOnObstacle = false;
		}
		else if (col.CompareTag("Stair"))
		{
			if (stairList.Contains(col.gameObject))
				stairList.Remove (col.gameObject);
		}
		else if (col.CompareTag ("InteractableObject"))
		{
			if (interactableObject.Contains (col.gameObject)) 
			{
				interactableObject.Remove (col.gameObject);
			}
		}
		else if (col.CompareTag ("NPC"))
		{
			if (npcList.Contains (col.gameObject))
			{
				npcList.Remove (col.gameObject);
			}
		}
		else if (col.GetComponent<Collider>().CompareTag ("Enemy"))
		{
			if (enemyList.Contains (col.gameObject))
			{
				enemyList.Remove (col.gameObject);
			}
		}
		else if (col.CompareTag("SwitchableObstacle"))
		{
			switchableObstacle = null;
			isOnObstacle = false;
		}
		else if(col.CompareTag("TeleportEntrance"))
		{
			teleportEntrance = null;
		}
	}

	public GameObject GetNearestEnemyObject ()
	{
		if (enemyList.Count != 0)
		{
			var tmpDis = Vector3.Distance (transform.position, enemyList [0].transform.position);
			var index = 0;
			for (int i = 0 ; i < enemyList.Count; i++)
			{
				var prevDis = tmpDis;
				tmpDis = Mathf.Min(tmpDis, Vector3.Distance (transform.position, enemyList[i].transform.position));

				if (prevDis != tmpDis)
					index = i;
			}
			return enemyList [index];
		}
		return null;
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
			for (int i = 0 ; i < stairList.Count; i++)
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

	public GameObject GetNearestNPCObject()
	{
		if (npcList.Count != 0)
		{
			var tmpDis = Vector3.Distance (transform.position, npcList [0].transform.position);
			var index = 0;
			for (int i = 0 ; i < npcList.Count; i++)
			{
				var prevDis = tmpDis;
				tmpDis = Mathf.Min(tmpDis, Vector3.Distance (transform.position, npcList[i].transform.position));

				if (prevDis != tmpDis)
					index = i;
			}
			nearestStair = npcList [index];
			return npcList [index];
		}
		return null;
	}
}
