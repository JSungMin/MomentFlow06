using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostingObject : MonoBehaviour {
	public SpriteRenderer ghostingTargetSprite;
	public Color startColor;
	public Color endColor;

	public float displayTime;
	private float displayedTime;
	private GhostingEffect spawner;

	
	// Update is called once per frame
	void Update () {
		displayedTime += Time.deltaTime;

		ghostingTargetSprite.color = Color.Lerp (startColor, endColor, displayedTime / displayTime);

		if (displayedTime >= displayTime)
		{
			spawner.RemoveTrailObject (gameObject);
			Destroy (gameObject);
		}
	}

	public void Init (float displayTime, Sprite sprite, GhostingEffect trail)
	{
		this.displayTime = displayTime;
		ghostingTargetSprite.sprite = sprite;
		displayedTime = 0;
		spawner = trail;
	}
}
