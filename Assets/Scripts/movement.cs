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
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    protected override void MovementUpdate()
    {
        Move();
        Jump();
    }


    void Move()
    {
        int dir = 0;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            dir = -1;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            dir = 1;
        }

        if (dir != 0)
        {
            transform.localScale = new Vector3(dir * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            OnChangeDirection.Invoke(dir);
        }

        if (IsGrounded) rb.velocity = new Vector2(dir * MovementSpeed, rb.velocity.y);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            DidJump.Invoke();
        }
    }

}
