using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : ItemBase {

	public int hp = 1;

	public void Start()
	{
		base.Start ();
	}

	public bool TryUseKey ()
	{
		if (hp > 0) {
			UseKey ();
			return true;
		}
		else {
			DropItem ();
			return false;
		}
	}

	private void UseKey()
	{
		hp -= 1;
	}

}
