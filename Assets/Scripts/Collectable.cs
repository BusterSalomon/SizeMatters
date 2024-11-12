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
