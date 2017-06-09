using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extinguisher : MonoBehaviour
{
    private Rigidbody rigidBody;
    private Vector3 explosionPos;
    private float explosionUpwardsModifier = 3.0f;

    public Vector3 relativeExplosionPos;
    public float explosionRadius;
    public float explosionForce;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        explosionPos = transform.position + relativeExplosionPos;
    }

    private void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            rigidBody.AddExplosionForce(explosionForce, explosionPos, explosionRadius, explosionUpwardsModifier, ForceMode.Impulse);
    }
}
