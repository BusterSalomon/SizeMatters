using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Movement : Walkable
{
    [Header("Implementation")]
    public float jumpForce = 15f;
    public UnityEvent DidJump;
    public UnityEvent<int> OnChangeDirection;
    private Animator anim;
    private AudioManager am;

    protected override void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        am = AudioManager.instance;
    }

    protected override void MovementUpdate()
    {
        Move();
        Jump();
    }


    void Move()
    {
        int dir = 0;
        if (Input.GetKey(KeyCode.A))
        {
            dir = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            dir = 1;
        }

        if (dir != 0)
        {
            transform.localScale = new Vector3(dir * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            OnChangeDirection.Invoke(dir);
        }

        rb.velocity = new Vector2(dir * MovementSpeed, rb.velocity.y);
    }

    void Jump()
    {
        if (Input.GetKey(KeyCode.W) && IsGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            DidJump.Invoke();
            am.Play("jump");
        }
    }

    protected override void DidStartMoving()
    {
        am.Play("run");
    }

    protected override void DidStopMoving()
    {
        am.Stop("run");
    }

    protected override void DidLandInternal()
    {
        am.Play("land");
    }

}
