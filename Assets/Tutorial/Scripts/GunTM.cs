using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTM : TutorialManager
{
    private bool collected;
    private bool didCollectOnce;
    private bool didReleaseOnce;
    private bool didReloadOnce;
    private bool gunUsed;

    void Start()
    {
        collected = false;
        gunUsed = false;
        didCollectOnce = false;
        didReleaseOnce = false;
        didReloadOnce = false;
     }

    protected override void Update()
    {
        base.Update();

        if (collected && Input.GetKey(KeyCode.N))
        {
            gunUsed = true;
        }

        if (collected && Input.GetKey(KeyCode.R))
        {
            didReloadOnce = true;
        }
    }

    public override bool DidComplete()
    {
        return didCollectOnce && didReleaseOnce && didReloadOnce && gunUsed;
    }

    public void DidCollectGun ()
    {
        didCollectOnce = true;
        collected = true;
    }

    public void DidReleaseGun ()
    {
        didReleaseOnce = true;
        collected = false;
    }
}
