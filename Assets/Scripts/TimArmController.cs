using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimArmController : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform armTransform; 

    void Start()
    {
        armTransform = transform.Find("Hoodie/Arm");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetArmAndCollectableToGripPosition()
    {
        armTransform.localEulerAngles += new Vector3(0, 0, 90);
        //c.transform.localEulerAngles += new Vector3(0, 0, -90);
    }
}
