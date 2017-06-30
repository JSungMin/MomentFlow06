using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extinguisher : MonoBehaviour
{
	public DynamicObject dynamicObject;

    private Rigidbody rigidBody;
    private Vector3 explosionPos;
    private float explosionUpwardsModifier = 2.0f;

    public Vector3 relativeExplosionPos;
    public float explosionRadius;
    public float explosionForce;

    private TimeRecallable extinguisherTimeRecallInfo;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        extinguisherTimeRecallInfo = GetComponent<TimeRecallable>();
        explosionPos = transform.position + relativeExplosionPos;
    }

	[HideInInspector]
	public bool isExplosing = false;
    private int exploseCount = 0;
    private const int exploseMaxCount = 10;

    private float exploseTimer = 0.0f;
    private const float exploseDelay = 0.4f;

    private float massRange = 0.4f;
    private bool isToLeft = true;
    private void Update()
    {
        if (exploseCount >= exploseMaxCount)
        {
            rigidBody.centerOfMass = Vector3.zero;
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.T))
            isExplosing = true;

        if (isExplosing)
        {
			exploseTimer += dynamicObject.customDeltaTime;
            rigidBody.centerOfMass = Vector3.right * Random.Range(-massRange, massRange) + Vector3.up * Random.Range(-massRange, massRange);
            if (exploseTimer > exploseDelay)
            {
                exploseTimer = 0.0f;

                if (isToLeft)
                    rigidBody.AddExplosionForce(explosionForce, transform.position + Vector3.right, explosionRadius, explosionUpwardsModifier, ForceMode.Impulse);
                else
                    rigidBody.AddExplosionForce(explosionForce, transform.position + Vector3.left, explosionRadius, explosionUpwardsModifier, ForceMode.Impulse);
                isToLeft = !isToLeft;
                exploseCount++;

                //EmitPowders();
            }
        }
    }

    private void FixedUpdate()
    {
        if (extinguisherTimeRecallInfo.isReverting)
            EmitPowders();
    }

    private const float radius = 0.5f;
    private const int rayNum = 12;
    private const float radianDelta = 2 * Mathf.PI / (float)rayNum;
    private void EmitPowders()
    {
        for (int i = 0; i < rayNum; i++)
        {
            Vector3 direction = Vector3.right * (radius * Mathf.Cos(i * radianDelta)) +
                Vector3.up * (radius * Mathf.Sin(i * radianDelta));

            Debug.DrawRay(transform.position, direction);

            RaycastHit raycastHit;
            if(Physics.Raycast(
                transform.position, 
                Vector3.Normalize(direction),
                out raycastHit,
                radius,
                (1 << LayerMask.NameToLayer("Enemy")) | (1 << LayerMask.NameToLayer("Collision"))))
            {
                if (raycastHit.collider.CompareTag("Enemy"))
                {
                    if (raycastHit.collider != null)
                    {
                        if (raycastHit.collider.GetComponentInChildren<InteractConditionChecker>() != null)
                            raycastHit.collider.GetComponentInChildren<InteractConditionChecker>().DoExtinguisherStun();
                        if (raycastHit.collider.GetComponent<InteractConditionChecker>() != null)
                            raycastHit.collider.GetComponent<InteractConditionChecker>().DoExtinguisherStun();
                    }
                }
            }
        }
    }
}
