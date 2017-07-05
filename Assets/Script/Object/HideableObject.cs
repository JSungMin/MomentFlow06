using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideableObject : InteractableObject {

	public GameObject hidedObject;

	public void Update()
	{
		if (null != hidedObject)
		{
			hidedObject.transform.position = new Vector3 (transform.position.x, hidedObject.transform.position.y, transform.position.z);
		}
	}

	public void HideChallenger (GameObject challenger)
	{
		hidedObject = challenger;
		hidedObject.GetComponent<HumanInfo> ().isHided = true;
		hidedObject.GetComponentInChildren<Animator> ().SetTrigger ("TriggerHide");

		if (hidedObject.CompareTag ("Player")) {
			hidedObject.GetComponentInChildren<Animator> ().GetBehaviour<PlayerHideState> ().hideFrom = this;
		}
		isInteracted = true;
	}

	public void OutChallenger ()
	{
		if (null != hidedObject)
		{
			hidedObject.GetComponent<HumanInfo> ().isHided = false;
			hidedObject.GetComponentInChildren<Animator> ().SetTrigger ("TriggerHideEnd");
			hidedObject = null;
		}
		isInteracted = false;
	}

	#region implemented abstract members of InteractableObject

	public override bool TryInteract (GameObject challenger)
	{
		if (!isInteracted) {
			HideChallenger (challenger);
		}
		else {
			OutChallenger ();
		}
		return true;
	}

	#endregion
}
