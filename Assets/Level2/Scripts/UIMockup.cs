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
}

