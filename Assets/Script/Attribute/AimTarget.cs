using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AimTarget : MonoBehaviour {
	public bool isActive = false;
	public bool hideShoulder = false;
	private Animator animator;
	public Transform shoulder;

	public float maxAngle;
	public float minAngle;

	public float defaultAngle = 45f;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		if (null == animator)
			animator = transform.GetComponentInChildren<Animator> ();
	}

	public void SetDirection(bool toLeft)
	{
		if (toLeft) {
			transform.localScale = new Vector3 (-1, 1, 1);
		} else {
			transform.localScale = new Vector3 (1, 1 ,1);
		}
	}

	public void AimToObject(Vector3 targetPos)
	{
		float dis_x;
		float dis_y;
		float degree;

		if (targetPos.x > transform.position.x)
		{
			dis_x = (shoulder.transform.position.x - targetPos.x);
			dis_y = targetPos.y - shoulder.transform.position.y;
			degree = Mathf.Atan2(dis_x, -dis_y) * Mathf.Rad2Deg;
			SetDirection (true);
		}
		else
		{
			dis_x = -(shoulder.transform.position.x - targetPos.x);
			dis_y = targetPos.y - shoulder.transform.position.y;
			degree = Mathf.Atan2(dis_x, -dis_y) * Mathf.Rad2Deg;
			SetDirection (false);
		}
		degree = Mathf.Clamp (degree, -maxAngle, -minAngle);

		animator.SetFloat ("AimAngleRatio", Mathf.Abs(degree / 180));

		shoulder.transform.localRotation = Quaternion.Euler(0, 0, degree + defaultAngle);
	}

	public void AimToForward(Vector3 targetpos)
	{
		var inputX = Input.GetAxis ("Horizontal");
		if (inputX < 0) {
			SetDirection (false);
		} 
		else if(inputX > 0){
			SetDirection (true);
		}

		animator.SetFloat ("AimAngleRatio", 0.5f);

		shoulder.transform.localRotation = Quaternion.Euler (Vector3.zero);
	}

	// Update is called once per frame
	void Update () {

		if (null == shoulder)
		{
			return;
		}

		if (true == hideShoulder)
		{
			shoulder.GetComponent<SpriteRenderer> ().enabled = false;
		}
		else
		{
			shoulder.GetComponent<SpriteRenderer> ().enabled = true;
		}

		var mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);

		if (isActive) {
			AimToObject (mousePos);
		} else {
			AimToForward (mousePos);
		}
	}
}
