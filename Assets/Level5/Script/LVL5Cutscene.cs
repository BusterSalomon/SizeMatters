using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LVL5Cutscene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(NextScene());
    }

    // Update is called once per frame
    public IEnumerator NextScene(){
        yield return new WaitForSeconds(17.9f);
        SceneManager.LoadScene(5);
    }
}
