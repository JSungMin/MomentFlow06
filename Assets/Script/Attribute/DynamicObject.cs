using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicObject : MonoBehaviour {
	public Transform rootTransform;

	public bool isImmunity;

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

		SetDynamicComponents ();
	}

	public void OnEnable()
	{
		if (!TimeManager.GetInstance().dynamicObjectList.Contains(this))
		{
			TimeManager.GetInstance ().dynamicObjectList.Add (this);
		}
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
		if (previousTimeScaleList.Count != 0) {
			customTimeScale = previousTimeScaleList [previousTimeScaleList.Count - 1];
			previousTimeScaleList.RemoveAt (previousTimeScaleList.Count - 1);
		} else
			customTimeScale = 1f;
		AffectCustomTimeScale ();
	}

	private void SetDynamicComponents ()
	{
		animators = rootTransform.GetComponentsInChildren<Animator> ();
		particles = rootTransform.GetComponentsInChildren<ParticleSystem> ();
	}

	private void AffectCustomTimeScale ()
	{
		if (null != animators) {
			for (int i = 0; i < animators.Length; i++)
			{
				animators [i].speed = customTimeScale;
			}
		}
		if (null != particles) {
			for (int i = 0; i < particles.Length; i++)
			{
				var ma = particles [i].main;
				ma.simulationSpeed = customTimeScale;
			}
		}
	}

	public bool IsUpdateable ()
	{
		if (isImmunity || customTimeScale != 0)
		{
			return true;
		}
		return false;
	}
}
