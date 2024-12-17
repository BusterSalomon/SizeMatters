using System.Collections;
using UnityEngine;
public class EnemyVirus : Enemy
{
    public Transform head;
    public float walkSpeed =  3f;
    private Rigidbody2D rb_V; 

    public float spawnDistance = 1.3f;
    private BoxCollider2D boxCollider;

    [SerializeField] private bool isLevelBoss = false;
    
    [SerializeField] private int numOfMultiply = 5;



    private Vector2 originalSize;  // Store the original size of the enemy
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer

    private Color originalColor; // Store the original color
    public countDown counterScript;
    private Transform countdownTransform;  // Reference to the countdown text object

     public GameObject enemyPrefab; // Enemy prefab to spawn
    [SerializeField] private float spawnIntervalMin ; // Minimum spawn interval
    [SerializeField] private float spawnIntervalMax ; // Maximum spawn interval

    protected override void Start()
    {
        base.Start();
        CurrentHealth = MaxHealth;
        Healthbar.SetHealth(CurrentHealth, MaxHealth);
        StartCoroutine(SpawnEnemyRoutine()); // Start spawning enemies
    }

    private void Awake()
    {
        rb_V = GetComponent<Rigidbody2D>();
        originalSize = transform.localScale;
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component
        originalColor = spriteRenderer.color; // Store the original color
        countdownTransform = counterScript.transform;  // Store the transform of the countdown text
    }
    protected override void FixedUpdate()
    {
        rb_V.velocity = new Vector2(walkSpeed *  Vector2.right.x, rb_V.velocity.y);
    }
   
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Enemy") )
        {
            Flip();
        }
        if(collision.gameObject.CompareTag("Character")){
            AudioManager.instance.Play("CellDie");
            collision.collider.GetComponent<Health>().TakeDamage(CollisionDamage);
            TakeHit((MaxHealth/4));
            Debug.Log("Bum hitted the player with: "+ CollisionDamage);
        }
    } 

    protected override void Flip()
    {
        // Reverse the walk speed to change direction
        walkSpeed *= -1;
        // Flip the enemy sprite by inverting the local scale on the X axis

        counterScript.transform.localScale = new Vector2(Mathf.Abs(counterScript.transform.localScale.x), counterScript.transform.localScale.y);   
    }
    private IEnumerator SpawnEnemyRoutine()
    {
        int reproduced = 0; 
        while ( (numOfMultiply - reproduced) > 0) // Keeps spawning indefinitely
        {
            // Random delay between spawns
            float spawnDelay = Random.Range(spawnIntervalMin, spawnIntervalMax);
            yield return new WaitForSeconds(spawnDelay);

            // Spawn the new enemy
            SpawnEnemy();
        }
    }

    public void mutateVirus(float _damage, float _health){
        CollisionDamage += _damage;
        MaxHealth += _health;
        CurrentHealth += _health;
    }

    private void SpawnEnemy()
    {
        AudioManager.instance.Play("CellSpawn");

        // Instantiate a new enemy at a position relative to the original enemy
        if(isLevelBoss){spawnDistance *= -1;}
        Vector2 spawnPosition = new Vector2(transform.position.x + spawnDistance, transform.position.y);

        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        EnemyVirus newEnemyScript = newEnemy.GetComponent<EnemyVirus>();
        newEnemyScript.spriteRenderer.color = originalColor;
        newEnemyScript.isLevelBoss = false;
        newEnemyScript.transform.localScale = new Vector3((float)0.35,(float)0.35,1);
        
        //newEnemyScript.counterScript = counterScript; 
    }


}
