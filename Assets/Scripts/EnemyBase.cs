using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{

    [SerializeField] private int health = 10;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float floorDistance = 4f;
    [SerializeField] private float distanceToJump = 0.5f;
    [SerializeField] private float platformLookAheadDistance = 3f;
    [SerializeField] private float jumpVelocity = 2;
    [SerializeField] private float maximumJumpDistance = 6f;
    [SerializeField] private LayerMask groundLayer;

    private int xDirection;

    private List<Target> targets = new List<Target>();

    
    private Rigidbody2D body;

    // ---- START ----
    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        targets = GetTargetList();
    }

    // ---- UPDATE ----
    private void FixedUpdate()
    {
        Target targetToFollow = GetTargetToFollow();
        PursueTargetXYNaively(targetToFollow);
    }

    // ---- OWN METHODS ----
    /// <summary>
    /// Method to call when the enemy should take a hit
    /// </summary>
    /// <param name="damage">Damage value</param>
    public void TakeHit(int damage)
    {
        Debug.Log("I took a hit!");
        health -= damage;

        if (health <= 0)
        {
            Debug.Log("I am dead, destroy me!");
            //Destroy(gameObject);
        }
    }

    /// <returns>A list of Target objects, that includes the target tag and the transform
    private List<Target> GetTargetList()
    {
        List<string> target_tags = GetTargetTags();
        List<Target> targets = new List<Target>();

        foreach (string tag in target_tags)
        {
            Transform target_transform = GameObject.FindGameObjectWithTag(tag).GetComponent<Transform>();
            Target target = new Target(tag, target_transform);
            targets.Add(target);
        }

        return targets;
    }

    /// <summary>
    /// May be overridden. Retrieves a list of tags that the enemy will target.
    /// </summary>
    /// <returns>By default ["Character"]</returns>
    public virtual List<string> GetTargetTags() {
        return new List<string> { "Character" };
    }

    
    /// <returns>By default returns the target closest to the enemy. May be overriden.</returns>
    protected virtual Target GetTargetToFollow()
    {
        
        // 1) Find the closest target
        Target closestTarget = null;
        float closestDistance = Mathf.Infinity;

        foreach (Target target in targets)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.Transform.position);
            if (distanceToTarget < closestDistance)
            {
                closestDistance = distanceToTarget;
                closestTarget = target;
            }
        }

        return closestTarget;

    }


    /// <summary>
    /// Default: Move towards the targets that it is closest to in the X direction
    /// </summary>
    protected virtual void PursueTargetX(Transform targetTransform)
    {
        //Make sure that the enemy doesn't follow vertically
        Vector3 currentPos = transform.position;
        Vector3 targetPosition = targetTransform.position;
        targetPosition.y = currentPos.y;
        

        // Move towards the target position
        transform.position = Vector3.MoveTowards(
            currentPos,                       // Current position
            targetPosition,                   // Target position
            moveSpeed * Time.deltaTime       // Speed factor
        );
    }

    /// <summary>
    /// Default: Move towards the targets that it is closest to in the X and Y direction in a naive way
    /// </summary>
    protected virtual void PursueTargetXYNaively(Target target)
    {
        // Set intermediate target! // to allow it to move when (x=xTarget && floor != floorTarget)
        // <>

        // Apply velocity in the X direction
        // NEW IDEA: change in X direction at random time intervals when not on the same level
        PursueTargetX(target.Transform);

        SetXDirectionGlobally(target.Transform);

        // Retrieve target transform
        Transform targetTransform = target.Transform;

        // Get relative floor
        RelativeFloor relativeFloor = GetRelativeTargetFloor(targetTransform);
        Debug.Log(relativeFloor);

        // Always check for gaps and jump
        if (IsGapAhead() && CanJumpGap() && relativeFloor != RelativeFloor.below)
        {
            Jump();
        }

        if (relativeFloor == RelativeFloor.above)
        {
            if (IsGapAbove() && IsPlatformAboveInReach()) // Only look for a platform above when there is a gap above
            {
                Jump();
            }
        }

    }

    private void SetXDirectionGlobally(Transform targetTransform)
    {
        xDirection = Math.Sign(targetTransform.position[0] - transform.position[0]);
    }

    private bool IsGapAbove()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, floorDistance, groundLayer);
        return !hit;
    }

    private bool IsPlatformAboveInReach()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(xDirection * platformLookAheadDistance, 0, 0), Vector2.up, floorDistance, groundLayer);
        return hit;
    }
    private bool IsGapAhead()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(distanceToJump, 0, 0), Vector2.down, 2f, groundLayer);
        return !hit;
    }

    private bool CanJumpGap()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(maximumJumpDistance, 0, 0), Vector2.down, 2f, groundLayer);
        return hit;
    }

    private void Jump()
    {
        // Do groundcheck
        RaycastHit2D ground = Physics2D.Raycast(transform.position, Vector2.down, 1f, groundLayer);

        // Jump if on ground
        if (ground) {
            body.velocity = body.velocity + new Vector2(0, jumpVelocity);
        }
    }

    private RelativeFloor GetRelativeTargetFloor(Transform targetTransform)
    {
        // Get distances
        Vector3 xyzDistance = targetTransform.position - transform.position;
        float yDistance = xyzDistance[1];
        
        // Instantiate relative floor type
        RelativeFloor relativeTargetFloor;

        // Set floor: above || below || same
        if (yDistance > floorDistance) relativeTargetFloor = RelativeFloor.above;
        else if (yDistance < -floorDistance) relativeTargetFloor = RelativeFloor.below;
        else relativeTargetFloor = RelativeFloor.same;
        
        return relativeTargetFloor;
    }

    private enum RelativeFloor
    {
        above = 1,
        same = 2,
        below = 3,
    }


}

public class Target
{
    public string Tag = null;
    public Transform Transform = null;

    public Target (string tag, Transform transform)
    {
        Tag = tag;
        Transform = transform;
    }
}

