using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseChecker : ActionCheckerBase {

	RunState runState;

	public bool needYMove = false;
	public HideableObject hidedObject;

	#region implemented abstract members of ActionCheckerBase
	protected override bool IsSatisfied ()
	{
		return true;
	}
	protected override void DoAction ()
	{
		runState = enemyAction.bodyAnimator.GetBehaviour<RunState> ();
		if (null !=	enemyAction.detectedTarget)
			hidedObject = HideableObject.FindHidedObject (enemyAction.detectedTarget.GetComponentInParent<HumanInfo> ().gameObject);

		if (null != hidedObject) {
			runState.targetPos = hidedObject.hidedObject.transform.position;
		}

		if (enemyAction.enemyOutsideInfo.interactableObject.Count != 0) {
			var obj = enemyAction.enemyOutsideInfo.interactableObject [Random.Range (0, enemyAction.enemyOutsideInfo.interactableObject.Count - 1)];
			if (!obj.GetComponentInParent<InteractableObject> ().isInteracted && obj.GetComponentInParent<InteractableObject>().objectType == InteractableObjectType.Door)
				obj.GetComponentInParent<InteractableObject> ().TryInteract (enemyInfo.gameObject);
		}

		var detectedPos = enemyAction.suspiciousPoint;

		if (null != enemyAction.detectedTarget)
		{
			detectedPos = enemyAction.detectedTarget.transform.position;
		}


		ChaseYAxis (detectedPos);

		ChaseZAxis (detectedPos);

		var pos01 = enemyInfo.transform.position;
		pos01.z = 0;
		var pos02 = runState.targetPos;
		pos02.z = 0;

		if (Vector3.Distance (pos01, pos02) <= 0.05f) {
			
			if (null != enemyAction.detectedTarget)
			{
				if (null != hidedObject) {
					Debug.Log ("Try Out");
					enemyAction.bodyAnimator.SetTrigger ("TriggerSearchTarget");
					enemyAction.shoulderAnimator.SetTrigger ("TriggerSearchTarget");
					hidedObject.TryInteract (enemyInfo.gameObject);
					hidedObject = null;
				}
			}
		} else {
			enemyAction.bodyAnimator.SetTrigger ("TriggerRun");
			enemyAction.shoulderAnimator.SetTrigger ("TriggerRun");
		}
	}

	public override void CancelAction()
	{

	}
	#endregion

	void ChaseYAxis (Vector3 detectedPos)
	{
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

	void ChaseZAxis (Vector3 detectedPos)
	{
		var myZ = enemyAction.transform.position.z;
		var targetZ = detectedPos.z;

		CheckAndAddTargetZList (targetZ);
		InitTargetZListOffset (myZ);

		if (enemyAction.targetZList.Count != enemyAction.targetZListOffet) {
			targetZ = enemyAction.targetZList [enemyAction.targetZListOffet];
		}
		else {
			targetZ = detectedPos.z;
		}

		if (CompareEnemyAndTargetOnSameZ ()) {
			SetWalkPosition (detectedPos);
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
		Debug.Log ("Set Pos");
		runState.targetPos = position;
	}
}
