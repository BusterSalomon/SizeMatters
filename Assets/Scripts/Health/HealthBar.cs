using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health playerHealth; // Referenz auf das Health-Skript des Spielers
    [SerializeField] private Image totalhealthBar; // Die gesamte Healthbar (Hintergrund)
    [SerializeField] private Image currenthealthBar; // Die aktuelle Healthbar (die sich basierend auf der Gesundheit füllt)
    
    [SerializeField] private GameOverScreen gameOverScreen; // Referenz auf das GameOverScreen-Skript

    private void Start() 
    {
        // Stelle sicher, dass die Healthbar zu Beginn korrekt auf den maximalen Wert gesetzt ist
        currenthealthBar.fillAmount = playerHealth.currentHealth / 10f; // Annahme: Maximalwert 10
    }

    private void Update() 
    {
        // Aktualisiere die Healthbar, während das Spiel läuft
        currenthealthBar.fillAmount = playerHealth.currentHealth / 10f; // Entsprechend der aktuellen Gesundheit des Spielers

        // Wenn die Gesundheit 0 erreicht, zeige den GameOver-Screen an
        if (playerHealth.currentHealth <= 0 && gameOverScreen != null)
        {
            gameOverScreen.ShowGameOverScreen(); // Zeige den GameOverScreen an
        }
    }
}
