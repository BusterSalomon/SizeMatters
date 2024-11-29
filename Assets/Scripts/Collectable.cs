using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Collectable : MonoBehaviour
{
    // Start is called before the first frame update
    public string CollectableType;
    public int RotationOnCollection = 0;
    protected bool IsCollected = false;
    public UnityEvent<string, int> CollectEvent;
    public UnityEvent<string, int> ReleaseEvent;
    public Transform HandlePoint;
    private int collectedBy = -1;
    

    /// <summary>
    /// The direction the collectable is pointing in.
    /// Used to point the collectable in the right direction when collected.
    /// </summary>
    public Direction direction = Direction.FORWARDS;

    /// <summary>
    /// Possible Directions
    /// </summary>
    public enum Direction
    {
        NONE = 0,
        FORWARDS = 1,
        BACKWARDS = -1
    }

    public void collect (int collectorUID)
    {
        IsCollected = true;
        collectedBy = collectorUID;
        CollectEvent.Invoke(CollectableType, collectorUID);
    }

    public void release (int collectorUID)
    {
        IsCollected = false;
        collectedBy = -1;
        ReleaseEvent.Invoke(CollectableType, collectorUID);
    }

    public static GameObject FindCollectableGameObjectByType(string typeToFind)
    {
        // Get all Collectable components in the scene
        Collectable[] collectables = FindObjectsOfType<Collectable>();

        // Iterate through the list to find the one with the matching type
        foreach (Collectable collectable in collectables)
        {
            if (collectable.CollectableType == typeToFind)
            {
                // Return the GameObject of the matching Collectable
                return collectable.gameObject;
            }
        }

        // Return null if no matching Collectable is found
        return null;
    }
}
