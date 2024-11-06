using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GripCollectable : MonoBehaviour
{
    private List<GameObject> collectableGameObjects = new List<GameObject>();
    public float pickupRange = 2.0f; // The distance threshold for picking up
    public Transform gripPoint;
    void Start()
    {
        SetCollectablesTransforms();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject collectableGripped = GetCollectableIfHovering();
        if (collectableGripped != null && Input.GetKey(KeyCode.E))
        {
            collectableGripped.transform.position = gripPoint.position;
            collectableGripped.transform.SetParent(transform);
        }
    }

    /// <summary>
    /// Sets the collectableTransforms to the transforms having the Collectable tag
    /// </summary>
    private void SetCollectablesTransforms ()
    {
        // Find all game objects with the "Item" tag
        //GameObject[] collectables = GameObject.FindGameObjectsWithTag("Collectable");
        Collectable[] collectables = FindObjectsOfType<Collectable>();

        // Loop through the array and add each Transform to the list
        foreach (Collectable collectable in collectables)
        {
            collectableGameObjects.Add(collectable.gameObject);
        }

        // Optional: Log the number of items found
        Debug.Log("Found " + collectableGameObjects.Count + " collectables.");
    }
    /// <summary>
    /// Returns the tag if the character is in the same position as a collectable
    /// </summary>
    private GameObject GetCollectableIfHovering()
    {
        foreach (GameObject collectable in collectableGameObjects)
        {
            float distance = Vector3.Distance(transform.position, collectable.transform.position);
            if (distance < pickupRange)
            {
                return collectable;
            }
        }
        return null;
    }
}
