﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInfo : HumanInfo {

	public OutsideInfo outsideInfo;
	public EquiptInfo equiptInfo;

	public Transform playerTransform;

    private Coroutine RecoveryManaCo;

    public float perManaInc = 1.0f;

    private void Awake()
    {
        mana = new Mana(10, 10.0f);
        RecoveryManaCo = StartCoroutine(mana.AddManaFor(perManaInc, 0.1f, false));
    }

    void Start () {
		//hp = 100;
		outsideInfo = GetComponent<OutsideInfo> ();
		equiptInfo = GetComponent<EquiptInfo> ();
		playerTransform = transform;
	}
}
