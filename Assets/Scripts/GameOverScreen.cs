using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    public GameObject gameOverUI; // Das UI-Element für den GameOver-Bildschirm

    // Methode zum Anzeigen des GameOverScreens
    public void ShowGameOverScreen()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);  // Aktiviert den GameOverScreen
            Time.timeScale = 0f;         // Pausiert das Spiel (optional)
        }
    }

    // Methode zum Verstecken des GameOverScreens (falls gewünscht)
    public void HideGameOverScreen()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false); // Deaktiviert den GameOverScreen
        }
    }
}