using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicObject : MonoBehaviour {
	public Transform rootTransform;

	private Animator[] animators;
	private ParticleSystem[] particles;

	public float customTimeScale;
	public float customDeltaTime;

	public List<float> previousTimeScaleList;

	// Use this for initialization
	void Start () {
		if (null == rootTransform)
		{
			rootTransform = transform;
		}
		customTimeScale = 1;
		TimeManager.GetInstance ().dynamicObjectList.Add (this);

		SetDynamicComponents ();
	}

	public void OnDisable()
	{
		TimeManager.GetInstance ().dynamicObjectList.Remove (this);
	}

	// Update is called once per frame
	void Update () {
		customDeltaTime = Time.unscaledDeltaTime * customTimeScale;
	}

	public void ChangeTimeScale (float timeScale)
	{
		previousTimeScaleList.Add (customTimeScale);
		customTimeScale = timeScale;
		AffectCustomTimeScale ();
	}

	public void BackToPreviousTimeScale ()
	{
		customTimeScale = previousTimeScaleList [previousTimeScaleList.Count - 1];
		previousTimeScaleList.RemoveAt (previousTimeScaleList.Count - 1);
		AffectCustomTimeScale ();
	}

	private void SetDynamicComponents ()
	{
		animators = rootTransform.GetComponentsInChildren<Animator> ();
		particles = rootTransform.GetComponentsInChildren <ParticleSystem> ();
	}

	private void AffectCustomTimeScale ()
	{
		for (int i = 0; i < animators.Length; i++)
		{
			animators [i].speed = customTimeScale;
		}

		for (int i = 0; i < particles.Length; i++)
		{
			var ma = particles [i].main;
			ma.simulationSpeed = customTimeScale;
		}
	}
}
