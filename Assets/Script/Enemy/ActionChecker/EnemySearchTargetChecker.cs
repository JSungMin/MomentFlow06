using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySearchTargetChecker : ActionCheckerBase {
	[Range(0,100)]
	public float searchProbability;

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
			if (Random.Range (0, 100) <= searchProbability) {
				enemyAction.bodyAnimator.SetTrigger ("TriggerSearchTarget");

				var obj = enemyAction.enemyOutsideInfo.interactableObject [Random.Range (0, enemyAction.enemyOutsideInfo.interactableObject.Count - 1)];
				if (!obj.GetComponentInParent<InteractableObject> ().isInteracted)
					obj.GetComponentInParent<InteractableObject> ().TryInteract (enemyInfo.gameObject);
			}
		}

		ChaseZAxis ();

		enemyAction.bodyAnimator.SetTrigger ("TriggerWalk");
		enemyAction.shoulderAnimator.SetTrigger ("TriggerWalk");

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
