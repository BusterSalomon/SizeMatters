using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class CannonMechanics : MonoBehaviour
{
    // Start is called before the first frame update
    private Collector cannonballCollector;
    private Transform barrelTransform;
    public int FireForceScaler = 7;
    private bool lastFirePressed = false;
    private float fireButtonInitiatedTime =-1f;
    private Animator anim;

    [Header("Aiming")]
    public float AimTickTimeInterval = 0.07f;
    public float AimTickDistance = 2f;
    public bool ConsiderBoundaries = true;
    public float MinRotation = -10;
    public float MaxRotation = 90;
    private float lastAimTickTime = -1f;

    void Start()
    {
        cannonballCollector = GetComponent<Collector>();
        barrelTransform = transform.Find("Barrel");
        lastAimTickTime = Time.time - AimTickTimeInterval; // Allow to aim at the beginning
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // ---- Get user input
        // Up / Down
        bool up = Input.GetKey(KeyCode.UpArrow);
        bool down = Input.GetKey(KeyCode.DownArrow);

        // XOR
        if (up ^ down)
        {
            Aim(up ? Direction.UP : Direction.DOWN);
        }

        // F (fire) (on realese) -> int
        int fireForce = GetFireForceFromInput();
        if (fireForce > 0) Fire(fireForce);

    }

    private enum Direction
    {
        UP=1,
        DOWN=-1
    }

    /// <summary>
    /// Calculates the fire force as a linear funciton of the time the fire button was pressed
    /// </summary>
    /// <returns>The force as an integer</returns>
    private int GetFireForceFromInput()
    {
        int fireForce = 0;
        
        // Get input
        bool fire = Input.GetKey(KeyCode.F);

        // Press
        if (fire && !lastFirePressed)
        {
            lastFirePressed = true;
            fireButtonInitiatedTime = Time.time;
        }
        // Release
        else if (!fire && lastFirePressed)
        {
            float pressTime = Time.time - fireButtonInitiatedTime;
            float pressTime0to5 = Mathf.Clamp(pressTime, 0, 5);
            fireForce = (int)Mathf.Floor(pressTime0to5 * FireForceScaler);
            lastFirePressed = false;
        }

        return fireForce;
    }

    
    /// <summary>
    /// Aims the barrel in discrete steps
    /// </summary>
    /// <param name="direction"></param>
    private void Aim(Direction direction)
    {
        float currentTime = Time.time;

        // Aim again
        if (currentTime - lastAimTickTime > AimTickTimeInterval)
        {
            Vector3 newBarrelAngle = barrelTransform.eulerAngles + new Vector3(0, 0, (int)direction * AimTickDistance);
            if (IsWithinBoundaries(newBarrelAngle.z)) barrelTransform.localEulerAngles = newBarrelAngle;

            // Set time
            lastAimTickTime = Time.time;
        }

    }
    /// <summary>
    /// Checks if the zAngle is within the boundaries. Can be disabled using the ConsiderBoundaries property.
    /// </summary>
    /// <param name="zAngle"></param>
    /// <returns>the result as a bool</returns>
    private bool IsWithinBoundaries (float zAngle)
    {
        if (!ConsiderBoundaries) return true; // early return if boundary-check is disabled
        if (zAngle > 180) zAngle -= 360; // maps to [180; -180]
        return (zAngle > MinRotation && zAngle < MaxRotation);
    }

    /// <summary>
    /// Fires the cannon ball if loaded
    /// </summary>
    public void Fire (int FireForce)
    {
        Debug.Log($"Fire called! {FireForce}");
        GameObject cannonBall = cannonballCollector.GetCollectableIfCollected()?.gameObject;
        if (cannonBall != null)
        {
            cannonballCollector.Release();

            // Get rigidbody to apply force to
            Rigidbody2D rb = cannonBall.GetComponent<Rigidbody2D>();

            // Get force angle
            float angleCannon = transform.eulerAngles.z;
            float angleBarrel = barrelTransform.eulerAngles.z;
            float angle = barrelTransform.eulerAngles.z;
            
            // Get direction vector
            Vector2 dirVec = getDiretionVectorFromDegAngle(angle);

            // FIRE!
            rb.AddForce(FireForce*dirVec, ForceMode2D.Impulse);
        }
        anim.SetTrigger("cannonFired");
    }

    private Vector2 getDiretionVectorFromDegAngle (float angleDeg)
    {
        float angleRad = Mathf.Deg2Rad * angleDeg;
        float cosAngle = Mathf.Cos(angleRad);
        float sinAngle = Mathf.Sin(angleRad);
        return new Vector2(cosAngle, sinAngle);
    }
}
