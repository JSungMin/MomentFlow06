using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour {
	public Animator playerAnimator;
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
        skills = new SkillBase[] { new TimePause(KeyCode.Z), new TimeRevert(KeyCode.X) };
        skillNum = skills.Length;
    }

	public void Start()
	{
		Physics.gravity = Vector3.down * 9.8f;
		playerAnimator = transform.GetComponentInChildren<Animator> ();
		pBody = GetComponent<Rigidbody> ();
		pCollider = GetComponent<BoxCollider> ();
		shoulderAction = gunAnimator.GetComponent<ShoulderAction> ();
		equiptInfo = GetComponent<EquiptInfo> ();
	}

    private T GetSkill<T>()
    {
        T child = skills.OfType<T>().FirstOrDefault();
        if (child == null)
            return default(T);
        return child;
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

		playerAnimator.SetFloat ("HorizontalInput", input.x);
		playerAnimator.SetFloat ("VerticalInput", input.y);

		playerAnimator.SetFloat ("MaxMoveSpeed", maxSpeed);
		playerAnimator.SetFloat ("MoveAccel", accel);

		playerAnimator.SetBool ("HoldOnWeapon", holdOnWeapon);

		if (holdOnWeapon) {
			ExcuteShotableLayer ();
		} else {
			ExcuteNoneShotableLayer ();
		}

		if (input.x == 0)
		{
			playerAnimator.SetTrigger ("TriggerIdle");
			gunAnimator.SetTrigger ("TriggerIdle");
		}
		
		if (!Input.GetKey(KeyCode.LeftShift) && input.x != 0)
		{
			playerAnimator.SetTrigger ("TriggerWalk");
			gunAnimator.SetTrigger ("TriggerIdle");
		}

		if (Input.GetKey (KeyCode.LeftShift) && input.x != 0) 
		{
			playerAnimator.SetTrigger ("TriggerRun");
			gunAnimator.SetTrigger ("TriggerRun");
		}
		
		if (Input.GetKeyDown (KeyCode.Space))
		{
			playerAnimator.SetTrigger ("TriggerJump");
		}

		if (Input.GetKey (KeyCode.S)) {
			playerAnimator.SetTrigger ("TriggerCrouch");
			playerAnimator.ResetTrigger ("TriggerStandUp");
			playerAnimator.SetBool ("IsCrouching", true);
		} else {
			playerAnimator.SetTrigger ("TriggerStandUp");
		}

		if (velocity.y < 0 && (pCollider.bounds.min.y + velocity.y * Time.deltaTime) >= distanceToGround)
		{
			playerAnimator.SetTrigger ("TriggerFalling");
		}
		else if (velocity.y < 0 && (pCollider.bounds.min.y + velocity.y* Time.deltaTime) < distanceToGround)
		{
			playerAnimator.SetTrigger ("TriggerLanding");
		}

        if (Input.GetKeyDown(KeyCode.C))
        {
            playerAnimator.SetTrigger("TriggerDie");
            holdOnWeapon = false;
            GetComponent<AimTarget>().hideShoulder = true;
        }
    }

	void ExcuteShotableLayer ()
	{
		playerAnimator.SetLayerWeight (1, 0);
		playerAnimator.SetLayerWeight (2, 1);
		GetComponent<AimTarget> ().hideShoulder = false;
	}

	void ExcuteNoneShotableLayer ()
	{
		playerAnimator.SetLayerWeight (1, 1);
		playerAnimator.SetLayerWeight (2, 0);
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
		playerAnimator.SetFloat ("VelocityY", velocity.y);

		GetDistanceToGround ();
		playerAnimator.SetFloat ("DistanceToGround", distanceToGround);

		if (distanceToGround > 0.05f) {
			playerAnimator.SetBool ("IsAir", true);
		} else {
			playerAnimator.SetBool ("IsAir", false);
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
