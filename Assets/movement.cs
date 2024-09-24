using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class movement : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody2D Rb;
    LayerMask mask;

    [Header("Movement Config")]
    public float ray_dist = 2f;
    public float jump_force = 1f;
    public float walk_force = 1f;
    
    public bool character_grounded = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        mask = LayerMask.GetMask("Ground");

    }

    private void FixedUpdate()
    {
        // Cast a ray straight down.
        RaycastHit2D ground_hit = Physics2D.Raycast(transform.position, -Vector2.up, ray_dist, mask);
        if (ground_hit.collider != null)
        {
            Debug.Log("The player is grounded");
            character_grounded = true;
        }
        else
        {
            character_grounded = false;
        }

        // Detect key presses
        bool up = Input.GetKey(KeyCode.W); // W pressed or Jump button
        bool left = Input.GetKey(KeyCode.A); // A pressed
        bool right = Input.GetKey(KeyCode.D); // D pressed

        if (up)
        {
            Debug.Log("Jump!");
            Rb.AddForce(transform.up * jump_force, ForceMode2D.Impulse);
            // Add jumping logic here
        }

        if (left)
        {
            Debug.Log("Move left");
            Rb.AddForce(Vector2.left * walk_force, ForceMode2D.Impulse);
            // Add left movement logic here
        }

        if (right)
        {
            Debug.Log("Move right");
            Rb.AddForce(Vector2.right * walk_force, ForceMode2D.Impulse);
            // Add right movement logic here
        }

    }
}
