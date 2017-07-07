using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : ItemBase {


	public void Start()
	{
		base.Start ();
	}

	public bool TryUseKey ()
	{
		return true;
	}
}
