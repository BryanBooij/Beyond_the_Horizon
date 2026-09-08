using UnityEngine;

public class Enemyspaceshipspawner : MonoBehaviour
{
    public GameObject Enemyspaceship;
    public float spawnInterval = 5f;
    public float startDelay = 3f;
    public int spawnAmount = 10;

    public float minY = -50f;
    public float maxY = 500f;

    private int spawnedAmount = 0;

    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), startDelay, spawnInterval);
    }

    void SpawnEnemy()
    {
        if (spawnedAmount >= spawnAmount)
        {
            CancelInvoke(nameof(SpawnEnemy));
            return;
        }

        float randomY = Random.Range(minY, maxY);

        Vector3 spawnPos = new Vector3(transform.position.x, randomY, 0f);

        Instantiate(Enemyspaceship, spawnPos, Quaternion.identity);

        spawnedAmount++;
    }
}