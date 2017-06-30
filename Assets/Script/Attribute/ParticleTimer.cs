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
}
