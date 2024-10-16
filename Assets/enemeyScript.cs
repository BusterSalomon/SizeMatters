using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class enemeyScript : MonoBehaviour
{
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
    void Start()
    {
        Hitpoints = MaxHitpoints;
        Healthbar.SetHealth(Hitpoints, MaxHitpoints);
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
  
}

