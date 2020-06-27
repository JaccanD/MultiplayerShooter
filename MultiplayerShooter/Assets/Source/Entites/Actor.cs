using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : Entity
{
    //Serialized Fields
    [Header ("Actor")]
    [SerializeField] private float gravity;
    [SerializeField] private float acceleration;

    [SerializeField] private LayerMask collisionMask;
    [SerializeField] private float skinWidth;
    [SerializeField][Range(0.01f, 0.99f)] private float staticFriktion;
    [SerializeField][Range(0.01f, 0.99f)] private float dynamicFriktion;
    
    //Private Fields
    private Vector3 velocity;
    new private CapsuleCollider collider;

    //Properties
    protected Vector3 Velocity { get { return velocity;} set { velocity = value; } }
    protected Vector3 Direction { get { return new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical")).normalized; } }

    protected virtual void Update()
    {
        AddForces();
        CheckCollision();
        MoveActor();
    }
    /// <summary>
    /// Adds acceleration in the input direction 
    /// and gravity
    /// </summary>
    protected virtual void AddForces()
    {
        
        Velocity += acceleration * (transform.rotation * Direction) * Time.deltaTime;
        Velocity += gravity * Vector3.down * Time.deltaTime;
    }
    /// <summary>
    /// Move the Actor in the direction and
    /// speed of its current Velocity
    /// </summary>
    protected virtual void MoveActor()
    {
        transform.position += velocity * Time.deltaTime;
    }
    /// <summary>
    /// Checks for kollisions and adds
    /// normalforce and friction if the 
    /// actor collides with another object
    /// </summary>
    protected void CheckCollision()
    {
        Vector3 topPoint = transform.position + (Vector3.up * (collider.height - collider.radius * 2) / 2);
        Vector3 botPoint = transform.position + (Vector3.down * (collider.height - collider.radius * 2) / 2);

        float possibleMoveDistance;
        int counter = 0;
        while(Physics.CapsuleCast(topPoint,botPoint,collider.radius,velocity.normalized, out RaycastHit collHit, Mathf.Infinity, collisionMask, QueryTriggerInteraction.Ignore))
        {
            possibleMoveDistance = skinWidth / Vector3.Dot(velocity.normalized, collHit.normal);
            possibleMoveDistance += collHit.distance;

            if(possibleMoveDistance > velocity.magnitude * Time.deltaTime)
            {
                break;
            }
            else if(possibleMoveDistance > 0)
            {
                transform.position += velocity.normalized * possibleMoveDistance;
            }

            if(collHit.distance <= velocity.magnitude * Time.deltaTime + skinWidth) {
                //NormalKraft
                Vector3 normalForce = NormalForce(velocity, collHit.normal);
                Velocity += normalForce;
                //Friktion
                velocity = Friktion(velocity, normalForce);
            }

            counter++;
            if (counter > 10)
                break;

            topPoint = transform.position + (Vector3.up * (collider.height - collider.radius * 2) / 2);
            botPoint = transform.position + (Vector3.down * (collider.height - collider.radius * 2) / 2);
        }
    }
    protected override void Initialize()
    {
        base.Initialize();
        collider = GetComponent<CapsuleCollider>();
        Debug.Assert(collider != null, "No CapsuleCollider found on GameObject " + gameObject.name);
    }

    protected virtual void Awake()
    {
        Initialize();
    }
    /// <summary>
    /// Applies friktion to the velocity and returns the resulting 
    /// velocity
    /// </summary>
    /// <param name="velocity"></param>
    /// <param name="normalForce"></param>
    /// <returns></returns>
    private Vector3 Friktion(Vector3 velocity, Vector3 normalForce)
    {
        // Om statiska friktionen är för stor
        if (velocity.magnitude <= normalForce.magnitude * staticFriktion)
        {
            // Hastighet till noll
            velocity.x = 0;
            velocity.z = 0;
            return velocity;
        }
        velocity += -velocity.normalized * (normalForce.magnitude * dynamicFriktion);
        return velocity;

    }
    /// <summary>
    /// Calculates and returns the NormalForce 
    /// that is applied to an actor as a result of
    /// a collision.
    /// </summary>
    /// <param name="velocity"> The velocity of the actor at the time of the collision</param>
    /// <param name="normal">The normal of the surface the actor collides with</param>
    /// <returns></returns>
    public Vector3 NormalForce(Vector3 velocity, Vector3 normal)
    {
        Vector3 projection;
        float DotProdukt = Vector3.Dot(velocity, normal);
        if (DotProdukt >= 0.0f)
        {
            DotProdukt = 0.0f;
        }
        projection = DotProdukt * normal;
        return -projection;
    }


}
