using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StencilScript : MonoBehaviour {

	public float stencilRef;
	public SpriteRenderer[] sprites;
	public Color stencilColor;

	private List<Material> previousMat;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < sprites.Length; i++)
		{
			previousMat.Add (sprites[i].material);
		}

		for (int i = 0; i < sprites.Length; i++)
		{
			var newMat = new Material (Shader.Find("Custom/CharacterShader"));
			newMat.SetColor ("_StencilColor", stencilColor);
			newMat.SetFloat ("_Ref", stencilRef);
			sprites [i].material = newMat;
		}
	}

	public void SetStencilRef (float refe)
	{
		stencilRef = refe;
		for (int i = 0; i < sprites.Length; i++) {
			sprites [i].material.SetFloat ("_Ref", refe);
		}
	}
}
