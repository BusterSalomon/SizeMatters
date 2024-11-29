using UnityEngine;

public class Bird : MonoBehaviour
{
    public float moveSpeed = 3f;         // Geschwindigkeit der Vögel
    public Transform treeTarget;        // Baumziel
    public float damageToTree = 1f;     // Schaden, den der Vogel dem Baum zufügt
    private Vector2 targetPosition;     // Zielposition (Baum)

    void Start()
    {
        if (treeTarget != null)
        {
            targetPosition = treeTarget.position;
        }
        else
        {
            Debug.LogError("Kein Ziel (Baum) zugewiesen.");
        }
    }

    void Update()
    {
        if (targetPosition != null)
        {
            MoveTowardsTarget();
        }
    }

    void MoveTowardsTarget()
    {
        // Vögel fliegen in Richtung Baum
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tree"))
        {
            // Hole das Health-Skript vom Baum
            Health treeHealth = collision.gameObject.GetComponent<Health>();
            if (treeHealth != null)
            {
                treeHealth.TakeDamage(damageToTree); // Schaden zufügen
            }
            Destroy(gameObject); // Vogel wird zerstört
        }
        else if (collision.gameObject.CompareTag("MovingBar"))
        {
            Destroy(gameObject); // Vogel wird durch Plattform zerstört
        }
    }
}
