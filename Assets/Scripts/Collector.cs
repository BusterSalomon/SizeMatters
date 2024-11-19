using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

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
    public List<string> CollectableTypes;

    private Collectable CollectableCollected;

    public UnityEvent RealeaseEvent;
    public UnityEvent CollectEvent;

    private List<GameObject> collectableGameObjects = new List<GameObject>();
    
    
    void Start()
    {
        SetCollectables();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject collectable = GetCollectableIfHovering();
        
        // Collector DID collect an item
        if (collectable != null && CollectableCollected == null && CollectCondition())
        {
            Collectable script = collectable.GetComponent<Collectable>();
            CollectableCollected = collectable.GetComponent<Collectable>();

            // Disable physics
            Rigidbody2D rb = collectable.GetComponent<Rigidbody2D>();
            if (rb != null) { 
                rb.isKinematic = true;
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0f;
            }

            // Disable collider
            Collider2D collider = collectable.GetComponent<Collider2D>();
            if (collider != null) collider.enabled = false;

            // Set parent, position and scale
            collectable.transform.SetParent(gripPoint);
            Vector3 collectableLocalScale = collectable.transform.localScale;
            collectable.transform.localScale = new Vector3((int)script.direction * Mathf.Abs(collectableLocalScale.x), collectableLocalScale.y, collectableLocalScale.z); // Make sure it points in the same position as the collector
            Vector3 GripHandleOffset = gripPoint.position - script.HandlePoint.position;
            collectable.transform.position += GripHandleOffset;

            // Notify collectable
            collectable.GetComponent<Collectable>().collect();

            // Notify subscribers
            CollectEvent.Invoke();
        }

        // Collector did NOT collect an item
        if (CollectableCollected != null && ReleaseCondition())
        {

            GameObject collectableCollectedGO = CollectableCollected.gameObject;
            Debug.Log($"Should release now! {collectableCollectedGO.name}");


            // Free
            collectableCollectedGO.transform.SetParent(null);

            // Enable physics
            Rigidbody2D rb = collectableCollectedGO.GetComponent<Rigidbody2D>();
            if (rb != null) rb.isKinematic = false;
            
            collectableCollectedGO.GetComponent<Collectable>().release();

            // Enable collider
            Collider2D collider = collectableCollectedGO.GetComponent<Collider2D>();
            if (collider != null) collider.enabled = true;

            // Call realease action and initiase collectable afterwards
            RealeaseEvent.Invoke();
            CollectableCollected = null;
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
            // add all if CollectableTypes are empty, otherwise only tagged
            if (CollectableTypes.Count == 0 || CollectableTypes.Contains(collectable.CollectableType))
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
    
    /// <returns>Collectable if any, otherwise null</returns>
    public Collectable GetCollectableIfCollected()
    {
        return CollectableCollected;
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
