using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    public GameObject birdPrefab;  // Prefab des Vogels
    public Transform leftSpawnPoint;  // Startposition der Vögel von links
    public Transform rightSpawnPoint; // Startposition der Vögel von rechts
    public float spawnInterval = 5f;  // Intervall für das Spawnen der Vögel
    public float spawnDuration = 120f;  // Dauer des Spawnens in Sekunden (2 Minuten)
    public float minYOffset = -8f; // Untere Grenze der zufälligen Höhe
    public float maxYOffset = 8f;  // Obere Grenze der zufälligen Höhe

    private float elapsedTime = 0f;  // Zeit, die seit Beginn vergangen ist

    void Start()
    {
        // Startet den Spawning-Prozess
        InvokeRepeating("SpawnBird", 0f, spawnInterval);
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;  // Zählt die vergangene Zeit

        // Stoppt das Spawnen nach der angegebenen Dauer
        if (elapsedTime >= spawnDuration)
        {
            CancelInvoke("SpawnBird");
        }
    }

    void SpawnBird()
    {
        if (birdPrefab == null)
        {
            Debug.LogError("birdPrefab ist null! Überprüfe die Zuweisung im Inspektor.");
            return;
        }

        // Zufällig zwischen linker und rechter Spawnposition wählen
        bool spawnFromLeft = Random.value > 0.5f;
        Transform chosenSpawnPoint = spawnFromLeft ? leftSpawnPoint : rightSpawnPoint;

        // Zufällige Y-Position für den Vogel festlegen
        float randomYOffset = Random.Range(minYOffset, maxYOffset);
        Vector3 spawnPosition = new Vector3(chosenSpawnPoint.position.x, chosenSpawnPoint.position.y + randomYOffset, chosenSpawnPoint.position.z);

        // Vogel instanziieren
        GameObject birdInstance = Instantiate(birdPrefab, spawnPosition, Quaternion.identity);

        // Skalierung anpassen (z. B. kleiner machen)
        Vector3 scale = birdInstance.transform.localScale * 0.4f; // Verkleinern

        if (!spawnFromLeft)
        {
            // Spiegelung entlang der X-Achse, wenn der Vogel von rechts kommt
            scale.x = Mathf.Abs(scale.x); // X-Spiegelung (negativ)
        }
        else
        {
            // Normale Skalierung, wenn der Vogel von links kommt
            scale.x = -Mathf.Abs(scale.x); // X bleibt positiv
        }

        birdInstance.transform.localScale = scale; // Skalierung setzen

        // Ziel (treeTarget) dem Bird-Skript zuweisen
        Bird birdScript = birdInstance.GetComponent<Bird>();
        if (birdScript != null)
        {
            GameObject tree = GameObject.FindWithTag("Tree");
            if (tree != null)
            {
                birdScript.treeTarget = tree.transform;
            }
            else
            {
                Debug.LogError("Kein Baum mit Tag 'Tree' in der Szene gefunden!");
            }
        }
        else
        {
            Debug.LogError("Das Bird-Skript wurde nicht im Prefab gefunden.");
        }
    }
}
