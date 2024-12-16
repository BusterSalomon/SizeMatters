using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LVL3Cutscene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(NextScene());
    }

    // Update is called once per frame
    public IEnumerator NextScene(){
        yield return new WaitForSeconds(30f);
        SceneManager.LoadScene(3);
    }
}
