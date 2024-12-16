using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4Enemy : Enemy
{
    public float walkSpeed = 3f; // Movement logic, part of Walkable

    public float spawnDistance = 1.3f;
    private PoliceHealthbar policeHealthbar;

    public GameObject enemyPrefab; // Enemy prefab to spawn
    private float respawnDelayMin = 5f; // Minimum respawn delay
    private float respawnDelayMax = 10f; // Maximum respawn delay

    private Vector2 spawnPosition; // Position to respawn the enemy

    protected override void Start()
    {
        base.Start(); // Call Enemy's Start method for basic initialization

        // Save the initial spawn position
        spawnPosition = transform.position;

        // Set health bar values if it exists
        if (Healthbar)
        {
            Healthbar.SetHealth(CurrentHealth, MaxHealth);
        }

        // Additional initialization specific to Level4Enemy
        rb = GetComponent<Rigidbody2D>();
        policeHealthbar = GetComponentInChildren<PoliceHealthbar>();
    }

    public override void TakeHit(float damage)
    {
        base.TakeHit(damage);

        if (Healthbar)
        {
            Healthbar.SetHealth(CurrentHealth, MaxHealth);
        }

        // Trigger respawn logic if health is depleted
        if (CurrentHealth <= 0)
        {
            DieAndRespawn();
        }
    }

    public new void Flip()
    {
        // Reverse the walk speed to change direction
        walkSpeed *= -1;

        // Flip the enemy sprite by inverting the local scale on the X axis
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;

        // Flip the health bar position
        if (policeHealthbar != null)
        {
            policeHealthbar.healthXposition *= -1;
        }
    }

    private void DieAndRespawn()
    {
        // Hide the enemy and disable its functionality
        gameObject.SetActive(false);

        // Start the respawn process
        float respawnDelay = Random.Range(respawnDelayMin, respawnDelayMax);
        Invoke(nameof(Respawn), respawnDelay);
    }

    private void Respawn()
    {
        // Reset enemy health
        CurrentHealth = MaxHealth;
        if (Healthbar)
        {
            Healthbar.SetHealth(CurrentHealth, MaxHealth);
        }

        // Reset position
        transform.position = spawnPosition;

        // Reactivate the enemy
        gameObject.SetActive(true);
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        // Handle collision with the player (Character tag)
        if (collision.gameObject.CompareTag("Character"))
        {
            // Get the Health component from the player
            Health characterHealth = collision.collider.GetComponent<Health>();
            if (characterHealth != null)
            {
                // Play damage sound effect
                AudioManager.instance.Play("takephysicaldamage");
                
                // Reduce the character's health
                characterHealth.TakeDamage(CollisionDamage);

                Debug.Log("Player collided with the enemy. Damage applied.");
            }
            else
            {
                Debug.LogWarning("No Health component found on the player.");
            }
        }
        // Handle collision with walls or ground to flip direction
        else if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Ground"))
        {
            Flip();
        }
    }

    protected new void FixedUpdate()
    {
        // Implement walk speed logic
        rb.velocity = new Vector2(walkSpeed * Vector2.right.x, rb.velocity.y);
    }
}
