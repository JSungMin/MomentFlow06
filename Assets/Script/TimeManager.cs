using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float customDeltaTime { private set; get; }
	public float customTimeScale;

	public static bool isTimeSlowed;

	public static bool isTimePaused = false;

    private void Update()
    {
		customDeltaTime =  Time.deltaTime * customTimeScale;
    }


    public void SetCustomTimeScale(float scale)
    {
        customTimeScale = scale;
		AffectCustomTimeScaleToAllAnimator ();
    }

	public void TimePause ()
	{
		isTimePaused = true;
		customTimeScale = 0;
		AffectCustomTimeScaleToAllAnimator ();
	}

	public void TimeSlowDown (float timeScale)
	{
		Time.timeScale = timeScale;
		customTimeScale = timeScale;

		AffectTimeScaleToAllAnimator ();

		isTimeSlowed = true;
	}
	// Time Pause의 Nagative
	public void TimeResume ()
	{
		customTimeScale = 1;
		AffectCustomTimeScaleToAllAnimator ();
		isTimePaused = false;
	}
	// TimeSlowDown의 Nagative
	public void TimeNormalize ()
	{
		Time.timeScale = 1.0f;
		customTimeScale = 1;

		AffectTimeScaleToAllAnimator ();

		isTimeSlowed = false;
	}

	private void AffectCustomTimeScaleToAllAnimator ()
	{
		var animators = GameObject.FindObjectsOfType <Animator> ();
		for (int i = 0; i < animators.Length; i++)
		{
			animators [i].speed = customTimeScale;
		}
	}

	private void AffectTimeScaleToAllAnimator ()
	{
		var animators = GameObject.FindObjectsOfType <Animator> ();
		for (int i = 0; i < animators.Length; i++)
		{
			animators [i].speed = Time.timeScale;
		}
	}

    public bool IsTimePaused()
    {
        return customTimeScale == 0.0f;
    }

    public IEnumerator IsTimePausedCo()
    {
        while (IsTimePaused())
            yield return new WaitForSeconds(0.02f);
    }

    /// single ton /////////////////////////////
    private static TimeManager instance;
    private static GameObject container;

    public static TimeManager GetInstance()
    {
        if(!instance)
        {
            container = new GameObject();
            container.name = "TimeManager";
            instance = container.AddComponent<TimeManager>();
			instance.customTimeScale = 1;
        }
        return instance;
    }
    ///////////////////////////////////////////
}
