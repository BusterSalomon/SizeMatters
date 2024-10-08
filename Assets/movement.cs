gusing System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Animations;

public class movement : MonoBehaviour
{
    private float horizontalSpeed = 10;
    private float verticalSpeed = 12;
    private Rigidbody2D body;
    private bool grounded = false;
    
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Get inputs
        float horizontalMovement = Input.GetAxis("Horizontal");
        bool jumpKeyPressed = Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W);
        // Set character speed on input
        body.velocity = new Vector2(horizontalMovement * horizontalSpeed, body.velocity.y);
        if (jumpKeyPressed && grounded) {
            body.velocity = new Vector2(body.velocity.x, verticalSpeed);
            grounded = false;
        } 

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }
}



//public class movement : MonoBehaviour
//{
//    [Header("Components")]
//    public Rigidbody2D Rb;
//    public BoxCollider2D characterBC;
//    public LayerMask groundLayer;

//    [Header("Movement Config")]
//    public float jump_force = 1f;
//    public float walk_force = 1f;

//    [Header("Ground Collision")]
//    public bool onGround = false;
//    public float ray_dist = 2f;

//    // Start is called before the first frame update
//    void Start()
//    {
//        Rb = GetComponent<Rigidbody2D>();
//        characterBC = GetComponent<BoxCollider2D>();
//        ray_dist = characterBC.size.y / 2 + 0.1f;
//    }

//    private void FixedUpdate()
//    {
//        // Cast a ray straight down.
//        onGround = Physics2D.Raycast(transform.position, Vector2.down, ray_dist, groundLayer);

//        // Detect key presses
//        bool up = Input.GetKey(KeyCode.W); // W pressed or Jump button
//        bool left = Input.GetKey(KeyCode.A); // A pressed
//        bool right = Input.GetKey(KeyCode.D); // D pressed

//        if (up && onGround)
//        {
//            Debug.Log("Jump!");
//            Rb.AddForce(transform.up * jump_force, ForceMode2D.Impulse);
//            // Add jumping logic here
//        }

//        if (left)
//        {
//            Debug.Log("Move left");
//            Rb.AddForce(Vector2.left * walk_force, ForceMode2D.Impulse);
//            // Add left movement logic here
//        }

//        if (right)
//        {
//            Debug.Log("Move right");
//            Rb.AddForce(Vector2.right * walk_force, ForceMode2D.Impulse);
//            // Add right movement logic here
//        }

//    }
//}
