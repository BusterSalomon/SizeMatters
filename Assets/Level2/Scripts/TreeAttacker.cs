using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeAttacker : EnemyBase
{
    public override List<string> GetTargetTags()
    {
        return new List<string> { "Tree" };
    }
}
