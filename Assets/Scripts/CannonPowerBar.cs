using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonPowerBar : MonoBehaviour
{

    private GameObject bar;
    private Transform indicatorTransform;
    private float barHeight;
    private float indicatorPosition = 0;
    private Vector3 initialIndicatorPos;

    // Start is called before the first frame update
    void Start()
    {
        bar = transform.GetChild(0).gameObject;
        indicatorTransform = bar.transform.GetChild(0);
        initialIndicatorPos = indicatorTransform.position;
        barHeight = bar.GetComponent<SpriteRenderer>().bounds.size.y;
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Reveals & initializes the power bar
    /// </summary>
    public void HandleOnFireInitiated ()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Hides the power bar
    /// </summary>
    public void HandleOnFire ()
    {
        // ...
        indicatorPosition = 0;
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Updates the indicator position
    /// </summary>
    /// <param name="newFireForce"></param>
    public void HandleOnFireForceUpdated (int newFireForce, int maxFireForce)
    {
        indicatorPosition = (barHeight-0.7f)/maxFireForce * newFireForce;
        indicatorTransform.position = new Vector3(initialIndicatorPos.x, initialIndicatorPos.y + indicatorPosition, initialIndicatorPos.z);
    }




}
