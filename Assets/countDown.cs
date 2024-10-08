using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class countDown : MonoBehaviour
{
    public TextMeshPro timer;
    private float counter;
    private Vector2 originalScale;
    public float RemainingTime => counter;

    public bool IsCounterZero => counter <= 0;
        public void Start()
    {
        ResetCounter();
    }

    public void RestartCounter()
    {
        ResetCounter();  
    }

    private void ResetCounter()
    {
        counter = Random.Range(5, 10); 
    }
    void Awake()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (counter > 0)
        {
            counter -= Time.deltaTime;
            timer.text = Mathf.Ceil(counter).ToString();
        }
        else
        {
            timer.text = "0";
        }
        transform.localScale = new Vector2(Mathf.Abs(originalScale.x), originalScale.y);
    }
}
