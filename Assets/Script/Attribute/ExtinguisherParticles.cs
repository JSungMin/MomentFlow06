using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtinguisherParticles : MonoBehaviour {
	private ParticleSystem thisParticleSystem;
	private ParticleSystem.Particle[] emittedParticle;

	public Extinguisher extinguisher;
	private TimeRecallable extinguisherTimeRecallInfo;

	private Vector3 originPosition;

	// Use this for initialization
	void Start () {
		thisParticleSystem = GetComponent<ParticleSystem> ();
		extinguisher = GetComponentInParent<Extinguisher> ();
		extinguisherTimeRecallInfo = GetComponentInParent<TimeRecallable> ();
		originPosition = extinguisher.transform.position;
	}
	bool revertStart = false;
	// Update is called once per frame
	void Update () {
		emittedParticle = new ParticleSystem.Particle[thisParticleSystem.particleCount];

		if (extinguisher.isExplosing)
		{
			var particleLength = thisParticleSystem.GetParticles (emittedParticle);

			if (!extinguisherTimeRecallInfo.isReverting && !revertStart) {
				timer = 0;
				if (!thisParticleSystem.isPlaying)
					thisParticleSystem.Play ();
				EditParticles ();
			} 
			else {
				if (!revertStart) {

					thisParticleSystem.Pause ();
					tmpEmittedParticles = new ParticleSystem.Particle[emittedParticle.Length];
					tmpEmittedParticles = emittedParticle;
					revertStart = true;
				}
			}

			if (revertStart) {
				ParticesReturnToObject ();
			}

			thisParticleSystem.SetParticles (emittedParticle, particleLength);
		}
	}

	void EditParticles()
	{
		for (int i = 0; i < emittedParticle.Length; i++)
		{
			emittedParticle [i].remainingLifetime += 2 * Time.deltaTime;
			emittedParticle [i].velocity = Vector3.Lerp (emittedParticle [i].velocity, Vector3.zero, Time.deltaTime * 5);
		}
	}

	float timer = 0;
	int index = 0;

	float duration = 0.05f;

	private ParticleSystem.Particle[] tmpEmittedParticles;

	void ParticesReturnToObject ()
	{
		thisParticleSystem.Pause ();
		timer = Mathf.Clamp(timer + Time.deltaTime,0,duration);
		var x = 16;
		if (timer < duration) {
			for (int i = 1; i <= x; i++) {
				emittedParticle [Mathf.Max(0,tmpEmittedParticles.Length - (index + i))].position = Vector3.Lerp (tmpEmittedParticles [Mathf.Max(0,tmpEmittedParticles.Length - (index + i))].position, extinguisher.transform.position, timer / duration); 
			}
		}
		else {
			Debug.Log ("Return");
			for (int i = 1; i <= x; i++) {
				emittedParticle [Mathf.Max (0, tmpEmittedParticles.Length - (index + i))].remainingLifetime = 0; 
			}
			timer = 0;
			index += x - 1;
		}
	}
}
