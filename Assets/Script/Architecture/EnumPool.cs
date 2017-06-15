﻿using System.Collections;
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

public enum BodyStateType
{
    Die = 0,
    Shot = 1,
    Idle = 2
}

public enum ShoulderStateType
{
	Shot = 0,
    Aim = 1,
    Idle = 2
}

public class EnumPool : MonoBehaviour {
	
}