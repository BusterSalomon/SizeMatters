using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene(12);
    }
    public void LoadTutorial()
    {
        SceneManager.LoadScene(7);
    }
    public void QuitGame()
    {
    Debug.Log("QUIT!");
    Application.Quit();
    }
}
