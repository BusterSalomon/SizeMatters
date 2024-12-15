using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTM : TutorialManager
{
    private bool leftPressed;
    private bool rightPressed;
    private bool jumpPressed;

    void Start()
    {
        leftPressed = false;
        rightPressed = false;
        jumpPressed = false;
    }

    protected override void Update()
    {
        base.Update();

        if (!leftPressed && Input.GetKey(KeyCode.A))
        {
            leftPressed = true;
        }

        if (!rightPressed && Input.GetKey(KeyCode.D))
        {
            rightPressed = true;
        }

        if (!jumpPressed && Input.GetKey(KeyCode.W))
        {
            jumpPressed = true;
        }
    }

    public override bool DidComplete()
    {
        return (leftPressed && rightPressed && jumpPressed);
    }
}
