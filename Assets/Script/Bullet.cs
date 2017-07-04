using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	public int bulletIndex;

	public DynamicObject dynamicObject;

	private Collider ignoreCollider;

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
		gameObject.name = gameObject.name + bulletIndex;
		rigid = GetComponent<Rigidbody> ();
		startPosition = transform.position;
		destroyParticle = GetComponentInChildren<ParticleSystem> ().gameObject;
		dynamicObject = GetComponent<DynamicObject> ();
	}

	public void Update()
	{
		flingDistance += rigid.velocity.magnitude * dynamicObject.customDeltaTime;
		rigid.velocity = originVelocity * Mathf.Pow(dynamicObject.customTimeScale, 12f);
		if (maxFlingDistance < flingDistance)
		{
			DestroyBullet ();
		}
	}

	public void FixedUpdate()
	{
		
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
				ignoreCollider = col.collider;
				Physics.IgnoreCollision (ignoreCollider, GetComponent<Collider>(), true);
				return;
			}
		} 
		else if (owner.CompareTag ("Enemy"))
		{
			if (col.collider.CompareTag ("Player"))
			{
				if (col.collider.CompareTag("Decoy"))
				{
					ignoreCollider = col.collider;
					Physics.IgnoreCollision (ignoreCollider, GetComponent<Collider>(), true);
					return;
				}
                col.collider.GetComponentInParent<HumanInfo>().hp -= damage;
			}
			if (col.collider.CompareTag ("Enemy"))
			{
				var enemy = col.collider;
				if (enemy.GetComponentInParent<EnemyInfo> ().teamId != owner.GetComponentInParent<EnemyInfo> ().teamId) {
					DamageToEnemy (col);
				}
				else {
					ignoreCollider = enemy;
					Physics.IgnoreCollision (ignoreCollider, GetComponent<Collider>(), true);
					return;
				}
			}
			if (col.collider.CompareTag ("Decoy"))
			{
				col.collider.GetComponent<DecoyObject> ().playTime = col.collider.GetComponent<DecoyObject> ().lifeTime;
				Physics.IgnoreCollision (col.collider, GetComponent<Collider>(),true);
				return;
			}
			if (col.collider.CompareTag ("SlowField"))
			{
				Physics.IgnoreCollision (col.collider, GetComponent<Collider>(),true);
				return;
			}
		}

		MakeParticle ();
		DestroyBullet ();
	}

	void DamageToEnemy (Collision col)
	{
		col.collider.GetComponentInParent<Rigidbody> ().AddForce (new Vector3 (rigid.velocity.x, 0).normalized * 2, ForceMode.Impulse);
		col.collider.GetComponentInChildren<InteractConditionChecker> ().DoBulletDamage ((int)damage, owner);
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
		if (null != ignoreCollider) {
			Debug.Log ("ignore 발동");
			Physics.IgnoreCollision (ignoreCollider, GetComponent<Collider> (), false);
		}
		dynamicObject.customTimeScale = 1;
		dynamicObject.previousTimeScaleList.Clear ();
		BulletPool.Instance.ReturnBullet (gameObject);
	}
}
