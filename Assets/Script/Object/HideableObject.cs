using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideableObject : InteractableObject {

	public GameObject hidedObject;

	private float hidedObjectPreviousZ;

	public bool isFromLeftShadow;

	public float toZ;

	public void Update()
	{
		if (null != hidedObject)
		{
			if (isFromLeftShadow) {
				hidedObject.transform.localScale = new Vector3 (-1, 1, 1);
			} else {
				hidedObject.transform.localScale = new Vector3 (1,1,1);
			}
			hidedObject.transform.position = new Vector3 (transform.position.x, hidedObject.transform.position.y, toZ);
		}
	}

	public void HideChallenger (GameObject challenger)
	{
		hidedObject = challenger;
		hidedObject.GetComponent<HumanInfo> ().isHided = true;
		hidedObject.GetComponentInChildren<Animator> ().SetTrigger ("TriggerHide");

		if (hidedObject.CompareTag ("Player")) {
			hidedObjectPreviousZ = hidedObject.transform.position.z;

			hidedObject.GetComponentInChildren<Animator> ().GetBehaviour<PlayerHideState> ().hideFrom = this;
		}
		isInteracted = true;
	}

	public void OutChallenger ()
	{
		if (null != hidedObject)
		{
			Debug.Log ("OutChallenger");
			hidedObject.GetComponent <HumanInfo> ().isHided = false;
			hidedObject.GetComponentInChildren<Animator> ().SetTrigger ("TriggerHideEnd");
			hidedObject.transform.position = new Vector3 (
				hidedObject.transform.position.x,
				hidedObject.transform.position.y,
				hidedObjectPreviousZ
			);
			hidedObject = null;
		}
		isInteracted = false;
	}

	public static HideableObject FindHidedObject (GameObject obj)
	{
		var hideableObjs = GameObject.FindObjectsOfType<HideableObject> ();

		for (int i = 0; i < hideableObjs.Length; i++)
		{
			if (hideableObjs[i].hidedObject == obj)
			{
				return hideableObjs [i];
			}
		}
		return null;
	}

	#region implemented abstract members of InteractableObject

	public override bool TryInteract (GameObject challenger)
	{
		if (challenger.CompareTag ("Player")) {
			if (!isInteracted) {
				HideChallenger (challenger);
			} else {
				OutChallenger ();
			}
		}
		else {
			OutChallenger ();
		}
		return true;
	}

	#endregion
}
