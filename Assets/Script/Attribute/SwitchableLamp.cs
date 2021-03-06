using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchableLamp : MonoBehaviour {
	[SerializeField]
	private bool isSwitchOn = true;
	public SpriteRenderer[] lampSprites;
	public List<float> lampsBrightnessList;

	public float turnOffSpeed = 1;
	public float turnOnSpeed = 1;

	public void SetSwtichState(bool value)
	{
		isSwitchOn = value;
	}

	public bool GetSwtichState()
	{
		return isSwitchOn;
	}

	public void Start()
	{
		for (int i = 0 ; i < lampSprites.Length; i++)
		{
			var prevMat = lampSprites [i].material;
			var newMat = new Material (prevMat);
			lampSprites [i].material = newMat;
		}

		if (!isSwitchOn)
		{
			for (int i = 0; i < lampSprites.Length; i++)
			{
				lampSprites [i].material.SetFloat ("_Radius",0);
			}
		}
	}

	public void Update()
	{
		if (isSwitchOn) {
			for (int i = 0; i < lampSprites.Length; i++) {
				var nowLightValue = lampSprites [i].material.GetFloat ("_Radius");
				lampSprites [i].material.SetFloat ("_Radius", Mathf.Lerp (nowLightValue, lampsBrightnessList [i], Time.deltaTime * turnOnSpeed));
			}
		}
		else {
			for (int i = 0; i < lampSprites.Length; i++) {
				var nowLightValue = lampSprites [i].material.GetFloat ("_Radius");
				lampSprites [i].material.SetFloat ("_Radius", Mathf.Lerp (nowLightValue, 0, Time.deltaTime * turnOffSpeed));
			}
		}
	}
}
