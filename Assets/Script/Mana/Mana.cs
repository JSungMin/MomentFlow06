﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Mana Controller
public class Mana
{
    public Mana(float maxManaPoint)
    {
        this.maxManaPoint = maxManaPoint;
    }

    public IEnumerator AddManaFor(float perIncManaPoint, float incDeltaTm, bool isTimePauseStillUse)
    {
        while(true)
        {
            yield return new WaitForSeconds(incDeltaTm);
            if (!isTimePauseStillUse)
                yield return TimeManager.GetInstance().StartCoroutine(TimeManager.GetInstance().IsTimePausedCo());
            ManaPoint += perIncManaPoint;
        }
    }

    public void AddMana(float manaPoint)
    {
        this.ManaPoint += manaPoint;
    }

    private float maxManaPoint;
    private float manaPoint;
    public float ManaPoint
    {
        private set
        {
            manaPoint = value;
            if (manaPoint > maxManaPoint)
            {
                manaPoint = maxManaPoint;
            }
            else if (manaPoint <= 0.0f)
                manaPoint = 0.0f;
        }

        get
        {
            return manaPoint;
        }
    }
}