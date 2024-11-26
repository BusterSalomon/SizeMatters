using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursueEnemy : EnemyV2
{
    // ---- Public properties
    public float MoveSpeed = 5f;
    public List<string> TargetTags = new List<string> { "Character" };

    // ---- Private properties
    private List<Target> targets = new List<Target>();

    // ---- Methods
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        targets = GetTargetList();
        Debug.Log("Extended start called");
    }

    private void FixedUpdate()
    {
        if (enemyEnabled)
        {
            Target targetToFollow = GetTargetToFollow();
            PursueTargetX(targetToFollow.Transform);
        }
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
            MoveSpeed * Time.deltaTime       // Speed factor
        );
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
