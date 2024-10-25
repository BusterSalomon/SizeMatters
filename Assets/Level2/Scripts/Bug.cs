using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : EnemyBase
{
    public override List<string> GetTargetTags()
    {
        return new List<string> { "Character" };
    }
}
