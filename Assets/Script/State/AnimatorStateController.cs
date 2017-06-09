using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AnimatorStateController : MonoBehaviour
{
    public static string GetCurrentStateName(EnumType enumType, Animator animator)
    {
        int stateTypeNum = 0;
        switch (enumType)
        {
            case EnumType.WeaponEnum:
                stateTypeNum = Enum.GetNames(typeof(WeaponType)).Length;
                for (int i = 0; i < stateTypeNum; i++)
                    if (Animator.StringToHash(((WeaponType)i).ToString()) == animator.GetCurrentAnimatorStateInfo(0).shortNameHash)
                        return (WeaponType.Sword + i).ToString();
                break;

            case EnumType.SworldEnum:
                stateTypeNum = Enum.GetNames(typeof(SworldType)).Length;
                for (int i = 0; i < stateTypeNum; i++)
                    if (Animator.StringToHash(((SworldType)i).ToString()) == animator.GetCurrentAnimatorStateInfo(0).shortNameHash)
                        return (SworldType.Dagger + i).ToString();
                break;

            case EnumType.RifleEnum:
                stateTypeNum = Enum.GetNames(typeof(RifleType)).Length;
                for (int i = 0; i < stateTypeNum; i++)
                    if (Animator.StringToHash(((RifleType)i).ToString()) == animator.GetCurrentAnimatorStateInfo(0).shortNameHash)
                        return (RifleType.Pistol + i).ToString();
                break;

			case EnumType.BodyStateEnum:
				stateTypeNum = Enum.GetNames(typeof(BodyStateType)).Length;
                for (int i = 0; i < stateTypeNum; i++)
				if (Animator.StringToHash(((BodyStateType)i).ToString()) == animator.GetCurrentAnimatorStateInfo(0).shortNameHash)
					return (BodyStateType.Die + i).ToString();
                break;
		case EnumType.ShoulderStateEnum:
			stateTypeNum = Enum.GetNames(typeof(ShoulderStateType)).Length;
			for (int i = 0; i < stateTypeNum; i++)
				if (Animator.StringToHash(((ShoulderStateType)i).ToString()) == animator.GetCurrentAnimatorStateInfo(0).shortNameHash)
					return (ShoulderStateType.Idle + i).ToString();
			break;
            default:
                return "NONE";
        }

        return "NONE";
    }
}
