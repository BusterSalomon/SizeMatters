using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlattformController : MonoBehaviour
{
    public Transform posA, posB;
    public float speed; 
    Vector3 targetPos;

    private void Start()
    {
        targetPos = posB.position;
    }

    private void Update()
    {
        // Bewegungslogik der Plattform
        if (Vector2.Distance(transform.position, posA.position) < 0.05f) 
        {
            targetPos = posB.position;
        }

        if (Vector2.Distance(transform.position, posB.position) < 0.05f) 
        {
            targetPos = posA.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Character")) // Tag ändern, falls dein Charakter "Player" ist
        {
            collision.transform.SetParent(this.transform); // Charakter wird Kind der Plattform
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Character")) // Tag ändern, falls dein Charakter "Player" ist
        {
            collision.transform.SetParent(null); // Entfernt Charakter von der Plattform
        }
    }
}
 