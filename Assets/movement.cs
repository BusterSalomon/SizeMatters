using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private float horizontalSpeed = 10;
    private float verticalSpeed = 12;
    private Rigidbody2D body;
    private bool grounded = false;
    private Transform currentPlatform; // Speichere die aktuelle Plattform, auf der der Charakter steht
    private Vector3 platformLastPosition; // Speichere die letzte Position der Plattform

    private float horizontalInput;
    private Vector3 originalScale;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale; // Speichere die Originalskalierung des Charakters
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        // Flip player when moving left-right, aber behalte die Originalskalierung
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);

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

        // Wenn der Charakter auf einer Plattform steht und der Spieler keine Taste drückt
        if (currentPlatform != null && horizontalInput == 0)
        {
            Vector3 platformMovement = currentPlatform.position - platformLastPosition;
            transform.position += platformMovement; // Bewege den Charakter relativ zur Plattform
        }

        // Aktualisiere die letzte Position der Plattform
        if (currentPlatform != null)
        {
            platformLastPosition = currentPlatform.position;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }

        // Wenn der Charakter auf einer Plattform landet
        if (collision.gameObject.CompareTag("Platform"))
        {
            currentPlatform = collision.transform; // Speichere die Plattform
            platformLastPosition = currentPlatform.position; // Speichere die letzte Position der Plattform
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Wenn der Charakter die Plattform verlässt
        if (collision.gameObject.CompareTag("Platform"))
        {
            currentPlatform = null; // Entferne die Plattformreferenz
        }
    }

    public bool canAttack()
    {
        return true; // Beispiel: immer angreifen können
    }
}
