using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader {
	public static string GetSaveKey (string name, string type)
	{
		return SavePoint.sceneName + ":" + name + ":" + type;
	}

	public static Transform LoadTransform (string name, Transform targetTrans)
	{
		var data = PlayerPrefs.GetString (GetSaveKey (name, typeof(Transform).Name));
		string[] sData = data.Split ("," [0]);

		targetTrans.position = new Vector3 (float.Parse(sData[0]),float.Parse(sData[1]),float.Parse(sData[2]));
		targetTrans.localRotation = Quaternion.Euler (new Vector3 (float.Parse(sData[3]),float.Parse(sData[4]),float.Parse(sData[5])));
		targetTrans.localScale = new Vector3 (float.Parse(sData[6]),float.Parse(sData[7]),float.Parse(sData[8]));
		return targetTrans;
	}

	public static int LoadInt (string name, string typeTag)
	{
		return PlayerPrefs.GetInt(GetSaveKey(name,typeTag));
	}

	public static float LoadFloat (string name, string typeTag)
	{
		return PlayerPrefs.GetFloat(GetSaveKey(name,typeTag));
	}

	public static bool LoadSpriteEnabled(string name)
	{
		var data = PlayerPrefs.GetString (GetSaveKey (name,typeof(SpriteRenderer).Name));
		if (bool.TrueString  == data) {
			return true;
		}
		return false;
	}
}
