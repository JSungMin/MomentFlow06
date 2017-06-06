using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// damage 직접적인 처리를 여기서 함
// 직접 처리를 여기서 하는 것은 자연스러운 상황이 아님
public class InteractConditionChecker : ConditionChecker
{
    private const float damageEffectTm = 0.3f;

    private new void Awake()
    {
        base.Awake();
    }

    public void Damage(int quantity)
    {
        enemyInfo.Hp -= quantity;
        StartCoroutine(DamageEffectCo());
    }

    private IEnumerator DamageEffectCo()
    {
        for (int i = 0; i < spriteRenderers.Length; i++)
            spriteRenderers[i].color = Color.red;

        yield return new WaitForSeconds(damageEffectTm);

        for (int i = 0; i < spriteRenderers.Length; i++)
            spriteRenderers[i].color = Color.white;
    }
}