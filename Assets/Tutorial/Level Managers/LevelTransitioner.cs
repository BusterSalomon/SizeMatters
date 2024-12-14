using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransitioner : MonoBehaviour
{
    // Start is called before the first frame update
    public void TransitionToLevel (string name)
    {
        SceneManager.LoadScene(sceneName: name);
    }
}
