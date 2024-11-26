using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftBoard : MonoBehaviour
{
    public Transform posLU; // Zielposition für left_up
    public Transform posLD; // Zielposition für left_down
    public float speed;     // Bewegungsgeschwindigkeit

    private Vector3 targetPos; // Zielposition
    private bool isMoving = false; // Flag für die Bewegung

    private void Start()
    {
        Debug.Log("LeftBoard gestartet. Initialisiere...");
        
        if (posLU == null || posLD == null)
        {
            Debug.LogError("Zielpositionen (posLU oder posLD) sind nicht zugewiesen!");
        }
        else
        {
            Debug.Log("Zielpositionen korrekt zugewiesen. posLU: " + posLU.position + ", posLD: " + posLD.position);
        }
    }

    private void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            Debug.Log("Bewege LeftBoard. Aktuelle Position: " + transform.position + " | Ziel: " + targetPos);

            if (Vector2.Distance(transform.position, targetPos) < 0.05f)
            {
                isMoving = false;
                Debug.Log("Ziel erreicht bei: " + targetPos);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger betreten von: " + collision.gameObject.name);

        if (collision.gameObject.name == "Left_up")
        {
            targetPos = posLU.position;
            isMoving = true;
            Debug.Log("Bewege LeftBoard zu posLU: " + posLU.position);
        }
        else if (collision.gameObject.name == "Left_down")
        {
            targetPos = posLD.position;
            isMoving = true;
            Debug.Log("Bewege LeftBoard zu posLD: " + posLD.position);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Trigger verlassen von: " + collision.gameObject.name);

        if (collision.gameObject.name == "Left_up" || collision.gameObject.name == "Left_down")
        {
            isMoving = false;
            Debug.Log("Bewegung gestoppt: Trigger verlassen");
        }
    }

    private void OnDrawGizmos()
    {
        if (posLU != null && posLD != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(posLU.position, 0.2f);
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(posLD.position, 0.2f);
        }
    }
}
