using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnumType
{
    WeaponEnum,
    SworldEnum,
    RifleEnum,
    BodyStateEnum,
	ShoulderStateEnum
}

public enum WeaponType
{
	Sword,
	Rifle
}

public enum SworldType
{
	Dagger,
	DoubleHandSworld
}

public enum GunType
{
	Pistol,
	Rifle,
	Shotgun,
	GrenadeLauncher
}

public enum InteractableObjectType
{
	Door,
	Chest,
	Other
}

public enum ItemType
{
	Potion = 0,
	GunAmmoItem,
	Gun
}

public enum PlayerDetectState
{
	Safe = 0,
	Warning = 1,
	Detected = 2
}

public enum EnemyActionType
{
	Idle = 0,
	Suspicious = 1,
	Attack = 2,
	Chase = 3,
	SearchTarget = 4,
	Restore = 5,
	Patrol = 6,
	Damaged,
	Die
}

public enum BodyStateType
{
    Die = 0,
    Stun = 1,
    Shot = 2,
    Cross = 3,
    Run = 4,
    Crouch = 5,
    Idle = 6
}

public enum ShoulderStateType
{
    Stun = 0,
	Reload,
	Shot,
    Run,
    Aim,
    Idle
}

public class EnumPool : MonoBehaviour {
	
}
