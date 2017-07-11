using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySuspiciousChecker : ActionCheckerBase {

	WalkState walkState;

	bool needYMove = false;
	public float autoStopOffset = 0.05f;

	#region implemented abstract members of ActionCheckerBase

	protected override bool IsSatisfied ()
	{
		return true;
	}

	protected override void DoAction ()
	{
		walkState = enemyAction.bodyAnimator.GetBehaviour<WalkState> ();

		var myZ = enemyAction.transform.position.z;
		var targetZ = enemyAction.suspiciousPoint.z;

		if (targetZ == myZ) {
			SetWalkPosition (enemyAction.suspiciousPoint);
		}
		else {
			if (myZ > targetZ) {
				var nearestZTeleporter = FindNearestZTeleporter (TeleportZAxis.FindZDownTeleporters (myZ));
				if (null != nearestZTeleporter) {
					SetWalkPosition (nearestZTeleporter.transform.position);
					if (Mathf.Abs (walkState.targetPos.x - enemyInfo.transform.position.x) <= 0.05f)
					{
						nearestZTeleporter.CancelTeleport (enemyInfo.gameObject);
					}
				}
			}
			else {
				var nearestZTeleporter = FindNearestZTeleporter (TeleportZAxis.FindZUpTeleporters (myZ));
				if (null != nearestZTeleporter) {
					SetWalkPosition (nearestZTeleporter.transform.position);
					if (Mathf.Abs (walkState.targetPos.x - enemyInfo.transform.position.x) <= 0.05f)
					{
						nearestZTeleporter.CancelTeleport (enemyInfo.gameObject);
					}
				}
			}
		}
		if (Mathf.Abs (walkState.targetPos.x - enemyInfo.transform.position.x) <= autoStopOffset) {
			enemyAction.bodyAnimator.SetTrigger ("TriggerIdle");
			enemyAction.shoulderAnimator.SetTrigger ("TriggerIdle");
		}
		else {
			enemyAction.bodyAnimator.SetTrigger ("TriggerWalk");
			enemyAction.shoulderAnimator.SetTrigger ("TriggerWalk");
		}

	}

	public override void CancelAction()
	{

	}
	#endregion

	private void SetWalkPosition (Vector3 position)
	{
		if (!needYMove)
			walkState.targetPos = position;
	}

	public TeleportYAxis FindNearestYTeleporter (List<TeleportYAxis> teleporters)
	{
		if (teleporters.Count != 0) {
			Vector3 pos01 = enemyInfo.transform.position;
			pos01.z = 0;
			Vector3 pos02 = teleporters [0].transform.position;
			pos02.z = 0;
			var dis = Vector3.Distance (pos01, pos02);
			var index = 0;
			for (int i = 0; i < teleporters.Count; i++)
			{
				pos02 = teleporters [i].transform.position;
				pos02.z = 0;
				var tmpDis = Vector3.Distance (pos01,pos02);
				if (tmpDis < dis)
				{
					index = i;
					dis = tmpDis;
				}
			}
			return teleporters [index];
		}
		return null;
	}

	public TeleportZAxis FindNearestZTeleporter (List<TeleportZAxis> teleporters)
	{
		if (teleporters.Count != 0) {
			Vector3 pos01 = enemyInfo.transform.position;
			pos01.z = 0;
			Vector3 pos02 = teleporters [0].transform.position;
			pos02.z = 0;
			var dis = Vector3.Distance (pos01, pos02);
			var index = 0;
			for (int i = 0; i < teleporters.Count; i++)
			{
				pos02 = teleporters [i].transform.position;
				pos02.z = 0;
				var tmpDis = Vector3.Distance (pos01,pos02);
				if (tmpDis < dis)
				{
					index = i;
					dis = tmpDis;
				}
			}
			return teleporters [index];
		}
		return null;
	}
}
