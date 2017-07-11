using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySearchTargetChecker : ActionCheckerBase {
	[Range(0,100)]
	public float searchProbability;
	public float autoStopOffset = 0.05f;
	WalkState walkState;

	public List<InteractableObject> aroundObjects = new List<InteractableObject>();
	private int index = 0;

	private float initDetectGauge;

	#region implemented abstract members of ActionCheckerBase
	protected override bool IsSatisfied ()
	{
		return true;
	}
	protected override void DoAction ()
	{
		walkState = enemyAction.bodyAnimator.GetBehaviour<WalkState> ();
		if (aroundObjects.Count == 0) {
			initDetectGauge = enemyInfo.detectGauge;
			var tmpObjList = enemyAction.enemyOutsideInfo.onRoomInfo.interactObjectListInRoom;

			//Sort aroundObject base SuspiciousPoint
			tmpObjList.Sort (delegate(InteractableObject x, InteractableObject y) {
				float dis01 = Vector3.Distance (x.transform.position, enemyAction.suspiciousPoint);
				float dis02 = Vector3.Distance (y.transform.position, enemyAction.suspiciousPoint);

				if (dis01 > dis02)
					return 1;
				else if (dis01 < dis02)
					return -1;
				else
					return 0;
			});

			for (int i = 0; i < tmpObjList.Count; i++) {
				if (Random.Range (0, 100) <= searchProbability) {
					aroundObjects.Add (tmpObjList [i]);
				}
			}
		}
		else {
			if (index == aroundObjects.Count)
			{
				aroundObjects.Clear ();
				index = 0;
				enemyInfo.detectGauge = 0;
				Debug.Log ("Search End");
				return;
			}
			walkState.targetPos = aroundObjects [index].transform.position;
			ChaseZAxis (aroundObjects [index].transform.position);
			AutoStopWalking (aroundObjects [index].transform.position);
		}
	}

	public override void CancelAction()
	{
		index = 0;
		aroundObjects.Clear ();
	}
	#endregion

	private void SetWalkPosition (Vector3 position)
	{
		walkState.targetPos = position;
	}

	void ChaseZAxis (Vector3 targetPos)
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
			SetWalkPosition (targetPos);
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

	void AutoStopWalking (Vector3 targetPos)
	{
		if (Mathf.Abs (enemyInfo.transform.position.x - targetPos.x) <= autoStopOffset) {

			enemyAction.bodyAnimator.SetTrigger ("TriggerSearchTarget");
			enemyAction.shoulderAnimator.SetTrigger ("TriggerSearchTarget");
			aroundObjects [index].TryInteract (enemyInfo.gameObject);
			index += 1;
			enemyInfo.detectGauge = initDetectGauge * (1 - index / aroundObjects.Count);
		}
		else {
			enemyAction.bodyAnimator.SetTrigger ("TriggerWalk");
			enemyAction.shoulderAnimator.SetTrigger ("TriggerWalk");
		}
	}
}
