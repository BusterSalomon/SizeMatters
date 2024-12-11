using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    protected int latestPlatformID;

    private bool didJustLand = false;

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
                DidLandInternal();
                didJustLand = true;
                IsGrounded = true;
            }
            latestPlatformID = hit.collider.gameObject.GetInstanceID();
        }
        else if (IsGrounded) IsGrounded = false;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        DoGroundCheck();
        if (MovementEnabled) MovementUpdate();

        // TODO: doesn't start running sfx after landing?
        // Check for velocity changes
        float velX = rb.velocity.x;
        float velY = rb.velocity.y;
        
        // Start moving
        if (velX != 0 && velY==0 && currentVelocity == 0) {
            currentVelocity = velX;
            DidStartMoving();
        }

        // Start moving after run
        else if (didJustLand && velX != 0)
        {
            didJustLand = false;
        }

        // Stop moving
        else if ((velX == 0 && currentVelocity != 0) || (!didJustLand && velY != 0))
        {
            currentVelocity = velX;
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

    protected virtual void DidLandInternal() { }

    protected void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * CastDistance + new Vector3(XShift, 0, 0), BoxSize);
    }

    public int GetLatestPlatformID ()
    {
        return latestPlatformID;
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
