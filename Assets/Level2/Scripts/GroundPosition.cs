using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPosition : MonoBehaviour
{
    // Start is called before the first frame update
    Camera main_camera;
    void Start()
    {
        // Set size and position
        main_camera = Camera.main;
        float camera_height = 2f * main_camera.orthographicSize;
        float camera_width = main_camera.aspect * camera_height;
        Debug.Log($"Height: {camera_height}");
        Debug.Log($"Width: {camera_width}");

        float target_width = camera_width;
        float target_height = 1f;

        transform.position = new Vector3(0, -camera_height/2 + target_height/2, 0);
        transform.localScale = new Vector3(target_width, target_height, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
