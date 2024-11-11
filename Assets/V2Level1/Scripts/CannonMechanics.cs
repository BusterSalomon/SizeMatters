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
    private Transform barrelTransform;
    private bool btnPressed = false;
    public float CannonAngularVelocityOnBtnPress = 5f;
    public float FireForce = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cannonballCollector = GetComponent<Collector>();
        barrelTransform = transform.Find("Barrel");
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
    /// <summary>
    /// Fires the cannon ball if loaded
    /// </summary>
    public void Fire ()
    {
        Debug.Log("Fire called!");
        GameObject cannonBall = cannonballCollector.CollectableCollected.gameObject;
        if (cannonBall != null)
        {
            // Get rigidbody to apply force to
            Rigidbody2D rb = cannonBall.GetComponent<Rigidbody2D>();

            // Get force angle
            float angleCannon = transform.eulerAngles.z;
            float angleBarrel = barrelTransform.eulerAngles.z;
            float angle = barrelTransform.eulerAngles.z;
            Debug.Log($"Angle C: {angleCannon}");
            Debug.Log($"Angle B: {angleBarrel}");

            Debug.Log($"Angle: {angle}");
            
            // Get direction vector
            Vector2 dirVec = getDiretionVectorFromDegAngle(angle);
            Debug.Log($"Vec: {dirVec}");

            // FIRE!
            rb.AddForce(FireForce*dirVec, ForceMode2D.Impulse);
        }
    }

    private Vector2 getDiretionVectorFromDegAngle (float angleDeg)
    {
        float angleRad = Mathf.Deg2Rad * angleDeg;
        float cosAngle = Mathf.Cos(angleRad);
        float sinAngle = Mathf.Sin(angleRad);
        return new Vector2(cosAngle, sinAngle);
    }
}
