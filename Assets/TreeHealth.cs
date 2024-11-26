using UnityEngine;
using UnityEngine.Events;

public class Tree : MonoBehaviour
{
    public int health = 10;  // Anfangsleben des Baums
    public UnityEvent TreeDestroyed;  // Ereignis, wenn der Baum zerstört wird

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            TreeDestroyed.Invoke();
            Destroy(gameObject);  // Baum wird zerstört
        }
    }
}
