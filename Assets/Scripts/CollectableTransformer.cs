using UnityEngine;

public class CollectableTransformer : MonoBehaviour
{   
    public void RotateDownBy90(string collectableType)
    {
        Transform collectableTransform = GetCollectableTransformByType(collectableType);
        collectableTransform.localEulerAngles -= new Vector3(0, 0, 90);
    }

    public void Flip(string collectableType, int collectorUID)
    {
        //Debug.Log("Flip called!");
        //Transform collectable = GetCollectableTransformByType(collectableType);
        //Collector collector = Collector.GetCollectorByUID(collectorUID);
        //float collectorLocalScaleX = collector.transform.localScale.x;

        //FlipDirectChildWithSpriteRenderers(collectable, collectorLocalScaleX);
        
    }

    void FlipDirectChildWithSpriteRenderers(Transform parent, float localScaleX)
    {
        foreach (Transform child in parent)
        {
            SpriteRenderer spriteRenderer = child.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                // Flip the local scale of the child
                Debug.Log($"Flipped: {spriteRenderer.name}");
                Vector3 lc = child.localScale;
                child.localScale = new Vector3(localScaleX * lc.x, lc.y, lc.z);
            }
        }
    }

    // Function to flip all SpriteRenderers in the hierarchy
    public static void FlipAllSpriteRenderers(Transform root)
    {
        Debug.Log($"Checking: {root.name}");
        // Check if the current GameObject has a SpriteRenderer
        SpriteRenderer spriteRenderer = root.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            // Flip the SpriteRenderer on the X-axis
            //spriteRenderer.flipX = !spriteRenderer.flipX;
            Vector3 lc = spriteRenderer.transform.localScale;
            spriteRenderer.transform.localScale = new Vector3(-1 * lc.x, lc.y, lc.z);

            Debug.Log($"Did flip: {root.name}");
        }

        // Recursively check all child transforms
        foreach (Transform child in root)
        {
            FlipAllSpriteRenderers(child);
        }
    }

    private Transform GetCollectableTransformByType(string collectableType)
    {
        return Collectable.FindCollectableGameObjectByType(collectableType).transform;
    } 
}
