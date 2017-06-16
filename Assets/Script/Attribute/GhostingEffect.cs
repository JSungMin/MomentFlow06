using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostingEffect : MonoBehaviour {

	public SpriteRenderer ghostingTargetSprite;

	public int segments;
	public float trailTime;
	public GameObject trailObject;

	public float spawnInterval;
	public float spawnTimer;
	private bool mbEnabled;

	private List<GameObject> trailObjects;

	// Use this for initialization
	void Start () {
		trailObjects = new List<GameObject> ();
		mbEnabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (mbEnabled)
		{
			spawnTimer += Time.deltaTime;

			if (spawnTimer >= spawnInterval)
			{
				GameObject trail = GameObject.Instantiate (trailObject);
				GhostingObject tmpTrailObject = trail.GetComponent<GhostingObject> ();

				tmpTrailObject.Init (trailTime, ghostingTargetSprite.sprite, this);
				trail.transform.position = transform.position;
				trail.transform.localScale = ghostingTargetSprite.transform.parent.localScale;
				trailObjects.Add (trail);

				spawnTimer = 0;
			}
		}
	}

	public void RemoveTrailObject(GameObject obj)
	{
		trailObjects.Remove (obj);
	}

	public void SetEnabled (bool enabled)
	{
		mbEnabled = enabled;

		if (enabled)
		{
			spawnTimer = spawnInterval;
		}
	}
}
