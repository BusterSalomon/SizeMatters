using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    // Start is called before the first frame update
    public string CollectableType;
    public bool IsCollected = false;
    
    public void collect ()
    {
        IsCollected = true;
    }

    public void release ()
    {
        IsCollected = false;
    }

    private void Update()
    {
        
    }
}
