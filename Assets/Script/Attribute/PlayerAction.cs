﻿using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour {
	public Animator playerAnimator;
	public Animator gunAnimator;

	private PlayerInfo playerInfo;
	private EquiptInfo equiptInfo;
	private OutsideInfo outsideInfo;
	private Shoulder shoulderAction;

	private Rigidbody pBody;
	private BoxCollider pCollider;

	public static GameObject nearestStair;

	public bool holdOnWeapon = false;

	public Vector3 velocity;

	public LayerMask collisionMask;

	public float distanceToGround = 4;

	public float maxSpeed;
	public float accel;

    private SkillBase[] skills;
    private int skillNum;

	Vector2 input;
	bool inputF = false;
	bool inputQ = false;
    private AimTarget aimTarget;

    public Mana mana { private set; get; }
    private Coroutine RecoveryManaCo; 
    
	private UnityStandardAssets.ImageEffects.BloomAndFlares bloomEffect;
	private ParticleSystem timePauseEffect;

    private void Awake()
    {
        skills = new SkillBase[] { new TimePause(KeyCode.Z, 15.0f), new TimeRecall(KeyCode.X, 10.0f) };
        skillNum = skills.Length;

        aimTarget = GetComponent<AimTarget>();

        mana = new Mana(100.0f);
        RecoveryManaCo = StartCoroutine(mana.RecoveryMana(1.0f, 0.5f));
    }

	public void Start()
	{
		bloomEffect = Camera.main.GetComponent<UnityStandardAssets.ImageEffects.BloomAndFlares> ();
		timePauseEffect = GameObject.Find ("PauseEffect").GetComponent<ParticleSystem>();

		Physics.gravity = Vector3.down * 9.8f;
		playerInfo = GetComponent<PlayerInfo> ();
		playerAnimator = transform.GetComponentInChildren<Animator> ();
		pBody = GetComponent<Rigidbody> ();
		pCollider = GetComponentInChildren<BoxCollider> ();
		shoulderAction = gunAnimator.GetComponent<Shoulder> ();
		equiptInfo = GetComponent<EquiptInfo> ();
		outsideInfo = GetComponent<OutsideInfo> ();
	}

	public T GetSkill<T>()
    {
        T child = skills.OfType<T>().FirstOrDefault();
        if (child == null)
            return default(T);
        return child;
    }

	public void Update()
	{		
		// 오브젝트 하나 만들고 update 계속 돌리는 것이 좋을 거 같다
		for (int i = 0; i < skillNum; i++)
		{
			if (skills[i].IsTryUseSKill())
				skills[i].TryUseSkill();
			else if(skills[i].IsTryCancelSkill())
				skills[i].TryCancelSkill();
		}
        
		if (GetSkill<TimePause>().isTimePaused) {
			if (input.x != 0)
			{
				if (!timePauseEffect.isPlaying)
					timePauseEffect.Play ();
				GetComponent<GhostingEffect> ().SetEnabled (true);
			}
			else
				GetComponent<GhostingEffect> ().SetEnabled (false);
			bloomEffect.bloomIntensity = Mathf.Clamp (bloomEffect.bloomIntensity + Time.deltaTime * 5, 0, 1.5f);
		} else {
			timePauseEffect.Stop ();
			GetComponent<GhostingEffect> ().SetEnabled (false);
			bloomEffect.bloomIntensity = Mathf.Clamp (bloomEffect.bloomIntensity - Time.deltaTime * 5, 0, 1.5f);
		}

		input = new Vector2 (Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
		inputF = Input.GetKeyDown (KeyCode.F);
		inputQ = Input.GetKeyDown (KeyCode.Q);

		var nearestObstacle = outsideInfo.GetNearestObstacleObject ();
		nearestStair = outsideInfo.GetNearestStairObject ();

		InitAnimatorParameters ();

		SelectAnimatorLayer ();

		CheckCrossObstacle (nearestObstacle);

		CheckOnStair (nearestStair);

		CheckInteractWithObject ();

		CheckDrinkUpPotion ();

		TriggerActions ();

		if (aimTarget.isActive)
			aimTarget.AimToObject(Camera.main.ScreenToWorldPoint(Input.mousePosition));
		else
			aimTarget.AimToForward();

		aimTarget.CheckCanVisibleShoulder ();
	}

	void SelectAnimatorLayer ()
	{
		if (holdOnWeapon) 
		{
			ExcuteShotableLayer ();
		}
		else
		{
			ExcuteNoneShotableLayer ();
		}
	}

	void InitAnimatorParameters ()
	{
		playerAnimator.SetFloat ("HorizontalInput", input.x);
		playerAnimator.SetFloat ("VerticalInput", input.y);
		playerAnimator.SetFloat ("MaxMoveSpeed", maxSpeed);
		playerAnimator.SetFloat ("MoveAccel", accel);
		playerAnimator.SetBool ("HoldOnWeapon", holdOnWeapon);
	}

	void ExcuteShotableLayer ()
	{
		playerAnimator.SetLayerWeight (1, 0);
		playerAnimator.SetLayerWeight (2, 1);
	}

	void ExcuteNoneShotableLayer ()
	{
		playerAnimator.SetLayerWeight (1, 1);
		playerAnimator.SetLayerWeight (2, 0);
	}

	void CheckCrossObstacle (GameObject nearestObstacle)
	{
		if (nearestObstacle != null) {
			if (inputF) {
				playerAnimator.SetTrigger ("TriggerCross");
			}
		}
	}

	void CheckOnStair (GameObject nearestStair)
	{
		if (null != nearestStair) {
			var stairCol = nearestStair.GetComponent<Collider> ();
			var playerCol = playerAnimator.GetComponent<Collider> ();
			if (input.y > 0 && stairCol.bounds.min.y > playerCol.bounds.min.y)
			{
				playerAnimator.SetTrigger ("TriggerStair");
				playerAnimator.SetBool ("IsOnStair", true);
			}
			else if (input.y < 0 && stairCol.bounds.max.y > playerCol.bounds.min.y &&
				playerCol.bounds.min.y >= stairCol.bounds.min.y)
			{
				playerAnimator.SetTrigger ("TriggerStair");
				playerAnimator.SetBool ("IsOnStair", true);
			}
		}
	}

	void CheckInteractWithObject ()
	{
		if (inputF) {
			for (int i = 0; i < outsideInfo.interactableObject.Count; i++) {
				var interactableObject = outsideInfo.interactableObject [i].GetComponentInParent<InteractableObject>();
				interactableObject.TryInteract (gameObject);
			}
		}
	}

	void CheckDrinkUpPotion ()
	{
		if (inputQ) {
			if (equiptInfo.itemPocketList.Count != 0) {
				for (int i = 0; i < equiptInfo.itemPocketList.Count; i++) {
					if (null != equiptInfo.itemPocketList [i].GetComponent<Potion> ()) {
						var potion = equiptInfo.itemPocketList [i].GetComponent<Potion> ();
						potion.DrinkUp ();
					}
				}
			}
		}
	}

	void TriggerActions ()
	{
		if (playerInfo.hp <= 0)
		{
			playerAnimator.Play ("Die");
			((PlayerShoulderAction)shoulderAction).HideArm ();
		}

		if (input.x == 0)
		{
			playerAnimator.SetTrigger ("TriggerIdle");
			gunAnimator.SetTrigger ("TriggerIdle");
		}
		if (!Input.GetKey (KeyCode.LeftShift) && input.x != 0)
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

		if (Input.GetKey (KeyCode.S)) 
		{
			playerAnimator.SetTrigger ("TriggerCrouch");
			playerAnimator.ResetTrigger ("TriggerStandUp");
			playerAnimator.SetBool ("IsCrouching", true);
		}
		else
		{
			playerAnimator.SetTrigger ("TriggerStandUp");
		}

		if (velocity.y < 0 && (pCollider.bounds.min.y + velocity.y * Time.deltaTime) >= distanceToGround)
		{
			playerAnimator.SetTrigger ("TriggerFalling");
		}
		else
			if (velocity.y < 0 && (pCollider.bounds.min.y + velocity.y * Time.deltaTime) < distanceToGround) 
			{
				playerAnimator.SetTrigger ("TriggerLanding");
			}
		if (Input.GetKeyDown (KeyCode.C))
		{
			playerAnimator.SetTrigger ("TriggerDie");
			holdOnWeapon = false;
			GetComponent<AimTarget> ().hideShoulder = true;
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
		playerAnimator.SetFloat ("VelocityY", velocity.y);

		GetDistanceToGround ();
		playerAnimator.SetFloat ("DistanceToGround", distanceToGround);

		if (velocity.y < -0.05f) {
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
