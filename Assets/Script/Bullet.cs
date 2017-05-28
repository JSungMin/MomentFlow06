using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	private Rigidbody rigid;

	public float damage;
	public float moveSpeed;
	public float maxSpeed;
	public float accel;

	public GameObject destoryParticle;

	public LayerMask collisionMask;

	public void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.layer == collisionMask)
		{
			ProcessHitCollider (ref col);
		}
	}

	public void ProcessHitCollider(ref Collider col)
	{
		if (col.CompareTag("Enemy"))
		{

		}
		DestroyBullet ();
	}

	public void DestroyBullet()
	{
		rigid.velocity = Vector3.zero;

	}
}
