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
		var detectedPos = enemyAction.detectedTarget.transform.position + enemyAction.detectedTarget.GetComponent<Collider> ().bounds.center;
		if (detectedPos.y > enemyAction.enemyBodyCollider.bounds.max.y) {
			var nearestYTelporter = FindNearestYTeleporter (TeleportYAxis.FindYUpTeleporters (enemyInfo.transform.position + enemyAction.enemyBodyCollider.center));
			if (null != nearestYTelporter) {
				runState.targetPos = nearestYTelporter.transform.position;
				needYMove = true;
				if (Mathf.Abs (runState.targetPos.x - enemyInfo.transform.position.x) <= 0.05f) {
					nearestYTelporter.GoUpStair (enemyInfo.gameObject);
					needYMove = false;
				}
			}
		}
		else
			if (detectedPos.y < enemyAction.enemyBodyCollider.bounds.min.y) {
				var nearestYTelporter = FindNearestYTeleporter (TeleportYAxis.FindYDownTeleporters (enemyInfo.transform.position + enemyAction.enemyBodyCollider.center));
				if (null != nearestYTelporter) {
					runState.targetPos = nearestYTelporter.transform.position;
					needYMove = true;
					if (Mathf.Abs (runState.targetPos.x - enemyInfo.transform.position.x) <= 0.05f) {
						nearestYTelporter.GoDownStair (enemyInfo.gameObject);
						needYMove = false;
					}
				}
			}
	}

	void ChaseZAxis ()
	{
		var myZ = enemyAction.transform.position.z;
		var targetZ = enemyAction.detectedTarget.transform.position.z;
		CheckAndAddTargetZList (targetZ);
		InitTargetZListOffset (myZ);

		if (enemyAction.targetZList.Count != enemyAction.targetZListOffet) {
			targetZ = enemyAction.targetZList [enemyAction.targetZListOffet];
		}
		else {
			targetZ = enemyAction.detectedTarget.transform.position.z;
		}

		if (CompareEnemyAndTargetOnSameZ ()) {
			SetWalkPosition (enemyAction.detectedTarget.transform.position);
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
		if (!needYMove)
			runState.targetPos = position;
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
			Vector3 pos02 = teleporters [0].transform.position;

			var dis = Vector3.Distance (pos01, pos02);
			var index = 0;
			for (int i = 0; i < teleporters.Count; i++)
			{
				pos02 = teleporters [i].transform.position;
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
