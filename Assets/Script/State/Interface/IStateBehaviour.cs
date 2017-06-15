using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IStateBehaviour : StateMachineBehaviour
{
    protected SpriteRenderer shoulderSpriteRenderer;
    protected EnemyInfo enemyInfo;

    public void LoadData(SpriteRenderer shoulderSpriteRenderer, EnemyInfo enemyInfo)
    {
        this.shoulderSpriteRenderer = shoulderSpriteRenderer;
        this.enemyInfo = enemyInfo;
    }
}
