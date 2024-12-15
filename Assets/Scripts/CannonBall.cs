using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cannonball : MonoBehaviour
{
    public UnityEvent IWasFired;
    public UnityEvent IWasLoaded;
    public UnityEvent ILanded;
    private CannonMechanics cannon;
    private bool _WasFired = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float timeSinceFire = Time.time - fireTime;
        int barrelID = -1;
        if (cannon)
        {
            Transform br = cannon.transform.Find("BarrelRotationWrapper");
            Transform barrel = br.Find("Barrel");
            barrelID = barrel.gameObject.GetInstanceID();
        }
        if (_WasFired && cannon && collision.gameObject.GetInstanceID() != barrelID) // removed && collision.gameObject.CompareTag("Ground")
        {
            cannon.HandleCannonballDidLand(this);
            ILanded.Invoke();
            _WasFired = false;
        }
    }

    public void HandleOnLoad()
    {
        IWasLoaded.Invoke();
    }

    private float fireTime = -1f; 
    public void WasFired(CannonMechanics cM)
    {
        fireTime = Time.time;
        _WasFired = true;
        cannon = cM;
        IWasFired.Invoke();
    }

}
