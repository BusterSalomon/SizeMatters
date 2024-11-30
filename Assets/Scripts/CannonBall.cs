using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cannonball : MonoBehaviour
{
    public UnityEvent IWasFired;
    public UnityEvent IWasLoaded;
    private CannonMechanics cannon;
    private bool _WasFired = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_WasFired && collision.gameObject.CompareTag("Ground"))
        {
            cannon.HandleCannonballDidLand(this);
            _WasFired = false;
            Debug.Log("Did collide!");
        }
        
    }

    public void HandleOnLoad()
    {
        if (_WasFired)
        IWasLoaded.Invoke();
    }

    public void WasFired(CannonMechanics cM)
    {
        _WasFired = true;
        cannon = cM;
    }

}
