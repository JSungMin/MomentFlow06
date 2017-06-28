﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimLineRenderer : MonoBehaviour {
	public Material mat;
	public static AimLineRenderer instance;

	public List<Vector3> startPoint;
	public List<Vector3> endPoint;
	public List<float> pointAlpha;

	public void Awake ()
	{
		instance = this;
	}

	void OnPostRender()
	{
		if (!mat) {
			Debug.Log ("Mat is Null");
		}
		for (int i = 0; i < startPoint.Count; i++)
		{
			var point01 = Camera.main.WorldToScreenPoint (startPoint [i]);
			var point02 = Camera.main.WorldToScreenPoint (endPoint [i]);
			Debug.Log (point01.y + "  :  " + point02.y);
			GL.PushMatrix ();
			mat.SetPass (0);
			GL.LoadOrtho ();
			GL.Begin (GL.LINES);
			GL.Color (new Color (1 ,0 ,0 ,pointAlpha[i]));
			GL.Vertex (new Vector3 (point01.x/Screen.width, point01.y / Screen.height));
			var dir = (point02 - point01).normalized;
			GL.Vertex (new Vector3 (point01.x/Screen.width, point01.y / Screen.height) + dir * 10);
			GL.End ();
			GL.PopMatrix ();
		}

		startPoint.Clear ();
		endPoint.Clear ();
		pointAlpha.Clear ();
	}
}
