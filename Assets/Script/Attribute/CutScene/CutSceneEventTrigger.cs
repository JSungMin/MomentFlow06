using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CutSceneEventTrigger : MonoBehaviour {
	public List<int> unitTrackIndex;
	public List<CutSceneUnit> affectedUnitList;

	public bool isUsed = false;

	public void StartTrigger()
	{
		if (!isUsed) {
			for (int i = 0 ; i < affectedUnitList.Count; i++)
			{
				affectedUnitList [i].ChangeTrack (unitTrackIndex [i]);
				affectedUnitList [i].isAction = true;
			}
			isUsed = true;
		}
	}
}
