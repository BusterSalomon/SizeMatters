using UnityEngine;
using TMPro; // Für TextMeshPro-Elemente

public class CountdownTimer : MonoBehaviour
{
    private TextMeshProUGUI countdownText; // TextMeshPro-Element für den Countdown
    private float countdownTime = 120f; // 2 Minuten in Sekunden

    public bool IsTimeUp() // Neue Methode, um abzufragen, ob die Zeit abgelaufen ist
    {
        return countdownTime <= 0;
    }

    void Start()
    {
        // Automatisches Zuweisen des TextMeshPro-Elements anhand des Namens
        countdownText = GameObject.Find("CountdownText").GetComponent<TextMeshProUGUI>();

        // Debugging: Überprüfen, ob das Text-Element gefunden wurde
        if (countdownText == null)
        {
            Debug.LogError("TextMeshPro-Element 'CountdownText' wurde nicht gefunden. Bitte überprüfe den Namen.");
        }
    }

    void Update()
    {
        if (countdownText != null) // Sicherstellen, dass das Text-Element vorhanden ist
        {
            if (countdownTime > 0)
            {
                countdownTime -= Time.deltaTime;

                // Minuten und Sekunden berechnen
                int minutes = Mathf.FloorToInt(countdownTime / 60);
                int seconds = Mathf.FloorToInt(countdownTime % 60);

                // Text aktualisieren
                countdownText.text = $"{minutes:00}:{seconds:00}";
            }
            else
            {
                countdownText.text = "Time's Up!";
            }
        }
    }
}
