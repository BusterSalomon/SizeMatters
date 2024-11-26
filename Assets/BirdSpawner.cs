using UnityEngine;

public class BirdFlying : MonoBehaviour
{
    public GameObject birdPrefab;  // Prefab des Vogels
    public Transform spawnPoint;   // Startposition der Vögel
    public float spawnInterval = 3f;  // Wie oft Vögel erscheinen

    void Start()
    {
        InvokeRepeating("SpawnBird", 0f, spawnInterval);  // Alle X Sekunden einen Vogel erzeugen
    }

    void SpawnBird()
    {
        Instantiate(birdPrefab, spawnPoint.position, Quaternion.identity);  // Vogel spawnen
    }
}
