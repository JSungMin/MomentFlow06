using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightColor : MonoBehaviour {
	public bool renderOnce;

	public Color lightColor;
	public float lightColorRadius = 1.5f;
	public float contrastFactor = 1;
	public float colorFactor = 0;
	public float intencityVariationFactor = 0.5f;
	public float particleFactor = 0;
	public float flickerPeroid = 1;

	void Awake ()
	{
		GetComponent<DynamicLight2D.DynamicLight> ().lightColor = lightColor;
	}
		

	// Update is called once per frame
	void Update () {
		if (!renderOnce)
		{
			GetComponent<MeshCollider> ().sharedMesh = GetComponent<MeshFilter> ().sharedMesh;
		}

		GetComponent<DynamicLight2D.DynamicLight> ().lightMaterial.SetFloat ("_Radius",lightColorRadius);
		GetComponent<DynamicLight2D.DynamicLight> ().lightMaterial.SetFloat ("_ContrastFactor",contrastFactor);
		GetComponent<DynamicLight2D.DynamicLight> ().lightMaterial.SetFloat ("_ColorFactor",colorFactor);
		GetComponent<DynamicLight2D.DynamicLight> ().lightMaterial.SetFloat ("_IntensityFactor",intencityVariationFactor);
		GetComponent<DynamicLight2D.DynamicLight> ().lightMaterial.SetFloat ("_ParticleFactor",particleFactor);
		GetComponent<DynamicLight2D.DynamicLight> ().lightMaterial.SetFloat ("_FlickerPeroid",flickerPeroid);
	}
}
