using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursueEnemy : EnemyV2
{
    // ---- Public properties
    public List<string> TargetTags = new List<string> { "Character" };

    // ---- Private properties
    private List<Target> targets = new List<Target>();
    private Direction direction = Direction.Right;
    private Rigidbody2D rb;

    private enum Direction
    {
        Right=1,
        Left=-1
    }

    // ---- Methods
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        targets = GetTargetList();
        rb = GetComponent<Rigidbody2D>();
        Debug.Log("Extended start called");
    }

    private void FixedUpdate()
    {
        if (enemyEnabled)
        {
            Target targetToPursue = GetTargetToFollow();
            //CorrectDirection(targetToPursue.Transform);
            PursueTargetX(targetToPursue.Transform);
        }
    }
    
    
    /// <summary>
    /// Default: Move towards the targets that it is closest to in the X direction
    /// </summary>
    protected virtual void PursueTargetX(Transform targetTransform)
    {
        Vector3 currentPos = transform.position;
        Vector3 targetPos = targetTransform.position;
        Direction newDirection = (Direction)Mathf.Sign(targetPos.x - currentPos.x);
        if (newDirection != direction)
        {
            Flip();
            direction = newDirection;
        }

        //Make sure that the enemy doesn't follow vertically
        if (isGrounded) rb.velocity = new Vector2((int)newDirection * MovementSpeed, 0);
              
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

    /// <returns>A list of Target objects, that includes the target tag and the transform
    protected List<Target> GetTargetList()
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
