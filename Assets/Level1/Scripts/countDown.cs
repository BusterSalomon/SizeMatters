using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class countDown : MonoBehaviour
{
    public TextMeshPro timer;
    private float counter;
    private Vector2 originalScale;
    // Reference to the SpriteRenderer on the enemy for color change
    private SpriteRenderer enemySpriteRenderer;

    public Color targetColor = Color.red;
    public Color targetColor2 = Color.blue;
    int check = 0; 

        private void Awake()
    {
        originalScale = transform.localScale;
        enemySpriteRenderer = GetComponentInParent<SpriteRenderer>(); // Find the parent enemy's SpriteRenderer
    }

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

    void Update()
    {
        if (counter > 0)
        {
            counter -= Time.deltaTime;
            timer.text = Mathf.Ceil(counter).ToString();
        }
        else
        {
            transform.localScale = new Vector2(Mathf.Abs(originalScale.x), originalScale.y);
            timer.text = "0";
            if (check == 0)
            {
                enemySpriteRenderer.color = targetColor;
                check++;
                RestartCounter();
            }
            else if (check == 1) {
                enemySpriteRenderer.color = targetColor2;
                ResetCounter();
                check++; 
            }          
        }
    }
}
