using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToHomeOnEsc : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            FindObjectOfType<AudioManager>().StopAll();
            SceneManager.LoadScene(0);
        }
    }
}
