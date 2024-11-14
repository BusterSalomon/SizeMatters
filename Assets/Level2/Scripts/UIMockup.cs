using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class UIMockup : MonoBehaviour
{
    private void Start()
    {
        
    }

    public void LogWin()
    {
        UnityEngine.Debug.Log("You won!");
    }

    public void LogLose()
    {
        UnityEngine.Debug.Log("You lost!");
    }
}
