using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRestoreAction : ActionCheckerBase {

	bool needYMove;
	public float autoStopOffset = 0.02f;

	WalkState walkState;

	#region implemented abstract members of ActionCheckerBase
	protected override bool IsSatisfied ()
	{
		return true;
	}
	protected override void DoAction ()
	{
		if (Vector3.Distance (originPoint, enemyInfo.transform.position) > autoStopOffset) {
			walkState = enemyAction.bodyAnimator.GetBehaviour <WalkState> ();

			ChaseYAxis ();

			ChaseZAxis ();

			enemyAction.bodyAnimator.SetTrigger ("TriggerWalk");
			enemyAction.shoulderAnimator.SetTrigger ("TriggerWalk");
		} else {
			enemyAction.bodyAnimator.SetTrigger ("TriggerIdle");
			enemyAction.shoulderAnimator.SetTrigger ("TriggerIdle");
		}
	}
	#endregion

	private void SetWalkPosition (Vector3 position)
	{
		if (!needYMove)
			walkState.targetPos = position;
	}

	void ChaseYAxis ()
	{
		var detectedPos = originPoint + enemyAction.enemyBodyCollider.bounds.center;
		if (detectedPos.y > enemyAction.enemyBodyCollider.bounds.max.y) {
			var nearestYTelporter = FindNearestYTeleporter (TeleportYAxis.FindYUpTeleporters (enemyInfo.transform.position + enemyAction.enemyBodyCollider.center));
			if (null != nearestYTelporter) {
				walkState.targetPos = nearestYTelporter.transform.position;
				needYMove = true;
				if (Mathf.Abs (walkState.targetPos.x - enemyInfo.transform.position.x) <= 0.05f) {
					nearestYTelporter.GoUpStair (enemyInfo.gameObject);
					needYMove = false;
				}
			}
		}
		else
			if (detectedPos.y < enemyAction.enemyBodyCollider.bounds.min.y) {
				var nearestYTelporter = FindNearestYTeleporter (TeleportYAxis.FindYDownTeleporters (enemyInfo.transform.position + enemyAction.enemyBodyCollider.center));
				if (null != nearestYTelporter) {
					walkState.targetPos = nearestYTelporter.transform.position;
					needYMove = true;
					if (Mathf.Abs (walkState.targetPos.x - enemyInfo.transform.position.x) <= 0.05f) {
						nearestYTelporter.GoDownStair (enemyInfo.gameObject);
						needYMove = false;
					}
				}
			}
	}

	void ChaseZAxis ()
	{
		var myZ = enemyAction.transform.position.z;
		var targetZ = originPoint.z;
		CheckAndAddTargetZList (targetZ);
		InitTargetZListOffset (myZ);

		if (enemyAction.targetZList.Count != enemyAction.targetZListOffet) {
			targetZ = enemyAction.targetZList [enemyAction.targetZListOffet];
		}
		else {
			targetZ = originPoint.z;
		}

		if (CompareEnemyAndTargetOnSameZ ()) {
			SetWalkPosition (originPoint);
		}
		else {
			if (myZ > targetZ) {
				var nearestZTeleporter = FindNearestZTeleporter (TeleportZAxis.FindZDownTeleporters (myZ, targetZ));
				if (null != nearestZTeleporter) {
					SetWalkPosition (nearestZTeleporter.transform.position);
					if (Mathf.Abs (walkState.targetPos.x - enemyInfo.transform.position.x) <= 0.05f) {
						nearestZTeleporter.TryInteract (enemyInfo.gameObject);
					}
				}
			}
			else if (myZ < targetZ) {
				var nearestZTeleporter = FindNearestZTeleporter (TeleportZAxis.FindZUpTeleporters (myZ, targetZ));
				if (null != nearestZTeleporter) {
					SetWalkPosition (nearestZTeleporter.transform.position);
					if (Mathf.Abs (walkState.targetPos.x - enemyInfo.transform.position.x) <= 0.05f) {
						nearestZTeleporter.TryInteract (enemyInfo.gameObject);
					}
				}
			}
			else {
				enemyAction.targetZListOffet += 1;
			}
		}
	}
}
