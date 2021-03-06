using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneShake : MonoBehaviour {

	public bool constantShaking = false;

	public void ConstantShake()
	{
		var constantShakePreset = Com.LuisPedroFonseca.ProCamera2D.ProCamera2DShake.Instance.ConstantShakePresets [2];
		if (!constantShaking) {
			constantShaking = true;
			
			Com.LuisPedroFonseca.ProCamera2D.ProCamera2DShake.Instance.ConstantShake (constantShakePreset);
		} else {
			constantShaking = false;

			Com.LuisPedroFonseca.ProCamera2D.ProCamera2DShake.Instance.ConstantShake(constantShakePreset);
		}
	}

	public void InstanceShake()
	{
		var shakePreset =  Com.LuisPedroFonseca.ProCamera2D.ProCamera2DShake.Instance.ShakePresets[1];

		Com.LuisPedroFonseca.ProCamera2D.ProCamera2DShake.Instance.Shake(shakePreset);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
