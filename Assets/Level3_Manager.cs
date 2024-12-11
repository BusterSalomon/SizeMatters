using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Manager : LevelManager
{
    private TreeHealth treeHealth; // Referenz auf den Baum
    private CountdownTimer countdownTimer; // Referenz auf den Countdown

    public void Start()
    {
        // Initialisiere TreeHealth (Baum-Lebenssystem)
        GameObject tree = GameObject.Find("Tree");
        if (tree != null)
        {
            treeHealth = tree.GetComponent<TreeHealth>();
        }
        else
        {
            Debug.LogError("Tree-Objekt wurde nicht gefunden.");
        }

        // Initialisiere CountdownTimer
        GameObject countdownObject = GameObject.Find("CountdownText");
        if (countdownObject != null)
        {
            countdownTimer = countdownObject.GetComponent<CountdownTimer>();
        }
        else
        {
            Debug.LogError("CountdownText-Objekt wurde nicht gefunden.");
        }
    }

    // Überprüft, ob der Spieler gewonnen hat (Countdown abgelaufen)
    public override bool DidWin()
    {
        if (countdownTimer != null)
        {
            return countdownTimer.IsTimeUp(); // Prüft, ob die Zeit abgelaufen ist
        }
        return false;
    }

    // Überprüft, ob der Spieler verloren hat (Baum zerstört)
    public override bool DidLose()
    {
        if (treeHealth != null)
        {
            return treeHealth.health <= 0; // Prüft, ob die Gesundheit des Baums 0 oder kleiner ist
        }
        return false;
    }
}
