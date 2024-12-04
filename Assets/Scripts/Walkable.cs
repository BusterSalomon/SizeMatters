using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Walkable : MonoBehaviour
{
    [Header("Walkable: Ground Check")]
    public LayerMask groundLayer;
    public Vector2 BoxSize;
    public float CastDistance;
    public bool IsGrounded = false;
    public float XShift;

    [Header("Walkable: Events")]
    public UnityEvent DidLand;

    [Header("Walkable: Movement")]
    public float MovementSpeed;
    public bool MovementEnabled = true;

    protected Rigidbody2D rb;
    private float currentVelocity = 0;

    protected virtual void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    protected void DoGroundCheck()
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position + new Vector3(XShift, 0, 0), BoxSize, 0, Vector2.down, CastDistance, groundLayer);
        if (hit)
        {
            if (!IsGrounded)
            {
                DidLand.Invoke();
                IsGrounded = true;
            }   
        }
        else if (IsGrounded) IsGrounded = false;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        DoGroundCheck();
        if (MovementEnabled) MovementUpdate();

        // Check for velocity changes
        float vel = rb.velocity.x;
        if (vel != 0 && currentVelocity == 0) {
            currentVelocity = vel;
            DidStartMoving();
        }
        else if (vel == 0 && currentVelocity != 0)
        {
            currentVelocity = vel;
            DidStopMoving();
        }
    }

    protected virtual void FixedUpdate()
    {
        if (MovementEnabled) MovementFixedUpdate();
    }
    
    protected virtual void MovementUpdate(){}

    protected virtual void MovementFixedUpdate(){}

    protected virtual void DidStartMoving(){}

    protected virtual void DidStopMoving() { }

    protected void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * CastDistance + new Vector3(XShift, 0, 0), BoxSize);
    }

    public void DisableMovement ()
    {
        MovementEnabled = false;
    }

    public void EnableMovement ()
    {
        MovementEnabled = true;
    }

}
