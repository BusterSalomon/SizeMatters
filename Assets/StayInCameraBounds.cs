using UnityEngine;

public class StayInCameraBounds : MonoBehaviour
{
    private Camera mainCamera; // Referenz zur Hauptkamera
    private Vector2 screenBounds; // Grenzen des sichtbaren Bereichs
    private float objectWidth;    // Breite des Spielers
    private float objectHeight;   // Höhe des Spielers

    void Start()
    {
        mainCamera = Camera.main; // Hauptkamera holen
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        objectWidth = GetComponent<SpriteRenderer>().bounds.extents.x; // Hälfte der Breite des Spielers
        objectHeight = GetComponent<SpriteRenderer>().bounds.extents.y; // Hälfte der Höhe des Spielers
    }

    void Update()
    {
        // Spielerposition einschränken
        Vector3 viewPosition = transform.position;
        viewPosition.x = Mathf.Clamp(viewPosition.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
        viewPosition.y = Mathf.Clamp(viewPosition.y, screenBounds.y * -1 + objectHeight, screenBounds.y - objectHeight);
        transform.position = viewPosition;
    }
}
