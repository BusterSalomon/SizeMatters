using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.U2D.Animation;
using UnityEngine;

public class CustomCameraFollow : MonoBehaviour
{
    public Transform target;
    public float FollowSpeed = 2f;

    [Header("Cannon Load")]
    public float xOffsetFactorCannonLoad;
    public float yOffsetFactorCannonLoad;
    public float CameraSizeCannonLoad;
    
    [Header("Movement")]
    public float xOffsetFactorMovement;
    public float yOffsetFactorMovement;
    public float CameraSizeMovement;

    [Header("Flying")]
    public float xOffsetFactorFlying;
    public float yOffsetFactorFlying;
    public float CameraSizeFlying;

    [Header("Landing")]
    private float xOffsetFactorLanding;
    private float yOffsetFactorLanding;
    private float cameraSizeLanding;

    [Header("Cameras")]
    public Camera cam;
    public Camera bugCam;

    // Private states
    private float yOffset;
    private float xOffset;
    private bool cameraShiftOnCharacterDirectionChangeDisabled = false;
    private int cannonDirection;
    private bool bugIsLoaded;


    private void Start()
    {
        bugIsLoaded = false;
        SetActiveCamera(cam);
        xOffsetFactorLanding = xOffsetFactorMovement;
        yOffsetFactorLanding = yOffsetFactorMovement;
        cameraSizeLanding = CameraSizeMovement;
       
        cam.orthographicSize = CameraSizeMovement;
        xOffset = 0f;
        yOffset = GetScreenHeight() * yOffsetFactorMovement;
        PursueCameraTarget();
    }

    // Update is called once per frame
    void Update()
    {
        if (bugIsLoaded)
        {
            SetActiveCamera(bugCam);
        } else
        {
            SetActiveCamera(cam);
            PursueCameraTarget();
        }
    }

    public void SetActiveCamera(Camera activeCamera)
    {
        // Activate the selected camera and deactivate others
        cam.enabled = activeCamera == cam;
        bugCam.enabled = activeCamera == bugCam;
    }

    private void PursueCameraTarget()
    {
        Vector3 newPos = new Vector3(target.position.x + xOffset, target.position.y + yOffset, -10f);
        transform.position = Vector3.Lerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
    }

    private float GetScreenHeight()
    {
        return 2f * cam.orthographicSize;
    }

    private float GetScreenWidth()
    {
        return GetScreenHeight() * cam.aspect;
    }


    public void HandleOnCharacterChangeDirection(int direction)
    {
        if (!cameraShiftOnCharacterDirectionChangeDisabled)
        {
            cam.orthographicSize = CameraSizeMovement;
            xOffset = direction * GetScreenWidth() * xOffsetFactorMovement;
            yOffset = GetScreenHeight() * yOffsetFactorMovement;
        }
    } 

    public void HandleOnCannonLoadCharacter(string _, int cannonCollectorID)
    {
        Collector cannonCollector = Collector.GetCollectorByUID(cannonCollectorID);
        cannonDirection = (int)Mathf.Sign(cannonCollector.transform.localScale.x);

        cam.orthographicSize = CameraSizeCannonLoad;
        xOffset = cannonDirection * GetScreenWidth() * xOffsetFactorCannonLoad;
        yOffset = GetScreenHeight() * yOffsetFactorCannonLoad;

        // Disables movement for
        cameraShiftOnCharacterDirectionChangeDisabled = true;
    }
    public void HandleOnCannonFireCharacter (string _, int cannonCollectorID)
    {
        cameraShiftOnCharacterDirectionChangeDisabled = false;
        cam.orthographicSize = CameraSizeFlying;
        xOffset = cannonDirection * GetScreenWidth() * xOffsetFactorFlying;
        yOffset = GetScreenHeight() * yOffsetFactorFlying;
    }

    public void HandleOnCannonballLand ()
    {
        cam.orthographicSize = cameraSizeLanding;
        xOffset = cannonDirection * GetScreenWidth() * xOffsetFactorLanding;
        yOffset = GetScreenHeight() * yOffsetFactorLanding;
    }

    public void HandleOnCannonLoadBug ()
    {
        Debug.Log("Handle this!");
        bugIsLoaded = true;
    }


    // ---- Methods for freezing the camera
    //public void FreezeCamera()
    //{
    //    if (!freezeTarget)
    //    {
    //        // Create a new GameObject to hold the frozen position
    //        GameObject frozenObject = new GameObject("FrozenTarget");
    //        frozenObject.transform.position = target.transform.position;

    //        // Assign the frozen target
    //        frozenTarget = frozenObject.transform;
    //        freezeTarget = true;
    //    }
    //}

    //public void ThawCamera()
    //{
    //    if (freezeTarget)
    //    {
    //        // Destroy the frozen target GameObject
    //        if (frozenTarget != null)
    //        {
    //            Destroy(frozenTarget.gameObject);
    //            frozenTarget = null;
    //        }
    //        freezeTarget = false;
    //    }
    //}


    /*
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
     
     
     
     */

}
