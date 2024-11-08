using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafBlowerZone : MonoBehaviour
{
    private LeafblowerCollectable parent;
    
    void Start()
    {
        parent = GetComponentInParent<LeafblowerCollectable>();
    }
    
    //private void OnTriggerStay(Collider other)
    //{
    //    parent.HandleObjectInBlowZone(other);
    //    Debug.Log($"Object detected! {other.tag}");
        
    //}
    private void OnTriggerStay2D(Collider2D collision)
    {
        parent.HandleObjectInBlowZone(collision);
        Debug.Log($"Object detected! {collision.tag}");
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log($"Object detected! {other.tag}");
    //}
}
