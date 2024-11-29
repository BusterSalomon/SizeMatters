using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCameraFollow : MonoBehaviour
{
   
    public float FollowSpeed = 2f;
    public float yOffset = 1f;
    public float xOffset = 0f;
    public float defaultXOffset = -5f;
    private bool cannonLoaded = false;
    public Transform target;
    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
        Vector3 newPos = new Vector3(target.position.x + defaultXOffset + xOffset, target.position.y + yOffset, -10f);
        transform.position = newPos;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(target.position.x + defaultXOffset + xOffset, target.position.y + yOffset, -10f);
        transform.position = Vector3.Lerp(transform.position, newPos, FollowSpeed*Time.deltaTime);
    }


    public void SetXOffset(float offset)
    {
        xOffset = offset;
    }

    public void SetCameraSize(float size)
    {
        cam.orthographicSize = size;
    }

    public void HandleOnCharacterChangeDirection(int direction)
    {
        if (!cannonLoaded)
        {
            float screenWidth = GetScreenWidth();
            xOffset = direction * screenWidth / 12;
        }
    } 

    private float GetScreenWidth ()
    {
        return 2f * cam.orthographicSize * cam.aspect;
    }

    public void HandleOnCannonLoadCharacter(string _, int cannonCollectorID)
    {
        Collector cannonCollector = Collector.GetCollectorByUID(cannonCollectorID);
        float dir = Mathf.Sign(cannonCollector.transform.localScale.x);
        xOffset = dir * GetScreenWidth() / 4;
        cannonLoaded = true;
    }

    public void HandleOnCannonFireCharacter (string _, int cannonCollectorID)
    {
        Collector cannonCollector = Collector.GetCollectorByUID(cannonCollectorID);
        CannonMechanics cM = cannonCollector.GetComponent<CannonMechanics>();
        int dir = (int)Mathf.Sign(cM.ForceDirection.x);
        cannonLoaded = false;
        HandleOnCharacterChangeDirection(dir);
    }

}
