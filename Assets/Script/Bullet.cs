using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	public int bulletIndex;
	private Rigidbody rigid;

	public Rigidbody Rigid {
		get {
			return rigid;
		}
	}

	public float damage;
	public float moveSpeed;
	public float maxSpeed;
	public float accel;

	public GameObject destoryParticle;

	public LayerMask collisionMask;

	public void Start()
	{
		rigid = GetComponent<Rigidbody> ();
	}

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
		BulletPool.Instance.ReturnBullet (gameObject);
	}
}
