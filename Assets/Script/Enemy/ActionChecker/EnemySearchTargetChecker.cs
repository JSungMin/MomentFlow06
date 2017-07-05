using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySearchTargetChecker : ActionCheckerBase {
	[Range(0,100)]
	public float searchProbability;
	public float autoStopOffset = 0.05f;
	WalkState walkState;

	#region implemented abstract members of ActionCheckerBase
	protected override bool IsSatisfied ()
	{
		return true;
	}
	protected override void DoAction ()
	{
		walkState = enemyAction.bodyAnimator.GetBehaviour<WalkState> ();

		if (enemyAction.enemyOutsideInfo.interactableObject.Count != 0) {
			if (Random.Range (0, 10000) <= searchProbability) {
				enemyAction.bodyAnimator.SetTrigger ("TriggerSearchTarget");

				var obj = enemyAction.enemyOutsideInfo.interactableObject [Random.Range (0, enemyAction.enemyOutsideInfo.interactableObject.Count - 1)];
				if (obj.GetComponentInParent<InteractableObject> ().objectType == InteractableObjectType.Door) {
					if (!obj.GetComponentInParent<InteractableObject>().isInteracted)
					{
						obj.GetComponentInParent<InteractableObject> ().TryInteract (enemyInfo.gameObject);
					}
				} 
				else {
					if (obj.GetComponentInParent<InteractableObject>().isInteracted)
					{
						obj.GetComponentInParent<InteractableObject> ().TryInteract (enemyInfo.gameObject);
					}
				}
			}
		}

		ChaseZAxis ();

		AutoStopWalking ();

	}
	#endregion

	private void SetWalkPosition (Vector3 position)
	{
		walkState.targetPos = position;
	}

	void ChaseZAxis ()
	{
		var myZ = enemyAction.transform.position.z;
		var targetZ = enemyAction.suspiciousPoint.z;;
		CheckAndAddTargetZList (targetZ);
		InitTargetZListOffset (myZ);

		if (enemyAction.targetZList.Count != enemyAction.targetZListOffet) {
			targetZ = enemyAction.targetZList [enemyAction.targetZListOffet];
		}
		else {
			targetZ = enemyAction.suspiciousPoint.z;
		}

		if (CompareEnemyAndTargetOnSameZ ()) {
			SetWalkPosition (enemyAction.suspiciousPoint);
		}
		else {
			if (myZ > targetZ) {
				var nearestZTeleporter = FindNearestZTeleporter (TeleportZAxis.FindZDownTeleporters (myZ, targetZ));
				if (null != nearestZTeleporter) {
					SetWalkPosition (nearestZTeleporter.transform.position);
					if (Mathf.Abs (walkState.targetPos.x - enemyInfo.transform.position.x) <= 0.05f) {
						nearestZTeleporter.CancelTeleport (enemyInfo.gameObject);
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
		
	void AutoStopWalking ()
	{
		if (Mathf.Abs (enemyInfo.transform.position.x - enemyAction.suspiciousPoint.x) <= autoStopOffset) {
			enemyAction.bodyAnimator.SetTrigger ("TriggerIdle");
			enemyAction.shoulderAnimator.SetTrigger ("TriggerIdle");
		}
		else {
			enemyAction.bodyAnimator.SetTrigger ("TriggerWalk");
			enemyAction.shoulderAnimator.SetTrigger ("TriggerWalk");
		}
	}
}
