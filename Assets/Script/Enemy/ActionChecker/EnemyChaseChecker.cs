using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseChecker : ActionCheckerBase {

	RunState runState;

	public bool needYMove = false;

	#region implemented abstract members of ActionCheckerBase
	protected override bool IsSatisfied ()
	{
		return true;
	}
	protected override void DoAction ()
	{
		runState = enemyAction.bodyAnimator.GetBehaviour<RunState> ();

		if (enemyAction.enemyOutsideInfo.interactableObject.Count != 0) {
			var obj = enemyAction.enemyOutsideInfo.interactableObject [Random.Range (0, enemyAction.enemyOutsideInfo.interactableObject.Count - 1)];
			if (!obj.GetComponentInParent<InteractableObject> ().isInteracted && obj.GetComponentInParent<InteractableObject>().objectType == InteractableObjectType.Door)
				obj.GetComponentInParent<InteractableObject> ().TryInteract (enemyInfo.gameObject);
		}

		ChaseYAxis ();

		ChaseZAxis ();
		
		enemyAction.bodyAnimator.SetTrigger ("TriggerRun");
		enemyAction.shoulderAnimator.SetTrigger ("TriggerRun");
	}
	#endregion

	void ChaseYAxis ()
	{
		var detectedPos = enemyAction.suspiciousPoint;

		if (null != enemyAction.detectedTarget)
			detectedPos = enemyAction.detectedTarget.transform.position + enemyAction.detectedTarget.GetComponent<Collider> ().bounds.center;

		if (detectedPos.y > enemyAction.enemyBodyCollider.bounds.max.y) {
			var nearestYTelporter = FindNearestYTeleporter (TeleportYAxis.FindYUpTeleporters (enemyInfo.transform.position + enemyAction.enemyBodyCollider.center));
			if (null != nearestYTelporter) {
				runState.targetPos = nearestYTelporter.transform.position;
				//needYMove = true;
				Debug.Log ("Need Y " + needYMove);
				if (Mathf.Abs (runState.targetPos.x - enemyInfo.transform.position.x) <= 0.05f) {
					if (!enemyAction.bodyAnimator.GetBehaviour<TeleportState> ().isTeleporting) {
						nearestYTelporter.GoUpStair (enemyInfo.gameObject);
						Debug.Log ("Go Up Stair");
					}
				//	needYMove = false;
				}
			}
		}
		else if (detectedPos.y < enemyAction.enemyBodyCollider.bounds.min.y)
		{
			var nearestYTelporter = FindNearestYTeleporter (TeleportYAxis.FindYDownTeleporters (enemyInfo.transform.position + enemyAction.enemyBodyCollider.center));
			if (null != nearestYTelporter) {
				runState.targetPos = nearestYTelporter.transform.position;
				//needYMove = true;
				Debug.Log ("Need Y " + needYMove);
				if (Mathf.Abs (runState.targetPos.x - enemyInfo.transform.position.x) <= 0.05f) {
					if (!enemyAction.bodyAnimator.GetBehaviour<TeleportState> ().isTeleporting) {
						nearestYTelporter.GoDownStair (enemyInfo.gameObject);
						Debug.Log ("Go Up Stair");
					}
				//	needYMove = false;
				}
			}
		}
	}

	void ChaseZAxis ()
	{
		bool isUseSuspiciousPoint = true;
		var myZ = enemyAction.transform.position.z;
		var targetZ = enemyAction.suspiciousPoint.z;

		if (null != enemyAction.detectedTarget) {
			targetZ = enemyAction.detectedTarget.transform.position.z;
			isUseSuspiciousPoint = false;
		}

		CheckAndAddTargetZList (targetZ);
		InitTargetZListOffset (myZ);

		if (enemyAction.targetZList.Count != enemyAction.targetZListOffet) {
			targetZ = enemyAction.targetZList [enemyAction.targetZListOffet];
		}
		else {
			if (isUseSuspiciousPoint)
				targetZ = enemyAction.suspiciousPoint.z;
			else
				targetZ = enemyAction.detectedTarget.transform.position.z;
		}

		if (CompareEnemyAndTargetOnSameZ ()) {
			if (isUseSuspiciousPoint) {
				SetWalkPosition (enemyAction.suspiciousPoint);
			}
			else {
				SetWalkPosition (enemyAction.detectedTarget.transform.position);
			}
		}
		else {
			if (myZ > targetZ) {
				var nearestZTeleporter = FindNearestZTeleporter (TeleportZAxis.FindZDownTeleporters (myZ, targetZ));
				if (null != nearestZTeleporter) {
					SetWalkPosition (nearestZTeleporter.transform.position);
					if (Mathf.Abs (runState.targetPos.x - enemyInfo.transform.position.x) <= 0.05f) {
						nearestZTeleporter.TryInteract (enemyInfo.gameObject);
					}
				}
			}
			else if (myZ < targetZ) {
				var nearestZTeleporter = FindNearestZTeleporter (TeleportZAxis.FindZUpTeleporters (myZ, targetZ));
				if (null != nearestZTeleporter) {
					SetWalkPosition (nearestZTeleporter.transform.position);
					if (Mathf.Abs (runState.targetPos.x - enemyInfo.transform.position.x) <= 0.05f) {
						nearestZTeleporter.TryInteract (enemyInfo.gameObject);
					}
				}
			}
			else {
				enemyAction.targetZListOffet += 1;
			}
		}
	}

	private void SetWalkPosition (Vector3 position)
	{
		runState.targetPos = position;
	}
}
