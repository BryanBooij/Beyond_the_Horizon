using UnityEngine;

public class Enemyspaceshipspawner : MonoBehaviour
{
    public GameObject enemySpaceship;
    public float spawnInterval = 5f;
    public float minY = -4.1f;
    public float maxY = 4.1f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 0f, spawnInterval);
    }

    void SpawnEnemy()
    {
        float randomY = Random.Range(minY, maxY);

        Vector3 spawnPos = new Vector3(transform.position.x, randomY, 0f);

        Instantiate(enemySpaceship, spawnPos, Quaternion.identity);
    }
}