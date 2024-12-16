using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public abstract class LevelManager : MonoBehaviour
{
    public UnityEvent DidWinEvent;
    public UnityEvent DidLoseEvent;
    public UnityEvent<string> DidWinSwitchToScene;
    public string nextScene = "";
    public float TransitionDelay = 2f; 
    private GameState gameState = GameState.Running;
    private float gamestateChangeTime;
    private bool didWinOnce = false;
    private bool didLoseOnce = false;


    public List<GameObject> GameObjectsToHideOnLose;

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
            gameState = GameState.Lost;
            gamestateChangeTime = Time.time;
        }
        if (DidWin() && gameIsRunning)
        {
            Debug.Log("level complete!");
            gameState = GameState.Won;
            gamestateChangeTime = Time.time;

            
        }

        if (!didWinOnce && gameState == GameState.Won && nextScene != "" && Time.time - gamestateChangeTime > TransitionDelay)
        {
            DidWinEvent.Invoke();
            SceneManager.LoadScene(sceneName: nextScene);
            DidWinSwitchToScene.Invoke(nextScene);
        }

        if (!didLoseOnce && gameState == GameState.Lost && nextScene != "" && Time.time - gamestateChangeTime > TransitionDelay)
        {
            foreach (GameObject GO in GameObjectsToHideOnLose)
            {
                GO.SetActive(false);
            }

            DidLoseEvent.Invoke();
            SceneManager.LoadScene(sceneName: nextScene);
            DidWinSwitchToScene.Invoke(nextScene);
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
