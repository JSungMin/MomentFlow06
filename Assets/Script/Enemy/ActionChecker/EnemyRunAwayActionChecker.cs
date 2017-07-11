using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyRunAwayActionChecker : ActionCheckerBase {

	bool needYMove;
	public float autoStopOffset = 0.02f;
	public float runStepAmount = 1;
	public LayerMask collisionMask;

	public Transform alertTransform;
	private Vector3 alertPoint;

	private bool isAlertSuccess = false;

	RunState runState;

	public UnityEvent alertEvent;

	private Vector3 dir;

	#region implemented abstract members of ActionCheckerBase

	new void Start()
	{
		base.Start();
		alertPoint = alertTransform.position;
	}

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

		if (!isAlertSuccess) {
			dir = (enemyAction.detectedTarget.transform.position - enemyInfo.transform.position).normalized;
			ChaseYAxis ();
			ChaseZAxis ();

			if (Mathf.Abs (alertPoint.x - enemyInfo.transform.position.x) <= 0.05f)
			{
				isAlertSuccess = true;
				alertEvent.Invoke ();
				Debug.Log ("AlertSuccess");
			}
		}
		else {
			if (enemyAction.enemyOutsideInfo.interactableObject.Count != 0) {
				var obj = enemyAction.enemyOutsideInfo.interactableObject [Random.Range (0, enemyAction.enemyOutsideInfo.interactableObject.Count - 1)];
				if (!obj.GetComponentInParent<InteractableObject> ().isInteracted && obj.GetComponentInParent<InteractableObject>().objectType == InteractableObjectType.Door)
					obj.GetComponentInParent<InteractableObject> ().TryInteract (enemyInfo.gameObject);
			}
			
			var collisions = Physics.RaycastAll (enemyAction.transform.position + enemyAction.enemyBodyCollider.center, dir, runStepAmount, collisionMask);

			Debug.Log (dir);

			if (collisions.Length != 0) {
				Debug.Log ("Collider");
				runState.targetPos = enemyInfo.transform.position + dir * runStepAmount;
			}
			else
				runState.targetPos = enemyInfo.transform.position - dir * runStepAmount;
		}
		enemyAction.bodyAnimator.SetTrigger ("TriggerRun");
		enemyAction.shoulderAnimator.SetTrigger ("TriggerRun");
	}

	public override void CancelAction()
	{

	}
	#endregion

	private void SetWalkPosition (Vector3 position)
	{
		runState.targetPos = position;
	}

	void ChaseYAxis ()
	{
		var detectedPos = alertPoint;
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
		var targetZ = alertPoint.z;
		CheckAndAddTargetZList (targetZ);
		InitTargetZListOffset (myZ);

		if (enemyAction.targetZList.Count != enemyAction.targetZListOffet) {
			targetZ = enemyAction.targetZList [enemyAction.targetZListOffet];
		}
		else {
			targetZ = alertPoint.z;
		}

		if (CompareEnemyAndTargetOnSameZ ()) {
			SetWalkPosition (alertPoint);
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
}
