using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostingObject : MonoBehaviour {
	public SpriteRenderer ghostingTargetSprite;
	public _2dxFX_PixelDie pixelEffect;
	public Color startColor;
	public Color endColor;

	public float displayTime;
	private float displayedTime;
	private GhostingEffect spawner;

	public void Start ()
	{
		pixelEffect = GetComponent<_2dxFX_PixelDie> ();
	}
	
	// Update is called once per frame
	void Update () {
		displayedTime += Time.deltaTime;

		ghostingTargetSprite.color = Color.Lerp (startColor, endColor, displayedTime / displayTime);
		pixelEffect._Value1 = Mathf.Lerp (0.5f, 1f, displayedTime);
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
