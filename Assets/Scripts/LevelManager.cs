using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.Events;

public abstract class LevelManager : MonoBehaviour
{
    public UnityEvent DidWinEvent;
    public UnityEvent DidLoseEvent;
    private GameState gameState = GameState.Running;
    public enum GameState
    {
        Running,
        Lost,
        Won,
    }

    void Update()
    {
        bool gameIsRunning = gameState == GameState.Running;
        if (DidLose() && gameIsRunning)
        {
            DidLoseEvent.Invoke();
            gameState = GameState.Won;
        }
        if (DidWin() && gameIsRunning)
        {
            DidWinEvent.Invoke();
            gameState = GameState.Lost;
        }
    }
    /// <summary>
    /// Resets game state to running
    /// </summary>
    public void ResetGameState ()
    {
        gameState = GameState.Running;
    } 

    public abstract bool DidLose();
    public abstract bool DidWin();
}
