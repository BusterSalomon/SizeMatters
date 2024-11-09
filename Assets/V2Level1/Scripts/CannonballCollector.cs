using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonballCollector : Collector
{
    // TODO: make it only able to collect when the cannonball rolls into the barrel
    public override bool CollectCondition()
    {
        return true;
    }
}
