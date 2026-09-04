using UnityEngine;

public class AstroidSpawner : MonoBehaviour
{
    public GameObject[] asteroidPrefabs; // list to add as many astroids as preferred
    public float spawnInterval = 1f;
    public float minY = -4.1f;
    public float maxY = 4.1f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnAsteroid), 0f, spawnInterval); // every second repeat SpawnAstroid to spawn a astroid
    }

    void SpawnAsteroid()
    {
        if (asteroidPrefabs == null || asteroidPrefabs.Length == 0) return; // check to see if a minimum of 1 proper prefabs is loaded

        float randomY = Random.Range(minY, maxY); // randomize spawn Vertical location 
        Vector3 spawnPos = new Vector3(transform.position.x, randomY, 0f); // spawn astroid on previous randomized location

        GameObject prefabToSpawn = asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)]; // randomize which astroid from the list is spawned
        Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
    }
}