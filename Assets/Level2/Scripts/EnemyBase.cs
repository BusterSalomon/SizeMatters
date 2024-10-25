using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{

    [SerializeField] private float health;
    [SerializeField] private int move_speed;
    private List<Transform> target_transforms = new List<Transform>();

    // ---- START ----
    private void Start()
    {
        target_transforms = GetTargetTransforms();
    }

    // ---- UPDATE ----
    private void FixedUpdate()
    {
        Move();
    }

    // ---- OWN METHODS ----
    /// <summary>
    /// Method to call when the enemy should take a hit
    /// </summary>
    /// <param name="damage">Damage value</param>
    public void TakeHit(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Debug.Log("I am dead, destroy me!");
            //Destroy(gameObject);
        }
    }

    /// <summary>
    /// Default: Move towards the targets that it is closest to
    /// </summary>
    protected virtual void Move()
    {
        if (target_transforms == null || target_transforms.Count == 0)
        {
            return; // No targets to move towards
        }

        // 1) Find the closest target
        Transform closestTarget = null;
        float closestDistance = Mathf.Infinity;

        foreach (Transform target in target_transforms)
        {
            if (target == null) continue; // Skip any null targets

            float distanceToTarget = Vector3.Distance(transform.position, target.position);
            if (distanceToTarget < closestDistance)
            {
                closestDistance = distanceToTarget;
                closestTarget = target;
            }
        }

        // 2) Move towards the closest target with move_speed
        if (closestTarget != null)
        {
            //Make sure that the enemy doesn't follow vertically
            Vector3 currentPos = transform.position;
            Vector3 targetPosition = closestTarget.position;
            targetPosition.y = currentPos.y;

            // Move towards the target position
            transform.position = Vector3.MoveTowards(
                currentPos,                       // Current position
                targetPosition,                   // Target position
                move_speed * Time.deltaTime               // Speed factor
            );
        }
    }

    /// <returns>The transforms of the targets</returns>
    private List<Transform> GetTargetTransforms()
    {
        List<string> target_tags = GetTargetTags();
        List<Transform> target_transforms = new List<Transform>();

        foreach (string tag in target_tags)
        {
            Transform target_transform = GameObject.FindGameObjectWithTag(tag).GetComponent<Transform>();
            target_transforms.Add(target_transform);
        }

        return target_transforms;
    }

    /// <summary>
    /// Abstract, must be implemented. Retrieves a list of tags that the enemy will target.
    /// </summary>
    /// <returns>A list of strings representing the target tags.</returns>
    public abstract List<string> GetTargetTags(); 

}

