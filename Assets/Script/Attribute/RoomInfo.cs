using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInfo : MonoBehaviour {
	public string roomName;
	[HideInInspector]
	public BoxCollider roomCollider;
	public List<EnemyInfo> enemyListInRoom;
	public List<InteractableObject> interactObjectListInRoom;

	public void Start ()
	{
		roomCollider = GetComponent<BoxCollider> ();

		StartCoroutine (OnRectCheck ());
	}

	public IEnumerator OnRectCheck ()
	{
		while (true) {
			yield return new WaitForSeconds (0.2f);
			interactObjectListInRoom.Clear ();

			var objs = Physics.OverlapBox (transform.position + roomCollider.center, roomCollider.bounds.extents, Quaternion.identity, 1<<LayerMask.NameToLayer ("Collision"));

			for (int i = 0; i < objs.Length; i++)
			{
				var obj = objs [i].GetComponent<InteractableObject> ();
				if (null != obj)
					interactObjectListInRoom.Add (obj);
			}
		}
	}

	public void OnTriggerEnter (Collider col)
	{
		if (null != col.GetComponentInParent<EnemyInfo>())
		{
			var obj = col.GetComponentInParent<EnemyInfo> ();
			if (!enemyListInRoom.Contains (obj))
			{
				enemyListInRoom.Add (obj);
			}
		}
	}

	public void OnTriggerStay (Collider col)
	{
		if (null != col.GetComponentInParent<EnemyInfo>())
		{
			var obj = col.GetComponentInParent<EnemyInfo> ();
			if (!enemyListInRoom.Contains (obj))
			{
				enemyListInRoom.Add (obj);
			}
		}
	}

	public void OnTriggerExit (Collider col)
	{
		if (null != col.GetComponentInParent<EnemyInfo>())
		{
			var obj = col.GetComponentInParent<EnemyInfo> ();
			if (enemyListInRoom.Contains (obj))
			{
				enemyListInRoom.Remove (obj);
			}
		}
	}

	public static RoomInfo FindRoomWithName (string roomName)
	{
		var rooms = GameObject.FindObjectsOfType<RoomInfo> ();

		for (int i = 0; i < rooms.Length; i++)
		{
			if (rooms [i].roomName == roomName)
			{
				return rooms [i];
			}
		}
		return null;
	}

	public static RoomInfo FindRoomWithPoint (Vector3 point)
	{
		var rooms = GameObject.FindObjectsOfType<RoomInfo> ();

		for (int i = 0; i < rooms.Length; i++)
		{
			var roomRect = rooms [i].roomCollider;
			if (roomRect.bounds.Contains (point))
			{
				return rooms [i];
			}
		}
		return null;
	}
}
