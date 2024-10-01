using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemeyScript : MonoBehaviour
{
    public float walkSpeed =  3f;
    private Rigidbody2D rb; 
   
    public GameObject ChildEnemies;  // Prefab of the child enemy
    public float spawnInterval = 5f;  // Time interval for spawning
    public float growthFactor = 2f;  // Factor to increase the parent size every interval
    public float spawnDistance = 1.3f;
    private BoxCollider2D boxCollider;

    private Vector2 originalSize;  // Store the original size of the enemy

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        originalSize = transform.localScale;
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        // Start the coroutine to spawn enemies and grow the parent
        StartCoroutine(SpawnAndGrowRoutine());
    }

    // Coroutine to handle enemy spawning and parent growth
    private IEnumerator SpawnAndGrowRoutine()
    {
        while (true)  // Infinite loop
        {
            // Wait for the defined interval (5 seconds)
            yield return new WaitForSeconds(spawnInterval);

            // Spawn two child enemies
            SpawnChildEnemies();

            // Grow the parent enemy
            GrowParentEnemy();
        }
    }

    // Method to spawn two child enemies
    private void SpawnChildEnemies()
    {
        // Position the first child enemy on the left side of the parent
        Vector2 leftSpawnPosition = new Vector2(transform.position.x - spawnDistance, transform.position.y);
        GameObject leftChild = Instantiate(ChildEnemies, leftSpawnPosition, Quaternion.identity);
        leftChild.transform.localScale = originalSize;  // Ensure child has the original size

        // Position the second child enemy on the right side of the parent
        Vector2 rightSpawnPosition = new Vector2(transform.position.x + spawnDistance, transform.position.y);
        GameObject rightChild = Instantiate(ChildEnemies, rightSpawnPosition, Quaternion.identity);
        rightChild.transform.localScale = originalSize;  // Ensure child has the original size

        // Optionally, add additional behavior to the children if needed
    }

    // Method to grow the parent enemy
    private void GrowParentEnemy()
    {
        // Increase the parent's size by multiplying its scale by the growth factor (2D Vector)
        transform.localScale *= growthFactor; 
        if (transform.localScale.x > boxCollider.size.x || transform.localScale.y > boxCollider.size.y)
        {
            growthFactor = 1; // Adjust offset if needed to keep it centered
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(walkSpeed *  Vector2.right.x, rb.velocity.y);
        // Check for a wall in front of the enemy
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the enemy collides with a wall (you can use a tag or a specific layer)
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Ground") )
        {
            Flip();
        }
    }

    // Flip the enemy's movement direction
    private void Flip()
    {
        // Reverse the walk speed to change direction
        walkSpeed *= -1;

        // Flip the enemy sprite by inverting the local scale on the X axis
        Vector2 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

}
