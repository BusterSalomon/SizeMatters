using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Collectable : MonoBehaviour
{
    // Start is called before the first frame update
    public string CollectableType;
    public bool IsCollected = false;
    public UnityEvent collectEvent;
    public UnityEvent releaseEvent;
    public Transform HandlePoint;

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

    public void collect ()
    {
        IsCollected = true;
        collectEvent.Invoke();
    }

    public void release ()
    {
        IsCollected = false;
        releaseEvent.Invoke();
    }

    private void Update()
    {
        
    }
}
