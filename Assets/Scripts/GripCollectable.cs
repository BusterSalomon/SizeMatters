using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GripCollectable : MonoBehaviour
{
    private List<GameObject> collectableGameObjects = new List<GameObject>();
    public float pickupRange = 5.0f; // The distance threshold for picking up
    public Transform gripPoint;
    private bool collectableGripped = false;
    private float delay = 2f;
    void Start()
    {
        SetCollectables();
    }

    // Update is called once per frame
    void Update()
    {
        float collectToReleaseDelay = -1f;
        GameObject collectable = GetCollectableIfHovering();
        if (collectable != null && Input.GetKey(KeyCode.E) && !collectableGripped)
        {
            collectableGripped = true;
            //collectable.transform.position = gripPoint.position - new Vector3(collectable.transform.localScale.x/2, 0, 0);
            collectable.transform.position = gripPoint.position;
            collectable.transform.SetParent(transform);
            collectToReleaseDelay = Time.time;
            collectable.GetComponent<Collectable>().collect();
        }
        bool canRelease = Time.time - collectToReleaseDelay > delay;
        Debug.Log($"can realse: {canRelease}");
        Debug.Log($"gripped: {collectableGripped}");
        Debug.Log($"key: {Input.GetKey(KeyCode.E)}");
        if (collectableGripped && Input.GetKey(KeyCode.E) && canRelease)
        {
            collectableGripped = false;
            collectable.transform.SetParent(null);
            collectable.GetComponent<Collectable>().release();
        }

    }

    /// <summary>
    /// Sets the collectableTransforms to the transforms having the Collectable tag
    /// </summary>
    private void SetCollectables ()
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
