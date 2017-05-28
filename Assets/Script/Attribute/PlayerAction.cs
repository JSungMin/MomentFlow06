using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour {
	private Animator animator;
	public Animator gunAnimator;

	private EquiptInfo equiptInfo;

	private ShoulderAction shoulderAction;

	private Rigidbody pBody;
	private BoxCollider pCollider;
	public bool holdOnWeapon = false;

	public Vector3 velocity;

	public LayerMask collisionMask;

	public float distanceToGround = 4;

	public float maxSpeed;
	public float accel;

    private SkillBase[] skills;
    private int skillNum;

	Vector2 input;

    private void Awake()
    {
        // TODO factory pattern을 사용해서 리팩토링하면 될거 같은데
		// 이건 고민해야 할 것 같아오..  by SungMin
        skills = new SkillBase[] { new TimePause(KeyCode.Z), new TimeRevert(KeyCode.X) };
        skillNum = skills.Length;
        // DEBUG 시간 돌아가는거 잘 확인하기 위해 이렇게 해둠
        //Physics.gravity = Vector3.down * 0.1f;
    }

    private T GetSkill<T>()
    {
        T child = skills.OfType<T>().FirstOrDefault();
        if (child == null)
            return default(T);
        return child;
    }

    void Start () 
	{
		Physics.gravity = Vector3.down * 9.8f;
		animator = transform.GetComponentInChildren<Animator> ();
		pBody = GetComponent<Rigidbody> ();
		pCollider = GetComponent<BoxCollider> ();
		shoulderAction = gunAnimator.GetComponent<ShoulderAction> ();
		equiptInfo = GetComponent<EquiptInfo> ();
		EquiptDefaultWeapon ();
	}

	void Update ()
	{
        // 오브젝트 하나 만들고 update 계속 돌리는 것이 좋을 거 같다
        for (int i = 0; i < skillNum; i++)
        {
            if (skills[i].IsTryUseSKill())
                skills[i].TryUseSkill();
            else if(skills[i].IsTryCancelSkill())
                skills[i].TryCancelSkill();
        }

        input = new Vector2 (Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));

		animator.SetFloat ("HorizontalInput", input.x);
		animator.SetFloat ("VerticalInput", input.y);

		animator.SetFloat ("MaxMoveSpeed", maxSpeed);
		animator.SetFloat ("MoveAccel", accel);

		animator.SetBool ("HoldOnWeapon", holdOnWeapon);

		if (holdOnWeapon) {
			ExcuteShotableLayer ();
		} else {
			ExcuteNoneShotableLayer ();
		}

		if (input.x == 0)
		{
			animator.SetTrigger ("TriggerIdle");
			gunAnimator.SetTrigger ("TriggerIdle");
		}
		
		if (!Input.GetKey(KeyCode.LeftShift) && input.x != 0)
		{
			animator.SetTrigger ("TriggerWalk");
			gunAnimator.SetTrigger ("TriggerIdle");
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
	}

	void EquiptDefaultWeapon ()
	{
		equiptInfo.AddWeapon (0);
		equiptInfo.EquiptWeapon (0);
	}

	void ExcuteShotableLayer ()
	{
		animator.SetLayerWeight (1, 0);
		animator.SetLayerWeight (2, 1);
		GetComponent<AimTarget> ().hideShoulder = false;
	}

	void ExcuteNoneShotableLayer ()
	{
		animator.SetLayerWeight (1, 1);
		animator.SetLayerWeight (2, 0);
		GetComponent<AimTarget> ().hideShoulder = true;
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
