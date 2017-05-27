using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour {
	private Animator animator;
	public Animator gunAnimator;
	private Rigidbody pBody;
	private BoxCollider pCollider;

	public Vector3 velocity;

	public LayerMask collisionMask;

	public float distanceToGround = 4;

	public float maxSpeed;
	public float accel;

    private SkillBase[] skills;
    private int skillNum;

    private void Awake()
    {
        // TODO factory pattern을 사용해서 리팩토링하면 될거 같은데
        skills = new SkillBase[] { new TimePause(KeyCode.Z), new TimeRevert(KeyCode.X) };
        skillNum = skills.Length;

        // DEBUG 시간 돌아가는거 잘 확인하기 위해 이렇게 해둠
        Physics.gravity = Vector3.down * 0.1f;
    }

    private T GetSkill<T>()
    {
        T child = skills.OfType<T>().FirstOrDefault();
        if (child == null)
            return default(T);
        return child;
    }

    void Start () {
		Physics.gravity = Vector3.down * 9.8f;
		animator = transform.GetComponentInChildren<Animator> ();
		pBody = GetComponent<Rigidbody> ();
		pCollider = GetComponent<BoxCollider> ();
	}

	Vector2 input;
	bool isShot = false;

	void Update () {

        // 오브젝트 하나 만들고 update 계속 돌리는 것이 좋을 거 같다
        for (int i = 0; i < skillNum; i++)
        {
            if (skills[i].IsTryUseSKill())
                skills[i].TryUseSkill();
            else if(skills[i].IsTryCancelSkill())
                skills[i].TryCancelSkill();
        }

        input = new Vector2 (Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));

		isShot = Input.GetMouseButton (0);
		animator.SetBool ("IsShooting", isShot);
		gunAnimator.SetBool ("IsShooting", isShot);

		animator.SetFloat ("HorizontalInput", input.x);
		animator.SetFloat ("VerticalInput", input.y);

		animator.SetFloat ("MaxMoveSpeed", maxSpeed);
		animator.SetFloat ("MoveAccel", accel);

		if (input.x == 0)
		{
			animator.SetTrigger ("TriggerIdle");
			gunAnimator.SetTrigger ("TriggerIdle");
		}
		
		if (!Input.GetKey(KeyCode.LeftShift) && input.x != 0)
		{
			animator.SetTrigger ("TriggerWalk");
			gunAnimator.SetTrigger ("TriggerAim");
		}

		if (Input.GetKey (KeyCode.LeftShift) && input.x != 0) 
		{
			animator.SetTrigger ("TriggerRun");
			gunAnimator.SetTrigger ("TriggerRun");
		}
		
		if (Input.GetKeyDown (KeyCode.Space))
		{
			animator.SetTrigger ("TriggerJump");
		}
		if (velocity.y < 0 && (pCollider.bounds.min.y + velocity.y * Time.deltaTime) >= distanceToGround)
		{
			animator.SetTrigger ("TriggerFalling");
		}
		else if (velocity.y < 0 && (pCollider.bounds.min.y + velocity.y* Time.deltaTime) < distanceToGround)
		{
			animator.SetTrigger ("TriggerLanding");
		}

		if (isShot)
		{
			animator.SetTrigger ("TriggerShot");
			gunAnimator.SetTrigger ("TriggerShot");
		}
	}

	void FixedUpdate()
	{

		pBody.velocity = new Vector3 (
			Mathf.Clamp (pBody.velocity.x, -maxSpeed, maxSpeed),
			pBody.velocity.y,
			0
		);
		velocity = pBody.velocity;
		animator.SetFloat ("VelocityY", velocity.y);

		GetDistanceToGround ();
		animator.SetFloat ("DistanceToGround", distanceToGround);

		if (distanceToGround > 0.1f) {
			animator.SetBool ("IsAir", true);
		} else {
			animator.SetBool ("IsAir", false);
		}
	}
		
	public float GetDistanceToGround ()
	{
		for (int i = 0; i < 3; i++)
		{
			Vector3 origin = pCollider.bounds.min + Vector3.right * pCollider.bounds.size.x * (i / 2);
			Debug.DrawLine (origin, origin + Vector3.down * 3, Color.red);
			RaycastHit hit = new RaycastHit();
			if (Physics.Raycast(origin, Vector3.down, out hit, collisionMask))
			{
				distanceToGround = hit.distance;
				continue;
			}
		}
		return distanceToGround;
	}
}
