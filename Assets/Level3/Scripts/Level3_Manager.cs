using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Manager : LevelManager
{
    private CountdownTimer countdownTimer; // Referenz auf das CountdownTimer-Skript
    private Health playerHealth; // Referenz auf das Health-Skript

    private void Start()
    {
        // Verknüpfen der benötigten Komponenten
        countdownTimer = FindObjectOfType<CountdownTimer>(); // Sucht das CountdownTimer-Skript in der Szene
        playerHealth = GameObject.FindGameObjectWithTag("Tree").GetComponent<Health>();

        // Fehlerüberprüfung
        if (countdownTimer == null)
        {
            Debug.LogError("CountdownTimer-Skript wurde nicht gefunden. Bitte stelle sicher, dass es in der Szene vorhanden ist.");
        }

        if (playerHealth == null)
        {
            Debug.LogError("Health-Skript wurde nicht gefunden. Bitte stelle sicher, dass das Charakter-Objekt das Health-Skript enthält.");
        }
    }

    // Überprüfen, ob man gewonnen hat (Zeit ist abgelaufen)
    public override bool DidWin()
    {
        return countdownTimer != null && countdownTimer.IsTimeUp();
    }

    // Überprüfen, ob man verloren hat (keine Gesundheit mehr)
    public override bool DidLose()
    {
        return playerHealth != null && playerHealth.currentHealth <= 0;
    }
}
