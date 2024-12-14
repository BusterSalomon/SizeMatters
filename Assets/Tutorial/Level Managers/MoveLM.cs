using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLM : LevelManager
{

    private bool leftPressed;
    private bool rightPressed;
    private bool jumpPressed;

    // Start is called before the first frame update
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

    public override bool DidWin()
    {
        return (leftPressed && rightPressed && jumpPressed);
    }

    public override bool DidLose()
    {
        return false;
    }
}
