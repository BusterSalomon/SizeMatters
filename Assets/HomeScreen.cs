using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeScreen : MonoBehaviour

{
    [SerializeField] private GameObject Canvas;

    // Start is called before the first frame update
   public void StartGame()
    {
        // Load the home screen (assumed to be at build index 0)
        SceneManager.LoadScene(1);
    }
}
