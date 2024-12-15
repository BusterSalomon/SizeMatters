using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTM : TutorialManager
{
    private bool virusFiredOnce;
    private bool timFiredOnce;

    void Start()
    {
        virusFiredOnce = false;
        timFiredOnce = false;
    }

    public override bool DidComplete()
    {
        return virusFiredOnce && timFiredOnce;
    }

  
    public void VirusFired ()
    {
        Debug.Log("virus");
        virusFiredOnce = true;
    }

    public void TimFired ()
    {
        Debug.Log("tim");
        timFiredOnce = true;
    }
}
