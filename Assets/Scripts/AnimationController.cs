using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private Animator anim;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float velX = rb.velocity.x;
        if (velX == 0) anim.SetBool("isRunning", false);
        else anim.SetBool("isRunning", true);
    }

    public void PlayTakeOfAnim()
    {
        //anim.SetTrigger("takeOf");
    }

    public void PlayJumpAnim()
    {
        anim.SetBool("isJumping", true);
    }

    public void PlayLandAnim()
    {
        anim.SetBool("isJumping", false);
    }

}
