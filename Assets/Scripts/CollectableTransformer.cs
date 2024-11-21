using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableTransformer : MonoBehaviour
{   
    public void RotateDownBy90(string collectableType)
    {
        Transform collectableTransform = GetCollectableTransformByType(collectableType);
        collectableTransform.localEulerAngles -= new Vector3(0, 0, 90);
    }

    private Transform GetCollectableTransformByType(string collectableType)
    {
        return Collectable.FindCollectableGameObjectByType(collectableType).transform;
    }

    
}
