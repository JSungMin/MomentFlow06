using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITimeRecallableForInteractable {
	bool IsChangeable {
		get;
		set;
	}
	float ConsumeAmount{
		get;
		set;
	}
		
	bool TryRecall (GameObject challenger);
	bool IsSatisfied (GameObject challenger);
	void DoRecall (GameObject challenger);
}
