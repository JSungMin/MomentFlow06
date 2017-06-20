using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScatterSprite : MonoBehaviour {

	private SpriteRenderer spriteRenderer;
	private Material spriteMat;

	public Vector2 girdSize;
	private Vector2 spriteUv;

	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		spriteMat = new Material (spriteRenderer.material);
		spriteRenderer.material = spriteMat;
	}
	
	// Update is called once per frame
	void Update () {
		EditMaterial ();
		ApplyMaterial ();
	}
		
	public void EditMaterial ()
	{
		
	}

	public void ApplyMaterial ()
	{
		spriteRenderer.material = spriteMat;
	}
}
