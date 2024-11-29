using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4Enemy : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float startingHealth;
    private float currentHealth;
    public float walkSpeed = 3f;
    private Rigidbody2D rb;

    public float spawnDistance = 1.3f;
    private BoxCollider2D boxCollider;

    private Vector2 originalSize;  // Store the original size of the enemy
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer

    private Color originalColor; // Store the original color
    public float Hitpoints;
    public float MaxHitpoints = 5;
    public EnemyHealthbar Healthbar;

    public GameObject enemyPrefab; // Enemy prefab to spawn
    private float respawnDelayMin = 5f; // Minimum respawn delay
    private float respawnDelayMax = 10f; // Maximum respawn delay

    private Vector2 spawnPosition; // Position to respawn the enemy

    private PoliceHealthbar policeHealthbar;

    void Start()
    {
        Hitpoints = MaxHitpoints;
        Healthbar.SetHealth(Hitpoints, MaxHitpoints);
        spawnPosition = transform.position; // Save the initial spawn position
    }

    public void TakeHit(float damage)
    {
        Hitpoints -= damage;
        Healthbar.SetHealth(Hitpoints, MaxHitpoints);

        if (Hitpoints <= 0)
        {
            DieAndRespawn();
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        originalSize = transform.localScale;
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component
        originalColor = spriteRenderer.color; // Store the original color
        policeHealthbar = GetComponentInChildren<PoliceHealthbar>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(walkSpeed * Vector2.right.x, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Ground"))
        {
            Flip();
        }
        if (collision.gameObject.CompareTag("Character"))
        {
            collision.collider.GetComponent<Health>().TakeDamage(damage);
            TakeHit((MaxHitpoints / 4));
            Debug.Log("Bum");
        }
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            // Player hurt logic
        }
        else
        {
            DieAndRespawn();
        }
    }

    public void Flip()
    {
        
         // Reverse the walk speed to change direction
        walkSpeed *= -1;
        // Flip the enemy sprite by inverting the local scale on the X axis
        Vector2 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler; 
        policeHealthbar.healthXposition *= -1;

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
        Hitpoints = MaxHitpoints;
        Healthbar.SetHealth(Hitpoints, MaxHitpoints);

        // Reset position
        transform.position = spawnPosition;

        // Reactivate the enemy
        gameObject.SetActive(true);
    }
}