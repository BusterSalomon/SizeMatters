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
    public UnityEvent<string> DidWinSwitchToScene;
    public string nextScene = "";
    private GameState gameState = GameState.Running;
    public enum GameState
    {
        Running,
        Lost,
        Won,
    }

    protected virtual void Update()
    {
        bool gameIsRunning = gameState == GameState.Running;
        if (DidLose() && gameIsRunning)
        {
            DidLoseEvent.Invoke();
            gameState = GameState.Won;
        }
        if (DidWin() && gameIsRunning)
        {
            Debug.Log("level complete!");
            DidWinEvent.Invoke();
            gameState = GameState.Lost;

            if (nextScene != "")
            {
                DidWinSwitchToScene.Invoke(nextScene);
            }
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
