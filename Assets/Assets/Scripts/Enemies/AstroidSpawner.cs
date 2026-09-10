using UnityEngine;

public class AstroidSpawner : MonoBehaviour
{
    public GameObject[] asteroidPrefabs;
    public float spawnInterval = 1f;

    private float minY;
    private float maxY;

    void Start()
    {
        InvokeRepeating(nameof(SpawnAsteroid), 0f, spawnInterval);
    }

    void SpawnAsteroid()
    {
        if (asteroidPrefabs == null || asteroidPrefabs.Length == 0)
        {
            return;
        }
        GameObject prefabToSpawn = asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)];
        // keep astroids spawning in frame
        Camera cam = Camera.main;
        float cameraHeight = cam.orthographicSize;
        Collider2D asteroidCollider = prefabToSpawn.GetComponent<Collider2D>();
        float halfAsteroidHeight = 0f;
        // keeping astroids perfectly in frame
        if (asteroidCollider != null)
        {
            halfAsteroidHeight = asteroidCollider.bounds.extents.y;
        }
        
        minY = cam.transform.position.y - cameraHeight + halfAsteroidHeight;
        maxY = cam.transform.position.y + cameraHeight - halfAsteroidHeight;
        
        float randomY = Random.Range(minY, maxY);

        Vector3 spawnPos = new Vector3(transform.position.x, randomY, 0f);

        Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
    }
}