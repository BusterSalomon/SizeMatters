using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public abstract class TutorialManager : MonoBehaviour
{
    public List<GameObject> GameObjectsToShowOnComplete;
    public string NextTutorial;
    private bool levelComplete = false;
    protected virtual void Update()
    {
        if (!levelComplete && DidComplete())
        {
            Debug.Log("level complete!");
            levelComplete = true;
            OnDidComplete();
        }
    }
    public abstract bool DidComplete();

    private void OnDidComplete()
    {
        foreach (GameObject GO in GameObjectsToShowOnComplete)
        {
            GO.SetActive(true);
        }
    }

    public void OnNextTutorialBtnClick ()
    {
        SceneManager.LoadScene(sceneName: NextTutorial);
    }
}
