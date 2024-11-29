using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoliceHealthbar : EnemyHealthbar
{
    public float healthXposition = 0.25f;
    // Update is called once per frame
    void Update()
    {
        Slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + new Vector3(healthXposition,HealthbarHeight,0));
    }
}
