using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTimer : MonoBehaviour {
	private ParticleSystem ps;
	private float timer = 0;
	// Use this for initialization
	void Start () {
		ps = GetComponent<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () {
		var ma = ps.main;
		ma.simulationSpeed = TimeManager.GetInstance ().customTimeScale;
	}
}
