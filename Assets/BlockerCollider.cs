using UnityEngine;

public class BlockerCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Character"))
        {
            // Stoppe den Player oder verhindere seine Bewegung
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero; // Setzt Geschwindigkeit auf 0
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Character"))
        {
            // Optional: Zusätzliche Logik, um dauerhaftes Blockieren zu steuern
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Character"))
        {
            // Optional: Logik, wenn der Player den Blocker verlässt
        }
    }
}
