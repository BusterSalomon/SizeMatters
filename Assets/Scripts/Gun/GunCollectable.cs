using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCollectable : Collectable
{
    private Animator anim;
    private AudioManager audioManager;

    private void Start()
    {
        anim = GetComponent<Animator>();
        audioManager = AudioManager.instance;
    }
    private void Update()
    {
        if (IsCollected && KeyWasPressed(KeyCode.N))
        {
            // Do shooting logic
            // ...

            // Do animation
            anim.SetTrigger("fire");
            //anim.ResetTrigger("fire");
            // Do audio
            // audioManager.Play(...)
        } 
        if (IsCollected && KeyWasReleased(KeyCode.N))
        {
            // Optional on release logic
        }
    }
}
