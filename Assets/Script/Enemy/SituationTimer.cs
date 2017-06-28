using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SituationTimer
{
    private EnemyInfo enemyInfo;
    private float timer;
    private Coroutine SituationDecreaseTimeCo = null;

    public SituationTimer(EnemyInfo enemyInfo)
    {
        this.enemyInfo = enemyInfo;
    }

    public bool IsInSituation()
    {
        return timer > 0.0f;
    }

    public void GetInSituation(float timeFor)
    {
        if (SituationDecreaseTimeCo != null)
            enemyInfo.StopCoroutine(SituationDecreaseTimeCo);
        timer = timeFor;
        SituationDecreaseTimeCo = enemyInfo.StartCoroutine(DecreaseSituaionTimeCo());
    }

    private IEnumerator DecreaseSituaionTimeCo()
    {
        float deltaTm = 0.02f;
        while (timer > 0)
        {
            Debug.Log(timer);
            timer -= deltaTm;
            yield return new WaitForSeconds(deltaTm);
            yield return enemyInfo.StartCoroutine(TimeManager.GetInstance().IsTimePausedCo());
        }
        timer = 0.0f;
    }
}
