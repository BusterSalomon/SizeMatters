using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonballCollector : Collector
{
    private bool buttonDidRealease = false;

    public void setButtonDidRealease ()
    {
        Debug.Log($"{name} was notified of realese");
        buttonDidRealease = true;
    }

    public void resetButtonDidRealease()
    {
        Debug.Log($"{name} was notified of press");
        buttonDidRealease = false;
    }

    // TODO: make it only able to collect when the cannonball rolls into the barrel
    private float cannonballReleaseTime = -1;
    public float cannonballReleaseToCollectTime = 3f;
    
    public override bool CollectCondition()
    {
        if (cannonballReleaseTime != -1)
        {
            float timeSinceRelease = Time.time - cannonballReleaseTime;
            return timeSinceRelease > cannonballReleaseToCollectTime;
        }
        else return true;
        
    }

    public override bool ReleaseCondition()
    {
        cannonballReleaseTime = Time.time;
        bool didRelease = false;
        if (buttonDidRealease)
        {
            didRelease = true;
            buttonDidRealease = false;
        }
        return didRelease;
    }
}
