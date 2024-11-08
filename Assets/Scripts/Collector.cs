using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Collector : MonoBehaviour
{

    /// <summary>
    /// The range for which the collector is able to collect items
    /// </summary>
    public float pickupRange = 5.0f; 

    /// <summary>
    /// The position to hold the collectable
    /// </summary>
    public Transform gripPoint;
    
    /// <summary>
    /// Collectables that the Collector is able to collect. If empty, is will collect any collectable.
    /// </summary>
    public List<string> CollectableTags; 

    private List<GameObject> collectableGameObjects = new List<GameObject>();
    private bool collectableGripped = false;
    private float collectToReleaseDelay = 2f;
    
    void Start()
    {
        SetCollectables();
    }

    // Update is called once per frame
    void Update()
    {
        float btnPressedTime = -1f;
        GameObject collectable = GetCollectableIfHovering();
        if (collectable != null && Input.GetKey(KeyCode.E) && !collectableGripped)
        {
            collectableGripped = true;
            collectable.transform.position = gripPoint.position;
            collectable.transform.SetParent(transform);
            btnPressedTime = Time.time;
            collectable.GetComponent<Collectable>().collect();
        }
        bool canRelease = Time.time - btnPressedTime > collectToReleaseDelay;
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

        // Loop through the array and add collectable game objects to the list
        foreach (Collectable collectable in collectables)
        {
            // add all if CollectableTags are empty, otherwise only tagged
            if (CollectableTags.Count == 0 || CollectableTags.Contains(collectable.tag))
            {
                collectableGameObjects.Add(collectable.gameObject);
            }
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
