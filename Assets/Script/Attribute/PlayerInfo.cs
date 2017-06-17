using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : HumanInfo {

	public OutsideInfo outsideInfo;
	public EquiptInfo equiptInfo;

	public Transform playerTransform;

    public Mana mana { private set; get; }
    private Coroutine RecoveryManaCo;

    public float perManaInc = 1.0f;

    private void Awake()
    {
        mana = new Mana(100.0f);
        RecoveryManaCo = StartCoroutine(mana.RecoveryMana(perManaInc, 0.5f));
    }

    void Start () {
		hp = 100;
		outsideInfo = GetComponent<OutsideInfo> ();
		equiptInfo = GetComponent<EquiptInfo> ();
		playerTransform = transform;
	}
}
