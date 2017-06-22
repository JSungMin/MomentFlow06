using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCutSceneAnimation : MonoBehaviour {
	public Animator rayDummyAnimation;

	public void RayTypeKeyboard ()
	{
		rayDummyAnimation.Play ("TypeKeyboard");
	}
		
	public void RayStopTyping ()
	{
		rayDummyAnimation.Play ("StopTyping");
	}

	public void RayTurnOn ()
	{
		rayDummyAnimation.Play ("DoctorRayTurn");
	}

	public void RayReverseTurnOn()
	{
		rayDummyAnimation.Play ("DoctorRayReverseTurn");
	}

	public void RayIdle ()
	{
		rayDummyAnimation.Play ("DoctorRayIdle");
	}
}
