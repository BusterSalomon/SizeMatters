using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBar : MonoBehaviour
{
    public Transform posUp;    // Zielposition, wenn "up" gedrückt wird
    public Transform posDown;  // Zielposition, wenn "down" gedrückt wird
    public float speed = 2.0f; // Bewegungsgeschwindigkeit

    private Vector3 targetPos; // Aktuelle Zielposition
    private bool isMoving = false; // Flag, ob sich die Bar bewegt

    private void Update()
    {
        // Nur bewegen, wenn isMoving aktiv ist
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

            // Bewegung stoppen, wenn Ziel erreicht ist
            if (Vector2.Distance(transform.position, targetPos) < 0.05f)
            {
                isMoving = false;
            }
        }
    }

    // Methode, um die Bar nach oben zu bewegen
    public void MoveToUp()
    {
        targetPos = posUp.position;
        isMoving = true;
    }

    // Methode, um die Bar nach unten zu bewegen
    public void MoveToDown()
    {
        targetPos = posDown.position;
        isMoving = true;
    }

    // Methode, um die Bewegung sofort zu stoppen
    public void StopMoving()
    {
        isMoving = false;
    }
}
