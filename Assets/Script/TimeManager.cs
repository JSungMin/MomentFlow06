using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float customDeltaTime { private set; get; }
	public float customTimeScale;

	public static bool isTimePaused = false;

    private void Update()
    {
		customDeltaTime =  Time.deltaTime * customTimeScale;
    }

    public void SetTimeScale(float scale)
    {
        customTimeScale = scale;
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
