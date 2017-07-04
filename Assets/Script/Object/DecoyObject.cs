using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecoyObject : FieldBase {
	public DynamicObject dynamicObject;

	// Use this for initialization
	void Start () {
		playTime = 0;
		dynamicObject = GetComponent<DynamicObject> ();
		name = name + Random.Range (0, 10000);
	}
	
	// Update is called once per frame
	void Update () {
		CalculateRect ();
		DoActionForEachObject ();

		if (CheckDestroy())
		{
			DoDestroy ();
		}
		else
			playTime += dynamicObject.customDeltaTime;
	}

	protected override void DoActionForEachObject ()
	{
		RenderRect ();

		var enemies = BoxCastAllInRect ();
		Debug.Log (enemies.Length);

		for (int i = 0; i < enemies.Length; i++)
		{
			var enemyInfo = enemies [i].collider.GetComponentInParent <EnemyInfo> ();
			enemyInfo.attackTarget = gameObject;
			enemyInfo.enemyAction.detectedTarget = gameObject;
		}
	}
		
	protected override bool CheckDestroy() 
	{
		if (playTime >= lifeTime)
		{
			return true;
		}
		return false;
	}
	float destroyTimer = 0;
	protected override void DoDestroy ()
	{
		if (destroyTimer <= 1f) {
			GetComponent<_2dxFX_Hologram2> ()._Alpha = Mathf.Lerp (destroyTimer, 1f, Time.deltaTime);
			destroyTimer += dynamicObject.customDeltaTime;
		} else {
			Destroy (this.gameObject);
		}
	}
}
