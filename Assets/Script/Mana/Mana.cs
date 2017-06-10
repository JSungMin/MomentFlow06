using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Mana Controller
public class Mana : MonoBehaviour
{
    private const float maxManaPoint = 100.0f;
    private const float perIncManaPoint = 0.5f;

    private float manaPoint;
    public float ManaPoint
    {
        set
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

    private void Awake()
    {
        ManaPoint = 0.0f;
    }

    private void Update()
    {
        ManaPoint += perIncManaPoint;
    }
}