using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class enemeyScript : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float startingHealth;
    private float currentHealth;
    public float walkSpeed =  3f;
    private Rigidbody2D rb; 

    public float spawnDistance = 1.3f;
    private BoxCollider2D boxCollider;

    private Vector2 originalSize;  // Store the original size of the enemy
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer

    private Color originalColor; // Store the original color
    public countDown counterScript;
    private Transform countdownTransform;  // Reference to the countdown text object

    public float Hitpoints;
    public float MaxHitpoints = 5;
    public healthbarBehavior Healthbar;

     public GameObject enemyPrefab; // Enemy prefab to spawn
    private float spawnIntervalMin = 5f; // Minimum spawn interval
    private float spawnIntervalMax = 10f; // Maximum spawn interval
    void Start()
    {
        Hitpoints = MaxHitpoints;
        Healthbar.SetHealth(Hitpoints, MaxHitpoints);
        StartCoroutine(SpawnEnemyRoutine()); // Start spawning enemies
    }

    public void TakeHit(float damage)
    {
        Hitpoints -= damage;
        Healthbar.SetHealth(Hitpoints, MaxHitpoints);

        if (Hitpoints <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        originalSize = transform.localScale;
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component
        originalColor = spriteRenderer.color; // Store the original color
        countdownTransform = counterScript.transform;  // Store the transform of the countdown text
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(walkSpeed *  Vector2.right.x, rb.velocity.y);
    }
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Ground") )
        {
            Flip();
        }
        if(collision.gameObject.CompareTag("Character")){
            collision.collider.GetComponent<Health>().TakeDamage(damage);
            Debug.Log("Bum");
        }
    }

        public void TakeDamage(float _damage){

        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        currentHealth -= _damage;
        
        if(currentHealth > 0){
            //player hurt
        }else{
            //Dead
        }
    
    }  

    private void Flip()
    {
        // Reverse the walk speed to change direction
        walkSpeed *= -1;
        // Flip the enemy sprite by inverting the local scale on the X axis
        //Vector2 scaler = transform.localScale;
        //scaler.x *= -1;
        //transform.localScale = scaler; 
        counterScript.transform.localScale = new Vector2(Mathf.Abs(counterScript.transform.localScale.x), counterScript.transform.localScale.y);   
    }
    private IEnumerator SpawnEnemyRoutine()
    {
        while (true) // Keeps spawning indefinitely
        {
            // Random delay between spawns
            float spawnDelay = Random.Range(spawnIntervalMin, spawnIntervalMax);
            yield return new WaitForSeconds(spawnDelay);

            // Spawn the new enemy
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        // Instantiate a new enemy at a position relative to the original enemy
        Vector2 spawnPosition = new Vector2(transform.position.x + spawnDistance, transform.position.y);

        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        enemeyScript newEnemyScript = newEnemy.GetComponent<enemeyScript>();
        newEnemyScript.spriteRenderer.color = originalColor;
        newEnemyScript.counterScript = counterScript; 
    }
  
}

