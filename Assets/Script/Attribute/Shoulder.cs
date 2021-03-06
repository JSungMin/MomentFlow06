﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoulder : MonoBehaviour {

	protected bool isShooting = false;
	protected bool isAimming = false;

	protected Animator parentAnimator;
	protected Animator shoulderAnimator;

	protected Weapon nowEquiptWeapon = null;

	public Transform shotPosition;

    private AudioSource audioSource;
    private AudioClip gunAClip;
    private AudioClip gunBClip;
    private AudioClip reloadClip;

    protected void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        gunAClip = Resources.Load("Sound/Effect/GunA") as AudioClip;
        gunBClip = Resources.Load("Sound/Effect/GunB") as AudioClip;
        reloadClip = Resources.Load("Sound/Effect/GunReload") as AudioClip;

        parentAnimator = transform.parent.GetComponent<Animator>();
        shoulderAnimator = GetComponent<Animator>();

        nowEquiptWeapon = GetComponentInParent<EquiptInfo>().nowEquiptWeapon;
    }

	public void SyncColorWithBody ()
	{
		if (null == parentAnimator.GetComponent<SpriteRenderer> ().sprite) {
			GetComponent<SpriteRenderer> ().color = new Color (0, 0, 0, 0);
		} else {
			GetComponent<SpriteRenderer> ().color = parentAnimator.GetComponent<SpriteRenderer> ().color;
		}
	}

	public void Shot ()
	{
		if (gameObject.CompareTag ("Enemy")) {
			((Gun)nowEquiptWeapon).ammo = 1;
		}

        if (((Gun)nowEquiptWeapon).ammo - 1 >= 0)
        {
            ((Gun)nowEquiptWeapon).ammo -= 1;
            var usingBullet = ((Gun)nowEquiptWeapon).usingBullet;
            var borrowedBullet = BulletPool.Instance.BorrowBullet(usingBullet, this.gameObject);
            borrowedBullet.transform.position = shotPosition.position;
			var velocity = (shotPosition.position - transform.position).normalized * borrowedBullet.maxSpeed;
			velocity.z = 0;
			borrowedBullet.GetComponent<Bullet>().originVelocity = velocity;
            if (Random.Range(0.0f, 1.0f) > 0.5f)
                audioSource.clip = gunAClip;
            else
                audioSource.clip = gunBClip;
            audioSource.Play();
        }
	}

	public void Reload()
	{
		int emptyAmount = ((Gun)nowEquiptWeapon).maxAmmo - ((Gun)nowEquiptWeapon).ammo;

		if (gameObject.CompareTag ("Enemy")) {
			((Gun)nowEquiptWeapon).magazine += ((Gun)nowEquiptWeapon).maxAmmo;
		}

        audioSource.clip = reloadClip;
        audioSource.Play();
		if (((Gun)nowEquiptWeapon).magazine - emptyAmount >= 0)
		{
			((Gun)nowEquiptWeapon).ammo = ((Gun)nowEquiptWeapon).maxAmmo;
			((Gun)nowEquiptWeapon).magazine -= emptyAmount;
		}
		else 
		{
			((Gun)nowEquiptWeapon).ammo += ((Gun)nowEquiptWeapon).magazine;
			((Gun)nowEquiptWeapon).magazine = 0;
		}
	}
}
