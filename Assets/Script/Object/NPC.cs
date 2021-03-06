using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

	public int tryCount;
	public List<NPCAction> actions;

	public void Start()
	{
		if (actions.Count == 0)
		{
			var acts = transform.GetComponentsInChildren<NPCAction> ();
			for (int i = 0; i < acts.Length; i++)
			{
				actions.Add (acts[i]);
			}
		}
	}

	public void Interact (GameObject challenger)
	{
		for (int i = 0; i < actions.Count; i++)
		{
			actions [i].TryAction (ref tryCount);
		}
	}
}
