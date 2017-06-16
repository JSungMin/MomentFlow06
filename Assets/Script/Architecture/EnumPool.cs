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

public enum RifleType
{
	Pistol,
	MachineGun,
	Grenade
}

public enum InteractableObjectType
{
	Door,
	Chest,
	Potion
}

public enum BodyStateType
{
    Die = 0,
    Shot = 1,
    Cross = 2,
    Run = 3,
    Crouch = 4,
    Idle = 5
}

public enum ShoulderStateType
{
	Reload = 0,
	Shot,
    Run,
    Aim,
    Idle
}

public class EnumPool : MonoBehaviour {
	
}
