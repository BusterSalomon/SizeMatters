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
    private float cannonballReleaseTime = 0f;
    public float cannonballReleaseToCollectTime = 3f;
    
    public override bool CollectCondition(Collectable collectable)
    {
        float timeSinceRelease = Time.time - cannonballReleaseTime;
        bool releaseToCollectTimeConstraintSatisfied = timeSinceRelease > cannonballReleaseToCollectTime;
        if (releaseToCollectTimeConstraintSatisfied)
        {
            switch (collectable.CollectableType)
            {
                case "Bug":
                    Blowable blowable = collectable.gameObject.GetComponent<Blowable>();
                    bool blowDirectionIsTowardsCannonEntry = ((int)Mathf.Sign(transform.localScale.x)) != blowable.blowDirection;
                    return blowable.IsGettingBlownAt && blowDirectionIsTowardsCannonEntry;
                default:
                    return true;

            }
        }
        else return false;
    }

    public override bool ReleaseCondition(Collectable collectable)
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
