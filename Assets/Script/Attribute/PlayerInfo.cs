using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour {

	public float hp;
	public OutsideInfo outsideInfo;
	public EquiptInfo equiptInfo;

	public Transform playerTransform;

	// Use this for initialization
	void Start () {
		hp = 100;
		outsideInfo = GetComponent<OutsideInfo> ();
		equiptInfo = GetComponent<EquiptInfo> ();
		playerTransform = transform;
	}
}
