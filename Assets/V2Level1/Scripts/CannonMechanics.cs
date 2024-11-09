using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class CannonMechanics : MonoBehaviour
{
    // Start is called before the first frame update
    private Collector cannonballCollector;
    private Button button;
    private Rigidbody2D rb;
    private bool btnPressed = false;
    public float CannonAngularVelocityOnBtnPress = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cannonballCollector = GetComponent<Collector>();
        button = FindObjectOfType<Button>();
        button.addListener(BtnListener);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (btnPressed)
        {
            rb.angularVelocity = CannonAngularVelocityOnBtnPress;
        } 
    }

    public void BtnListener (bool btnStatus)
    {
        if (btnStatus)
        {
            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
        } else
        {
            rb.isKinematic = false;
            rb.constraints = RigidbodyConstraints2D.None; 
        }
       
        btnPressed = btnStatus;
    }
}
