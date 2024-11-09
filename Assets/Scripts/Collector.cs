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

    public Collectable CollectableCollected;

    private List<GameObject> collectableGameObjects = new List<GameObject>();
    
    
    void Start()
    {
        SetCollectables();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject collectable = GetCollectableIfHovering();
        if (collectable != null && CollectableCollected == null && CollectCondition())
        {
            // Disable physics
            Rigidbody2D rb = collectable.GetComponent<Rigidbody2D>();
            if (rb != null) { 
                rb.isKinematic = true;
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0f;
            }

            CollectableCollected = collectable.GetComponent<Collectable>();
            collectable.transform.SetParent(transform);
            collectable.transform.position = gripPoint.position;
            collectable.GetComponent<Collectable>().collect();

            Collider2D collider = collectable.GetComponent<Collider2D>();
            if (collider != null) collider.enabled = false;
        }

        if (CollectableCollected != null && ReleaseCondition())
        {
            // Enable physics
            Rigidbody2D rb = collectable.GetComponent<Rigidbody2D>();
            if (rb != null) rb.isKinematic = false;

            CollectableCollected = null;
            collectable.transform.SetParent(null);
            collectable.GetComponent<Collectable>().release();

            Collider2D collider = collectable.GetComponent<Collider2D>();
            if (collider != null) collider.enabled = true;
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
            if (CollectableTags.Count == 0 || CollectableTags.Contains(collectable.CollectableType))
            {
                collectableGameObjects.Add(collectable.gameObject);
            }
        }
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

    // ---- DEFAULT COLLECTION LOGIC ----
    private float collectToReleaseDelay = 2f;
    private float btnPressedTime = -1f;

    /// <summary>
    /// Condition to enable collect. By default, evalutes to true when C is pressed. May be overridden by subclasses.
    /// </summary>
    public virtual bool CollectCondition ()
    {
        btnPressedTime = Time.time;
        return Input.GetKey(KeyCode.C);
    }

    /// <summary>
    /// Condition to enable realse. By default, evalutes to true when R and collectToReleaseDelay is passed since the collectable was collected.
    /// May be overridden by subclasses.
    /// </summary>
    public virtual bool ReleaseCondition()
    {
        bool canRelease = Time.time - btnPressedTime > collectToReleaseDelay;
        return Input.GetKey(KeyCode.R) && canRelease;
    }


}
