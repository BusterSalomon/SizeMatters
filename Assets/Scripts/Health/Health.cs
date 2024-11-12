using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth = 10f; // Setze den Startwert für die Gesundheit
    public float currentHealth { get; private set; }

    // Referenz zum GameOverScreen
    public GameOverScreen gameOverScreen;

    private void Awake() 
    {
        currentHealth = startingHealth; // Initialisiere die Gesundheit
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth); // Gesundheit begrenzen (maximal 0)
        
        // Optional: Ausgabe in der Konsole, wenn Schaden genommen wird
        Debug.Log("Damage taken! Current health: " + currentHealth);
    }

    private void Update() 
    {
        // Zum Testen: Schaden zufügen, wenn 'E' gedrückt wird
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            TakeDamage(1); // Schaden von 1 zufügen
        }
    }
}
