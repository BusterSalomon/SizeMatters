using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class CannonMechanics : MonoBehaviour
{
    // Start is called before the first frame update
    private Collector cannonballCollector;
    private Transform barrelRotationWrapperTransform;
    private bool fireInitiated = false;
    private float fireButtonInitiatedTime =-1f;
    private Animator barrelAnim;
    private Animator explosionAnim;
    private bool cannonballRotated = false;

    [Header("Fire")]
    public int MaxForce = 150;
    public int TimeToReachMaxForce = 5;
    private int FireForceScaler;
    public UnityEvent OnFireInitiated;
    public UnityEvent<int, int> OnFireForceUpdated;
    public UnityEvent OnFire;
    public UnityEvent OnCannonballLand;
    private int maxForce;

    [Header("Aiming")]
    public float AimTickTimeInterval = 0.07f;
    public float AimTickDistance = 2f;
    public bool ConsiderBoundaries = true;
    public float MinRotation = -10;
    public float MaxRotation = 90;
    private float lastAimTickTime = -1f;

    void Start()
    {
        lastAimTickTime = Time.time - AimTickTimeInterval; // Allow to aim at the beginning

        // Retrive components
        cannonballCollector = GetComponent<Collector>();
        barrelRotationWrapperTransform = transform.Find("BarrelRotationWrapper");
        Transform barrelTransform = barrelRotationWrapperTransform.Find("Barrel");
        barrelAnim = barrelTransform.GetComponent<Animator>();
        explosionAnim = barrelTransform.Find("Explosion").Find("ExpAnimator").GetComponent<Animator>();
        
        // Calculate
        FireForceScaler = MaxForce / TimeToReachMaxForce;
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
        int fireForce = HandleFireForceIO();
        if (fireForce > 0) Fire(fireForce);

    }

    private enum Direction
    {
        UP=1,
        DOWN=-1
    }

    public int FireForce = 0;
    public bool ReadyToFire = false;
    /// <summary>
    /// Calculates the fire force as a linear funciton of the time the fire button was pressed (input), and invokes unity events on updates (output).
    /// </summary>
    /// <returns>The force as an integer</returns>
    private int HandleFireForceIO()
    {
        
        // Get input
        bool fire = Input.GetKey(KeyCode.F);

        // Press
        if (fire && !fireInitiated)
        {
            fireInitiated = true;
            fireButtonInitiatedTime = Time.time;
            OnFireInitiated.Invoke();
        }
        
        // Hold
        else if (fire && fireInitiated)
        {
            float pressTime = Time.time - fireButtonInitiatedTime;
            float pressTimeClamped = Mathf.Clamp(pressTime, 0, TimeToReachMaxForce);
            int newFireForce = (int)Mathf.Floor(pressTimeClamped * FireForceScaler);
            if (newFireForce != FireForce)
            {

                FireForce = newFireForce;
                OnFireForceUpdated.Invoke(FireForce, MaxForce);
            }
        }

        // Release
        else if (!fire && fireInitiated)
        {
            fireInitiated = false;
            int fireForce = FireForce;
            FireForce = 0;
            OnFire.Invoke();
            return fireForce;
        }

        return 0;
        
    }

    private float deltaAngleTotal = 0f;
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
            float deltaAngle = (int)direction * AimTickDistance;
            deltaAngleTotal += deltaAngle;
            Vector3 deltaVector = new Vector3(0, 0, deltaAngle);
            Vector3 newBarrelAngle = barrelRotationWrapperTransform.localEulerAngles + deltaVector;
            if (IsWithinBoundaries(newBarrelAngle.z)) {
                barrelRotationWrapperTransform.localEulerAngles = newBarrelAngle;
                
                GameObject cannonBall = getCannonball();
                if (cannonBall)
                { 
                   cannonBall.transform.localEulerAngles -= deltaVector;
                }

                //AudioManager.instance.Play("cannon_tic");
            } 

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
        zAngle = AngleToMP180(zAngle); // maps to [180; -180]
        return (zAngle > MinRotation && zAngle < MaxRotation);
    }


    /// <summary>
    /// Converts the angle domain to [180; -180]
    /// </summary>
    /// <param name="angle"></param>
    /// <returns></returns>
    private float AngleToMP180 (float angle)
    {
        if (angle > 180) return angle -= 360;
        else return angle;
    }

    private GameObject getCannonball()
    {
        return cannonballCollector.GetCollectableIfCollected()? cannonballCollector.GetCollectableIfCollected().gameObject : null;
    }

    public Vector2 ForceDirection;
    /// <summary>
    /// Fires the cannon ball if loaded
    /// </summary>
    public void Fire (int FireForce)
    {
        Debug.Log($"Fire called! {FireForce}");
        GameObject cannonballGO = getCannonball();
        if (cannonballGO != null)
        {
            Cannonball cannonball = cannonballGO.GetComponent<Cannonball>();

            cannonballCollector.Release();

            // Get rigidbody to apply force to
            Rigidbody2D rb = cannonballGO.GetComponent<Rigidbody2D>();

            // Get force angle
            float angle = GetAngleRelativeToScale(barrelRotationWrapperTransform.localEulerAngles.z);
            
            // Get direction vector
            ForceDirection = getDiretionVectorFromDegAngle(angle);
            Debug.Log($"{name} fires in angle: {angle} -> {ForceDirection}");

            // FIRE!
            rb.AddForce(FireForce*ForceDirection, ForceMode2D.Impulse);
            cannonball.WasFired(this);
            AudioManager.instance.Play("cannon_shot");
        }
        barrelAnim.SetTrigger("fired");
        explosionAnim.SetTrigger("fired");
    }

    public void HandleCannonballDidLand(Cannonball cannonball)
    {
        OnCannonballLand.Invoke();
    }

    public void HandleOnCollect(string type)
    {
        GameObject collectableGO = Collectable.FindCollectableGameObjectByType(type);
        Cannonball cannonball = collectableGO.GetComponent<Cannonball>();
        cannonball.HandleOnLoad();
    }

    /// <summary>
    /// If the cannon is flipped, the angle should be adjustet
    /// </summary>
    /// <param name="angle"></param>
    /// <returns>The adjustet angle</returns>
    private float GetAngleRelativeToScale (float angle)
    {
        float scaleX = transform.localScale.x;
        if (Mathf.Sign(scaleX) > 0) return angle;
        else return 180 - angle;
    }


    private Vector2 getDiretionVectorFromDegAngle (float angleDeg)
    {
        float angleRad = Mathf.Deg2Rad * angleDeg;
        float cosAngle = Mathf.Cos(angleRad);
        float sinAngle = Mathf.Sin(angleRad);
        return new Vector2(cosAngle, sinAngle);
    }


    // --- METHODS NOT IMPLEMENTED YET
    /// <summary>
    /// NOT IMPLEMENTED YET. Should rotate the cannonball when loaded.
    /// </summary>
    /// <param name="c"></param>
    private void RotateCannonballOnceIfLoaded(Collectable c)
    {
        if (c && !cannonballRotated)
        {
            //Vector3 barrelAngles = barrelRotationWrapperTransform.eulerAngles;
            //Debug.Log(barrelAngles);
            //c.transform.localEulerAngles = new Vector3(barrelAngles.x, barrelAngles.y, AngleToMP180(barrelAngles.z)-90);
            //cannonballRotated = true;
        }
    }

    /// <summary>
    /// NOT IMPLEMENTED YET
    /// </summary>
    /// <param name="cannonballTransform"></param>
    private void RotateCannonballOnFire(Transform cannonballTransform)
    {
        // cannonballTransform.localEulerAngles += new Vector3(0, 0, 90-deltaAngleTotal);
    }

    private void InitializeState()
    {
        cannonballRotated = false;
    }
}
