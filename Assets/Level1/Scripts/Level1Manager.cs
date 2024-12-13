using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Manager : LevelManager
{

    private EnemyVirus BossEnemy;
    private Health characterHealth;
    public bool WinConditionMet = false;
    public bool LoseConditionMet = false;

    public void Start()
    {
        characterHealth = GameObject.FindGameObjectWithTag("Character").GetComponent<Health>();
    }
    public override bool DidWin()
    {
        if( GameObject.Find("BossVirus") == null ){
            return true;
        }
        return false;
    }

    public override bool DidLose()
    {
        return  characterHealth.currentHealth <= 0;
    }
}

