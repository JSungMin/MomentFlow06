using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	public int bulletIndex;

	public GameObject owner;

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

	public Vector3 originVelocity;

	public void Start()
	{
		rigid = GetComponent<Rigidbody> ();
		startPosition = transform.position;
		destroyParticle = GetComponentInChildren<ParticleSystem> ().gameObject;
	}

	public void Update()
	{
		flingDistance += rigid.velocity.magnitude * TimeManager.GetInstance().customDeltaTime;
		if (maxFlingDistance < flingDistance)
		{
			DestroyBullet ();
		}
	}

	public void FixedUpdate()
	{
		rigid.velocity = originVelocity * TimeManager.GetInstance().customTimeScale;
	}

	public void OnCollisionEnter(Collision col)
	{
		ProcessHitCollider (ref col);
	}

	public void ProcessHitCollider(ref Collision col)
	{
		if (owner == null) {
			return;
		}
		if (owner.CompareTag ("Player")) {
			if (col.collider.CompareTag ("Enemy"))
			{
				DamageToEnemy (col);
			}
			else if (col.collider.CompareTag ("Player"))
			{
				
			}
		} 
		else if (owner.CompareTag ("Enemy"))
		{
			if (col.collider.CompareTag ("Player"))
			{
				Debug.Log ("DamageTo Player");
			}
			if (col.collider.CompareTag ("Enemy"))
			{
				var enemy = col.collider;
				if (enemy.GetComponentInParent<EnemyInfo> ().teamId != owner.GetComponentInParent<EnemyInfo> ().teamId) {
					DamageToEnemy (col);
				}
				else {
					Physics.IgnoreCollision (enemy, GetComponent<Collider> ());
					return;
				}
			}
		}

		MakeParticle ();
		DestroyBullet ();
	}

	void DamageToEnemy (Collision col)
	{
		col.collider.GetComponentInParent<Rigidbody> ().AddForce (new Vector3 (rigid.velocity.x, 0).normalized * 2, ForceMode.Impulse);
		col.collider.GetComponentInChildren<InteractConditionChecker> ().DoBulletDamage ((int)damage);
	}

	void MakeParticle ()
	{
		destroyParticle.transform.parent = transform.parent;
		destroyParticle.transform.position = transform.position;
		destroyParticle.transform.localScale = Vector3.one;
		if (rigid.velocity.normalized != Vector3.zero) {
			destroyParticle.transform.rotation = Quaternion.LookRotation (rigid.velocity.normalized);
		}
		destroyParticle.GetComponent<ParticleSystem> ().Play ();
	}
		
	public void DestroyBullet()
	{
		flingDistance = 0;
		rigid.velocity = Vector3.zero;
		transform.position = Vector3.zero;
		BulletPool.Instance.ReturnBullet (gameObject);
	}
}
