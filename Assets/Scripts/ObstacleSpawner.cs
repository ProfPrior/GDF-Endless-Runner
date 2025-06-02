using UnityEngine;

public class ObstacleSpawner : MonoBehaviour {

    public float spawnInterval = 2.0f; // Zeit zwischen den Spawns
    float maxSpawnInterval = 3.0f; // Maximale Zeit zwischen den Spawns
    float minSpawnInterval = 1.0f; // Minimale Zeit zwischen den Spawns
    int numberOfObstacles;
    float spawnTimer = 0.0f; // Timer für den Spawn
    float spawnDistance = 20.0f; // Distanz zum Spieler, wo das Hindernis erscheinen soll

    public GameObject obstaclePrefab; // Prefab für das Hindernis
    Transform playerPosition; // Referenz zum Spieler

    float[] lanes = {-2.0f, 0.0f, 2.0f}; // Positionen der Bahnen

    void Start() {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform; // Spieler finden
    }

    // Update is called once per frame
    void Update() {
        spawnTimer += Time.deltaTime; // Timer aktualisieren
        if (spawnTimer >= spawnInterval) {
            
            numberOfObstacles = Random.Range(1, 3); // Zufällige Anzahl an Hindernissen
            for (int i = 0; i < numberOfObstacles; i++) {
                SpawnObstacle();
            }
            spawnTimer = 0; // Timer zurücksetzen
            spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval); // Zufällige Spawn-Intervalle
        }
    }

    void SpawnObstacle() {
        int randomLane = Random.Range(0, lanes.Length); // Zufällige Bahn auswählen
        float lanePosition = lanes[randomLane]; // Position der Bahn
        Vector3 spawnPosition = new Vector3(lanePosition, 0.5f, playerPosition.position.z + spawnDistance);
        // Hier den Code zum Spawnen des Hindernisses einfügen
        // Zum Beispiel:
        Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
        Debug.Log("Obstacle spawned!");
    }
}
