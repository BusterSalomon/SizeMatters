using UnityEngine;

public class Level4Manager : LevelManager
{
    private Health characterHealth;
    private DoorScript door;

    private bool hasLoggedWin = false; // Ensure win is logged only once
    private bool hasLoggedLose = false; // Ensure loss is logged only once

    public void Start()
    {
        door = FindObjectOfType<DoorScript>(); // Find the door with DoorScript
        characterHealth = GameObject.FindGameObjectWithTag("Character").GetComponent<Health>();
    }

    public override bool DidWin()
    {
        if (!hasLoggedWin && door != null && !door.locked) // Check if the door is unlocked
        {
            Debug.Log("You Win");
            hasLoggedWin = true; // Prevent further logs
            return true;
        }
        return false;
    }

    public override bool DidLose()
    {
        if (!hasLoggedLose && characterHealth.currentHealth <= 0) // Check if player health is 0
        {
            Debug.Log("You Lose");
            hasLoggedLose = true; // Prevent further logs
            return true;
        }
        return false;
    }
}
