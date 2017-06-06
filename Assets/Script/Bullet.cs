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

	public float maxFlingDistance;

	private Vector3 startPosition = Vector3.zero;
	private float flingDistance = 0;

	public GameObject destroyParticle;

	public LayerMask collisionMask;

	public void Start()
	{
		rigid = GetComponent<Rigidbody> ();
		startPosition = transform.position;
		destroyParticle = GetComponentInChildren<ParticleSystem> ().gameObject;
	}

	public void Update()
	{
		flingDistance += rigid.velocity.magnitude * Time.deltaTime;
		if (maxFlingDistance < flingDistance)
		{
			DestroyBullet ();
		}
	}

	public void OnCollisionEnter(Collision col)
	{
		ProcessHitCollider (ref col);
	}
		
	public void ProcessHitCollider(ref Collision col)
	{
		if (col.collider.CompareTag("Enemy"))
		{
			col.collider.GetComponent<Rigidbody> ().AddForce (new Vector3 (rigid.velocity.x, 0).normalized * 2, ForceMode.Impulse);
            col.collider.GetComponentInChildren<InteractConditionChecker>().Damage(10);
		}
		destroyParticle.transform.parent = transform.parent;
		destroyParticle.transform.position = transform.position;
		destroyParticle.transform.localScale = Vector3.one;
		destroyParticle.transform.rotation = Quaternion.LookRotation (rigid.velocity.normalized);
		destroyParticle.GetComponent<ParticleSystem> ().Play ();

		DestroyBullet ();
	}
		
	public void DestroyBullet()
	{
		flingDistance = 0;
		rigid.velocity = Vector3.zero;
		transform.position = Vector3.zero;
		BulletPool.Instance.ReturnBullet (gameObject);
	}
}
