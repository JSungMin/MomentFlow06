using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
	public static bool isTimeSlowed;

	public static bool isTimePaused = false;

	public List<DynamicObject> dynamicObjectList = new List<DynamicObject>();

	public void SetCustomTimeScale (DynamicObject obj, float timeScale)
	{
		obj.ChangeTimeScale (timeScale);
	}

	public void TimePause ()
	{
		isTimePaused = true;

		dynamicObjectList.ForEach (delegate (DynamicObject obj) 
		{
			obj.ChangeTimeScale (0);
		});
	}

	public void TimeSlowDown (float timeScale)
	{
		isTimeSlowed = true;
		Time.timeScale = timeScale;

		dynamicObjectList.ForEach (delegate (DynamicObject obj) 
		{
				obj.ChangeTimeScale (obj.customTimeScale * 0.25f);
		});

	}
	// Time Pause의 Nagative
	public void TimeResume ()
	{
		dynamicObjectList.ForEach (delegate (DynamicObject obj) 
		{
			obj.BackToPreviousTimeScale();
		});
		isTimePaused = false;
	}
	// TimeSlowDown의 Nagative
	public void TimeNormalize ()
	{
		Time.timeScale = 1.0f;
		dynamicObjectList.ForEach (delegate (DynamicObject obj) 
		{
				obj.BackToPreviousTimeScale();
		});

		AffectTimeScaleToAllAnimator ();

		isTimeSlowed = false;
	}

	private void AffectTimeScaleToAllAnimator ()
	{
		var animators = GameObject.FindObjectsOfType <Animator> ();
		for (int i = 0; i < animators.Length; i++)
		{
			animators [i].speed = Time.timeScale;
		}
	}
		
    public IEnumerator IsTimePausedCo()
    {
		while (isTimePaused)
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

        }
        return instance;
    }
    ///////////////////////////////////////////
}
