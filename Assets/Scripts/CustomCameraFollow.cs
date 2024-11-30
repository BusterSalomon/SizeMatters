using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class CustomCameraFollow : MonoBehaviour
{
   
    public float FollowSpeed = 2f;
    public float yOffset = 1f;
    public float xOffset = 0f;
    public float defaultXOffset = -5f;
    public float CameraSizeDeltaOnCharacterCannonLoad = 10f;
    public float CameraSizeDeltaOnBugCannonLoad = 10f;
    public float xOffsetOnCharacterCannonLoadRelativeToScreenWidth = 0f;
    public float yOffsetOnCharacterCannonLoadRelativeToScreenHeight = 0f;
    public Transform target;
    private Camera cam;
    private bool cameraShiftOnCharacterDirectionChangeDisabled = false;
    private float oldCameraSize;
    private bool cameraAdjustet = false;
    private bool freezeTarget = false;
    private Transform frozenTarget;


    private void Start()
    {
        cam = GetComponent<Camera>();
        Vector3 newPos = new Vector3(target.position.x + defaultXOffset + xOffset, target.position.y + yOffset, -10f);
        transform.position = newPos;
    }

    // Update is called once per frame
    void Update()
    {
        // If the camera is frozen, follow the frozenTarget instead of the character
        Transform currentTarget = freezeTarget ? frozenTarget : target;

       
        Vector3 newPos = new Vector3(currentTarget.position.x + defaultXOffset + xOffset, currentTarget.position.y + yOffset, -10f);
        transform.position = Vector3.Lerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
        
    }


    private float oldXOffset;
    public void SetXOffset(float offset)
    {
        oldXOffset = xOffset;
        xOffset = offset;
    }

    private float oldYOffset;
    public void SetYOffset(float offset)
    {
        oldYOffset = yOffset;
        yOffset = offset;
    }

    public void ResetXOffset()
    {
        yOffset = oldXOffset;
    } 

    public void ResetYOffset()
    {
        yOffset = oldYOffset;
    }

    public void SetCameraSize(float size)
    {
        if (!cameraAdjustet)
        {
            oldCameraSize = cam.orthographicSize;
            cam.orthographicSize = size;
            cameraAdjustet = true;
        }
        
    }

    public void ResetCameraSize()
    {
        cam.orthographicSize = oldCameraSize;
        cameraAdjustet = false;
    }

    private bool cameraSetFirst = false;
    /// <summary>
    /// Resets all camera settings to their previous values
    /// </summary>
    public void ResetAllCameraSettings()
    {
        if (cameraSetFirst)
        {
            ResetCameraSize();
            ResetXOffset();
            ResetYOffset();
            ThawCamera();
        }
        else cameraSetFirst = true;
    }

    public void IncrementCameraSize(float delta)
    {
        if (!cameraAdjustet)
        {
            oldCameraSize = cam.orthographicSize;
            cam.orthographicSize += delta;
            cameraAdjustet = true;
        }   
    }

    public void HandleOnCharacterChangeDirection(int direction)
    {
        if (!cameraShiftOnCharacterDirectionChangeDisabled)
        {
            float screenWidth = GetScreenWidth();
            xOffset = direction * screenWidth / 12;
        }
    } 

    private float GetScreenHeight ()
    {
        return 2f * cam.orthographicSize;
    }

    private float GetScreenWidth ()
    {
        return GetScreenHeight() * cam.aspect;
    }

    /// <summary>
    /// Set camera settings for when the character is collected
    /// </summary>
    /// <param name="_"></param>
    /// <param name="cannonCollectorID"></param>
    public void HandleOnCannonLoadCharacter(string _, int cannonCollectorID)
    {
        ResetAllCameraSettings();
        Collector cannonCollector = Collector.GetCollectorByUID(cannonCollectorID);
        float dir = Mathf.Sign(cannonCollector.transform.localScale.x);
        
        SetXOffset(dir * GetScreenWidth() * xOffsetOnCharacterCannonLoadRelativeToScreenWidth);
        SetYOffset(GetScreenWidth() * yOffsetOnCharacterCannonLoadRelativeToScreenHeight);
        IncrementCameraSize(CameraSizeDeltaOnCharacterCannonLoad);
        
        // Disables movement for
        cameraShiftOnCharacterDirectionChangeDisabled = true;
    }


    public void HandleOnCannonFireCharacter (string _, int cannonCollectorID)
    {
        Collector cannonCollector = Collector.GetCollectorByUID(cannonCollectorID);
        CannonMechanics cM = cannonCollector.GetComponent<CannonMechanics>();

        // reset camera settings
        ResetAllCameraSettings();
        IncrementCameraSize(5);
        cameraShiftOnCharacterDirectionChangeDisabled = false;
        int dir = (int)Mathf.Sign(cM.ForceDirection.x);
        HandleOnCharacterChangeDirection(dir);
    }


    // ---- Methods for freezing the camera
    public void FreezeCamera()
    {
        if (!freezeTarget)
        {
            // Create a new GameObject to hold the frozen position
            GameObject frozenObject = new GameObject("FrozenTarget");
            frozenObject.transform.position = target.transform.position;

            // Assign the frozen target
            frozenTarget = frozenObject.transform;
            freezeTarget = true;
        }
    }

    public void ThawCamera()
    {
        if (freezeTarget)
        {
            // Destroy the frozen target GameObject
            if (frozenTarget != null)
            {
                Destroy(frozenTarget.gameObject);
                frozenTarget = null;
            }
            freezeTarget = false;
        }
    }

}
