using UnityEngine;

public class AstroidSpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public float spawnInterval = 1f;
    public float minY = -4.1f;
    public float maxY = 4.1f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnAsteroid), 0f, spawnInterval);
    }

    void SpawnAsteroid()
    {
        float randomY = Random.Range(minY, maxY);
        Vector3 spawnPos = new Vector3(transform.position.x, randomY, 0f);
        Instantiate(asteroidPrefab, spawnPos, Quaternion.identity);
    }
}