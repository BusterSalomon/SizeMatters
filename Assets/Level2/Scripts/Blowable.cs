using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blowable : MonoBehaviour
{
    public bool IsGettingBlownAt = false;
    public int blowDirection { get; set; }
       
    public void StartBlowing(int xDirection)
    {
        blowDirection = xDirection;
        IsGettingBlownAt = true;
    }
    public void StopBlowing ()
    {
        IsGettingBlownAt = false;
    }

}
