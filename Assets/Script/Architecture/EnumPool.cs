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
	Chest
}

public enum ItemType
{
	Potion = 0,
	GunAmmoItem,
	Gun
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
