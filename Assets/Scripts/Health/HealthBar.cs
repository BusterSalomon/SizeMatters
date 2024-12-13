using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health playerHealth; // Reference to the Health script of the player
    [SerializeField] private Image totalhealthBar; // The total health bar (background)
    [SerializeField] private Image currenthealthBar; // The current health bar (that fills based on health)
    
    [SerializeField] private GameOverScreen gameOverScreen; // Reference to the GameOverScreen script

    private void Start() 
    {
        // Ensure that the health bar is correctly set to the maximum value at the start
        currenthealthBar.fillAmount = playerHealth.currentHealth / 10f; // Assumption: Maximum value is 10
    }

    private void Update() 
    {
        // Update the health bar while the game is running
        currenthealthBar.fillAmount = playerHealth.currentHealth / 10f; // Based on the player's current health

        // If health reaches 0, display the Game Over screen
        if (playerHealth.currentHealth <= 0 && gameOverScreen != null)
        {
            gameOverScreen.ShowGameOverScreen(); // Display the Game Over screen
        }
    }
}
