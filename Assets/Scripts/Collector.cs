using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Collector : MonoBehaviour
{
    // Static field to keep track of the next UID
    private static int nextUID = 1;


    // Dictionary to store all Collectors by UID
    private static Dictionary<int, Collector> collectorsByUID = new Dictionary<int, Collector>();

    // Instance field to hold the unique ID for each Collector
    public int UID { get; private set; }

    /// <summary>
    /// The range for which the collector is able to collect items
    /// </summary>
    public float pickupRange = 5.0f; 

    /// <summary>
    /// The position to hold the collectable
    /// </summary>
    public Transform GripPoint;
    
    /// <summary>
    /// Collectables that the Collector is able to collect. If empty, is will collect any collectable.
    /// </summary>
    public List<string> CollectableTypes;

    private Collectable CollectableCollected;

    public UnityEvent<string> RealeaseEvent;
    public UnityEvent<string> CollectEvent;

    private float collectableGroundOffSet;

    private List<GameObject> collectableGameObjects = new List<GameObject>();

    public bool PointCollectableInSameDirectionAsCollector = true;


    void Awake()
    {
        // Assign a unique ID to this Collector instance
        UID = GetUID();

        // Register the Collector in the static dictionary
        collectorsByUID[UID] = this;

        Debug.Log($"Collector {name} initialized with UID: {UID}");
    }

    void Start()
    {
        SetCollectables();
    }

    // Static method to get the next UID
    private static int GetUID()
    {
        return nextUID++;
    }

    // Static method to retrieve a Collector by UID
    public static Collector GetCollectorByUID(int uid)
    {
        collectorsByUID.TryGetValue(uid, out Collector collector);
        return collector;
    }
    void OnDestroy()
    {
        // Remove the Collector from the dictionary when it is destroyed
        if (collectorsByUID.ContainsKey(UID))
        {
            collectorsByUID.Remove(UID);
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameObject collectableGO = GetCollectableIfHovering();
        Collectable collectable = collectableGO ? collectableGO.GetComponent<Collectable>() : null;
        // Collector DID collect an item
        if (collectableGO != null && !collectable.IsCollected && CollectableCollected == null && CollectCondition(collectable))
        {
            AudioManager.instance.Play("collect");
            Collect(collectableGO);
        }

        // Collector did NOT collect an item
        if (CollectableCollected != null && ReleaseCondition(CollectableCollected))
        {
            AudioManager.instance.Play("release");
            Release();
        }

    }

    public void Collect(GameObject collectable)
    {
        CollectableCollected = collectable.GetComponent<Collectable>();
        Debug.Log($"{name} did collect {CollectableCollected.CollectableType}");
        //collectableGroundOffSet = GetCollectableGroundOffset(collectable);

        // Disable physics
        Rigidbody2D rb = collectable.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        // Disable collider
        Collider2D collider = collectable.GetComponent<Collider2D>();
        if (collider != null) collider.enabled = false;

        // Set parent, position and scale
        collectable.transform.SetParent(GripPoint);
        Vector3 collectableLocalScale = collectable.transform.localScale;
        if (PointCollectableInSameDirectionAsCollector)
        {
            collectable.transform.localScale = new Vector3(
                (int)CollectableCollected.direction * Mathf.Abs(collectableLocalScale.x),
                collectableLocalScale.y,
                collectableLocalScale.z
                );
        }


        // If HandlePoint of Collectable is set, set collectable to that, otherwise use the default
        if (CollectableCollected.HandlePoint != null)
        {
            Vector3 GripHandleOffset = GripPoint.position - CollectableCollected.HandlePoint.position;
            collectable.transform.position += GripHandleOffset;
        }
        else
        {
            collectable.transform.position = GripPoint.position;
        }


        // Notify collectable
        collectable.GetComponent<Collectable>().collect(UID);

        // Notify subscribers
        CollectEvent.Invoke(CollectableCollected.CollectableType);

        
        
    }

    public void Release()
    {
        GameObject collectableCollectedGO = CollectableCollected.gameObject;

        // Free
        collectableCollectedGO.transform.SetParent(null);

        // Enable collider
        Collider2D collider = collectableCollectedGO.GetComponent<Collider2D>();
        if (collider != null) collider.enabled = true;

        // Enable physics
        Rigidbody2D rb = collectableCollectedGO.GetComponent<Rigidbody2D>();
        if (rb != null) rb.isKinematic = false;
        // Place close to the ground is not affected by physics
        //else
        //{
        //    Vector3 collectablePosition = collectableCollectedGO.transform.position;
        //    RaycastHit2D hit = Physics2D.Raycast(new Vector2(collectablePosition.x, collectablePosition.y), Vector2.down, 10f);
        //    float yGround = hit.point.y;
        //    collectableCollectedGO.transform.position = new Vector3(collectablePosition.x, yGround+collectableGroundOffSet, collectablePosition.z);
        //}

        collectableCollectedGO.GetComponent<Collectable>().release(UID);

        // Call realease action and initiase collectable afterwards
        RealeaseEvent.Invoke(CollectableCollected.CollectableType);
        CollectableCollected = null;
        collectableGroundOffSet = -1;
    }

    /// <summary>
    /// Do not use yet - contain bugs
    /// </summary>
    /// <param name="collectableGO"></param>
    /// <returns></returns>
    private float GetCollectableGroundOffset (GameObject collectableGO)
    {
        Vector3 collectablePosition = collectableGO.transform.position;
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(collectablePosition.x, collectablePosition.y), Vector2.down, 10f);
        return hit.distance - 0.22f; // HACK, had to adjust as it was a bit off...
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
                Debug.Log($"{name} found {collectable.CollectableType}");
            }
        }

        
    }
    /// <summary>
    /// Returns the tag if the character is in the same position as a collectable
    /// </summary>
    protected GameObject GetCollectableIfHovering()
    {
        foreach (GameObject collectable in collectableGameObjects)
        {
            if (collectable)
            {
                // Get target and origin transform. Looks for grip point and handle point first.
                Transform collectableHandlePoint = collectable.GetComponent<Collectable>().HandlePoint;
                Transform targetTransformToConsider = collectableHandlePoint ? collectableHandlePoint : collectable.transform;
                Transform originTransformToConsider = GripPoint ? GripPoint : transform;


                float distance = Vector3.Distance(originTransformToConsider.position, targetTransformToConsider.position);
                if (distance < pickupRange)
                {
                    return collectable;
                }
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
    public virtual bool CollectCondition (Collectable collectable)
    {
        btnPressedTime = Time.time;
        return Input.GetKey(KeyCode.C);
    }

    /// <summary>
    /// Condition to enable realse. By default, evalutes to true when R and collectToReleaseDelay is passed since the collectable was collected.
    /// May be overridden by subclasses.
    /// </summary>
    public virtual bool ReleaseCondition(Collectable collectable)
    {
        bool canRelease = Time.time - btnPressedTime > collectToReleaseDelay;
        return Input.GetKey(KeyCode.V) && canRelease;
    }
}
