using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Collectable;

public class EnemyPursue : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /// <returns>A list of Target objects, that includes the target tag and the transform
    private List<Target> GetTargetList()
    {
        List<Target> targets = new List<Target>();

        foreach (string tag in TargetTags)
        {
            Transform target_transform = GameObject.FindGameObjectWithTag(tag).GetComponent<Transform>();
            Target target = new Target(tag, target_transform);
            targets.Add(target);
        }

        return targets;
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

    private void MoveX(Direction direction)
    {

        if (direction == Direction.right) body.velocity = new Vector2(Math.Abs(moveSpeed), 0);
        else body.velocity = new Vector2(-Math.Abs(moveSpeed), 0);
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
        if (ground)
        {
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

    private void Flip()
    {
        Debug.Log("did flip!");
        if (direction == Direction.right) direction = Direction.left;
        else direction = Direction.right;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        Debug.Log(direction);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Flip if colliding with a blocker
        bool isBlocker = collision.gameObject.CompareTag("Blocker");
        if (isBlocker) Flip();

        // Take damage if target
        bool isTarget = TargetTags.Contains(collision.gameObject.tag);
        if (isTarget)
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(1f);
        }
    }

    private enum RelativeFloor
    {
        above = 1,
        same = 2,
        below = 3,
    }

    private enum Direction
    {
        right,
        left
    }

    private enum State
    {
        PURSUE_X,
        PURSUE_UP,
        PURSUE_DOWN
    }

    public enum EnemyVersion
    {
        Naive,
        Smart
    }

    private static Dictionary<Direction, int> directionScalarMap = new Dictionary<Direction, int>
        {
            { Direction.left, -1 },
            { Direction.right, 1 }
        };

}

public class Target
{
    public string Tag = null;
    public Transform Transform = null;

    public Target(string tag, Transform transform)
    {
        Tag = tag;
        Transform = transform;
    }
}
