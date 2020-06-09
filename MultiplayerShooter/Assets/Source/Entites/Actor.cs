using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : Entity
{
    //Serialized Fields
    [SerializeField] private float gravity;
    [SerializeField] private float acceleration;
    
    //Private Fields
    private Vector3 velocity;

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
        Velocity += acceleration * Direction * Time.deltaTime;
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

    }
    protected override void Initialize()
    {
        base.Initialize();
    }

    protected virtual void Awake()
    {
        Initialize();
    }


}
