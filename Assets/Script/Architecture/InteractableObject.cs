using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour {
	public bool isInteracted;

	public bool isConnected = false;
	public InteractableObjectType objectType;
	public List<GameObject> willBeAffectedObjectList;

	public delegate void DoInteractActions();
	public delegate void CancelInteractActions ();

	public DoInteractActions doInteractActions;
	public CancelInteractActions cancelInteractActions;

	public abstract bool TryInteract (GameObject challenger);
}
