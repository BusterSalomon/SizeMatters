using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level5Manager : LevelManager
{
    public bool WinConditionMet = false;
    public bool LoseConditionMet = false;

    public void Start()
    {
    }
    public override bool DidWin()
    {

        return WinConditionMet;
    }

    public override bool DidLose()
    {
        return  LoseConditionMet;
    }
}

