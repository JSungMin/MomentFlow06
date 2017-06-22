using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILockable {
	bool IsLocked {
		get;
		set;
	}

	ItemBase KeyObject {
		get;
		set;
	}

	bool TryToReleaseLock(ref List<ItemInfoStruct> pocketItems);
}
