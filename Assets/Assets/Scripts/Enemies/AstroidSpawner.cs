using UnityEngine;
using Assets.Scripts.Game;

public class AstroidSpawner : MonoBehaviour
{
    public GameObject[] asteroidPrefabs;
    public float spawnInterval = 2f;
    public int maxAsteroids = 20;

    private float spawnTimer;
    private int spawnedAsteroids = 0;

    private float minY;
    private float maxY;

    void Update()
    {
        if (spawnedAsteroids >= maxAsteroids)
        {
            return;
        }

        float spawnMultiplier = 1f;

        if (DifficultyManager.Instance != null)
        {
            spawnMultiplier =
                DifficultyManager.Instance.astroidSpawnSpeedMultiplier;
        }

        float currentSpawnInterval = spawnInterval / spawnMultiplier;

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= currentSpawnInterval)
        {
            spawnTimer = 0f;
            SpawnAsteroid();
        }
    }

    void SpawnAsteroid()
    {
        if (asteroidPrefabs == null || asteroidPrefabs.Length == 0)
        {
            return;
        }

        GameObject prefabToSpawn =
            asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)];

        Camera cam = Camera.main;

        float cameraHeight = cam.orthographicSize;

        Collider2D asteroidCollider =
            prefabToSpawn.GetComponent<Collider2D>();

        float halfAsteroidHeight = 0f;

        if (asteroidCollider != null)
        {
            halfAsteroidHeight = asteroidCollider.bounds.extents.y;
        }

        minY = cam.transform.position.y
             - cameraHeight
             + halfAsteroidHeight;

        maxY = cam.transform.position.y
             + cameraHeight
             - halfAsteroidHeight;

        float randomY = Random.Range(minY, maxY);

        Vector3 spawnPos =
            new Vector3(transform.position.x, randomY, 0f);

        Instantiate(
            prefabToSpawn,
            spawnPos,
            Quaternion.identity
        );
    }
}