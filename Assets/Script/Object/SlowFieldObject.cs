using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowFieldObject : FieldBase {
	public _2dxFX_DestroyedFX effect;

	private bool isActivated = false;

	// Use this for initialization
	new void Start () {
		base.Start ();
		if (null == effect)
			effect = GetComponent<_2dxFX_DestroyedFX> ();
		isActivated = true;
		GetComponent<SpriteRenderer> ().color = new Color (0.384f,0.643f,1f,0.392f);
		effect.Destroyed = 1f;
	}
	
	// Update is called once per frame
	void Update () {
		if (isActivated) {
			effect.Destroyed = Mathf.Clamp (effect.Destroyed - Time.deltaTime, 0f, 1f);
		} else {
			effect.Destroyed = Mathf.Clamp (effect.Destroyed + Time.deltaTime, 0f, 1f);
		}
		CalculateRect ();
		RenderRect ();
		DeleteOutOfRectDynamicObject ();
		DoActionForEachObject ();

		if (CheckDestroy ()) {
			DoDestroy ();
		} else
			playTime += Time.unscaledDeltaTime * reductionAmount;
	}

	protected override void DoActionForEachObject()
	{
		var objects = FindDynamicObjectsInRect ();
		Debug.Log (objects.Length);
		for (int i = 0; i < objects.Length; i++)
		{
			Debug.Log (objects[i].name);
			if (affectedList.Contains (objects [i].gameObject)) {
				Debug.Log (objects[i].name + " is contains");
				continue;
			}
			
			var col = objects [i];
			if (null != col) {
				col.ChangeTimeScale (col.customTimeScale * reductionAmount);
				affectedList.Add (objects [i].gameObject);
			}
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
	protected override void DoDestroy ()
	{
		isActivated = false;
		if (effect.Destroyed == 1)
		{
			for (int i = 0; i < affectedList.Count; i++)
			{
				affectedList [i].GetComponent<DynamicObject> ().BackToPreviousTimeScale ();
			}
			Destroy (this.gameObject);
		}
	}
}
