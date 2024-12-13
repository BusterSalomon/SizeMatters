using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Level4Collector : Collector
{
    public override bool ReleaseCondition(Collectable collectable)
    {
        // bool canRelease = Time.time - btnPressedTime > collectToReleaseDelay;
        // return Input.GetKey(KeyCode.R) && canRelease;
        return Input.GetKey(KeyCode.V);
    }
}
