using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMockup : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject nextLevelScreen;

    private void Start()
    {
        // Ensure UI elements are inactive initially
        if (gameOverScreen != null) 
            gameOverScreen.SetActive(false);
            Debug.Log("Game Over Screen is there");

        if (nextLevelScreen != null) 
            nextLevelScreen.SetActive(false);
    }

    public void LogWin()
    {
        //Debug.Log("You won!");
        if (nextLevelScreen != null)
        {
            nextLevelScreen.SetActive(true);
        }
        else
        {
            Debug.LogWarning("NextLevelScreen is not assigned!");
        }
    }

    public void LogLose()
    {
        //Debug.Log("You lost!");
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }
        else
        {
            Debug.LogWarning("GameOverScreen is not assigned!");
        }
    }

    public void Restart()
    {
        // Reloads the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Home()
    {
        // Load the home screen (assumed to be at build index 0)
        SceneManager.LoadScene(0);
    }

    public void StartGame()
    {
        // Load the first playable level (assumed to be at build index 1)
        if (SceneManager.sceneCountInBuildSettings > 1)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            Debug.LogWarning("There are not enough scenes in the build settings.");
        }
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
            Debug.Log("Congratulations! You've completed the last level.");
            // Load home screen or show end game screen (replace 0 with your home screen index if different)
            SceneManager.LoadScene(0);
        }
    }

    public void LoadLevel(string levelName)
    {
        // Load a scene by name
        if (Application.CanStreamedLevelBeLoaded(levelName))
        {
            SceneManager.LoadScene(levelName);
        }
        else
        {
            Debug.LogWarning($"The scene '{levelName}' cannot be loaded. Please check the scene name and Build Settings.");
        }
    }

    public void LoadLevelByIndex(int index)
    {
        // Load a scene by index
        if (index >= 0 && index < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(index);
        }
        else
        {
            Debug.LogWarning("Invalid scene index. Please check your build settings.");
        }
    }
}
