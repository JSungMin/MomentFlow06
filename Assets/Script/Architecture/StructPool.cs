using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ItemInfoStruct
{
	public ItemType itemType;
	public int itemId;

	public ItemInfoStruct (ItemType it, int id)
	{
		itemType = it;
		itemId = id;
	}
}

[System.Serializable]
public struct ObjectExplainUIPoints
{
	public bool isRendered;
	public List<Vector3> points;
}

public class StructPool {
}
