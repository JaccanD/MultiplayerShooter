using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField] private float damage;
    [SerializeField] [Range(1, 4)] private float critModifier;
        
    [Header("Movement")]
    [SerializeField] private float speed;

    [Header("Hit Detection")]
    [SerializeField] private LayerMask hitBoxLayer;
    private SphereCollider collider;

    private void Start()
    {
        collider = GetComponent<SphereCollider>();
        Debug.Assert(collider == null, "Projectile does not have a collider");
    }
    private void Update()
    {
        hitDetection();
        transform.position += Vector3.forward * speed * Time.deltaTime;
    }
    private void hitDetection()
    {
        bool cast = Physics.SphereCast(transform.position, collider.radius, Vector3.forward, out RaycastHit hit, speed * Time.deltaTime, hitBoxLayer);

        if (cast)
        {
            //Om träff --> Event för träff på en actor
        }

    }
}
