using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private float horizontalSpeed = 10;
    private float verticalSpeed = 12;
    private Rigidbody2D body;
    private bool grounded = false;

    private float horizontalInput;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
{
    // Setze die Skalierung kontinuierlich zurück
    transform.localScale = Vector3.one;

    horizontalInput = Input.GetAxis("Horizontal");

    // Flip player when moving left-right
    if (horizontalInput > 0.01f)
        transform.localScale = Vector3.one;
    else if (horizontalInput < -0.01f)
        transform.localScale = new Vector3(-1, 1, 1);

    // Get jump input
    bool jumpKeyPressed = Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W);

    // Set character speed on input
    body.velocity = new Vector2(horizontalInput * horizontalSpeed, body.velocity.y);

    // Jump logic
    if (jumpKeyPressed && grounded)
    {
        body.velocity = new Vector2(body.velocity.x, verticalSpeed);
        grounded = false;
    }
}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }

    public bool canAttack() // Methode zur Überprüfung, ob der Spieler angreifen kann
    {
        // Hier kannst du die Logik für das Angreifen definieren
        return true; // Beispiel: immer angreifen können
    }
}
