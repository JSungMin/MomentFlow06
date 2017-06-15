using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILockable {
	bool IsLocked {
		get;
		set;
	}

	GameObject KeyObject {
		get;
		set;
	}

	bool TryToReleaseLock(ref List<GameObject> pocketItems);
}
