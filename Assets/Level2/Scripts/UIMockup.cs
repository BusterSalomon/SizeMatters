using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMockup : MonoBehaviour
{

    [SerializeField] private GameObject GameOverScreen;
    [SerializeField] private GameObject NextLevel;

    private void Start()
    {
        
    }

    public void LogWin()
    {
        UnityEngine.Debug.Log("You won!");
        NextLevel.SetActive(true);
    }

    public void LogLose()
    {
        UnityEngine.Debug.Log("You lost!");
        GameOverScreen.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Home()
    {
        SceneManager.LoadScene(0);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void NextLevelButton()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        // Check if the next scene index is within the valid range
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            // Handle end-of-game logic here
            UnityEngine.Debug.Log("Congratulations! You've completed the last level.");
            // Example: Load home screen or show end game screen
            SceneManager.LoadScene(0); // Optional - replace 0 with your home scene index
        }
    }

    public void Level1()
    {
        SceneManager.LoadScene(1);
    }
    public void Level2()
    {
        SceneManager.LoadScene(2);
    }
      public void Level3()
    {
        SceneManager.LoadScene(3);
    }
}

