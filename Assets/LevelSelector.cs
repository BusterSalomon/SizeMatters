using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    // Loads a scene based on its name
    public void LoadLevel(string levelName)
    {
        if (Application.CanStreamedLevelBeLoaded(levelName))
        {
            SceneManager.LoadScene(levelName);
        }
        else
        {
            Debug.LogError("The scene " + levelName + " is not included in the Build Settings!");
        }
    }

    // Loads a scene based on its index in the Build Settings
    public void LoadLevelByIndex(int levelIndex)
    {
        if (levelIndex >= 0 && levelIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(levelIndex);
        }
        else
        {
            Debug.LogError("The scene index " + levelIndex + " is invalid!");
        }
    }
}
