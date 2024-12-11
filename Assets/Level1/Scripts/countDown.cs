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
    
    private SpriteRenderer enemyHeadSpriteRenderer;

    public Color targetColor = Color.green;
    public Color targetColor2 = Color.red;
    public Color targetColor3 = Color.yellow;
    public Color targetColor4 = Color.blue;

    public Color targetColor5 = Color.black;

    int check = 0; 

    //Atempt to fix countdown
    private EnemyVirus emy;

        private void Awake()
    {
        emy = GetComponentInParent<EnemyVirus>();

        Transform head = emy.head;

        originalScale = transform.localScale;
        enemyHeadSpriteRenderer = head.GetComponent<SpriteRenderer>(); // Find the parent enemy's SpriteRenderer
        //enemySpriteRenderer = GetComponentInParent<SpriteRenderer>();
        Debug.Log(enemySpriteRenderer);
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
        Color nextcolor = targetColor;
        if (counter > 0)
        {
            counter -= Time.deltaTime;
            timer.text = Mathf.Ceil(counter).ToString();
        }
        else
        {
            transform.localScale = new Vector2(Mathf.Abs(originalScale.x), originalScale.y);
            timer.text = "0";

            switch(check){
                case 0:
                    nextcolor = targetColor;
                    break;
                case 1:
                    nextcolor = targetColor2;
                    break;
                case 2:
                    nextcolor = targetColor3;
                    break;
                case 3:
                    nextcolor = targetColor4;
                    break;
                case 4:
                    nextcolor = targetColor5;
                    break;
                default:
                    break;
            }
                Debug.Log("status :" + check);
                enemyHeadSpriteRenderer.color = nextcolor;
                ResetCounter();
                emy.mutateVirus(1,check);
                check++;   
        }
    }
}
