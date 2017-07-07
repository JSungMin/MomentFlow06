using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolAction : ActionCheckerBase {

	bool needYMove;

	public float stopAndSearchDuration;
	public float stopAndSearchDurationTimer = 0;

	public Transform[] patrolTransfroms;
	private Vector3[] patrolPoints;
	private int patrolIndex;
	private int patrolDirection = 1;

	public float autoStopOffset = 0.05f;

	WalkState walkState;

	new void Start ()
	{
		base.Start();
		patrolPoints = new Vector3[patrolTransfroms.Length];
		for (int i = 0; i < patrolPoints.Length; i++)
		{
			patrolPoints [i] = patrolTransfroms [i].transform.position;
			patrolPoints [i].z = patrolTransfroms [i].transform.localPosition.z;
		}
	}

	#region implemented abstract members of ActionCheckerBase
	protected override bool IsSatisfied ()
	{
		return true;
	}
	protected override void DoAction ()
	{
		if (enemyAction.enemyOutsideInfo.interactableObject.Count != 0) {
			var obj = enemyAction.enemyOutsideInfo.interactableObject [Random.Range (0, enemyAction.enemyOutsideInfo.interactableObject.Count - 1)];
			if (!obj.GetComponentInParent<InteractableObject> ().isInteracted && obj.GetComponentInParent<InteractableObject>().objectType == InteractableObjectType.Door)
				obj.GetComponentInParent<InteractableObject> ().TryInteract (enemyInfo.gameObject);
		}

		if (Vector3.Distance (patrolPoints [patrolIndex], enemyInfo.transform.position) > autoStopOffset) {
			walkState = enemyAction.bodyAnimator.GetBehaviour <WalkState> ();

			ChaseYAxis ();

			ChaseZAxis ();



			enemyAction.bodyAnimator.SetTrigger ("TriggerWalk");
			enemyAction.shoulderAnimator.SetTrigger ("TriggerWalk");
		} else {
			if (stopAndSearchDurationTimer <= stopAndSearchDuration) {
				stopAndSearchDurationTimer += enemyInfo.dynamicObject.customDeltaTime;

				enemyAction.bodyAnimator.SetTrigger ("TriggerIdle");
				enemyAction.shoulderAnimator.SetTrigger ("TriggerIdle");
			} 
			else {
				if (patrolIndex + patrolDirection < patrolPoints.Length &&
					patrolIndex + patrolDirection >= 0)
				{
					patrolIndex += patrolDirection;
					stopAndSearchDurationTimer = 0;
				}
				else {
					patrolDirection *= -1;
				}
			}
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
		var detectedPos = patrolPoints [patrolIndex] + enemyAction.enemyBodyCollider.bounds.center;
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
		var targetZ = patrolPoints [patrolIndex].z;
		CheckAndAddTargetZList (targetZ);
		InitTargetZListOffset (myZ);

		if (enemyAction.targetZList.Count != enemyAction.targetZListOffet) {
			targetZ = enemyAction.targetZList [enemyAction.targetZListOffet];
		}
		else {
			targetZ = patrolPoints [patrolIndex].z;
		}

		if (CompareEnemyAndTargetOnSameZ ()) {
			SetWalkPosition (patrolPoints [patrolIndex]);
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
