using UnityEngine;

public class EnemySpaceshipLevelSpawner : MonoBehaviour
{
    private float minY;
    private float maxY;

    public GameObject Enemyspaceship;
    public GameObject EnemyBoss;
    public float spawnInterval = 15f;
    public float startDelay = 5f;
    public int spawnAmount = 10;

    private int spawnedAmount = 0;

    void Start()
    {
        // keeps spawn area inside the frame
        Camera cam = Camera.main;
        float cameraHeight = cam.orthographicSize;
        Collider2D enemyCollider = Enemyspaceship.GetComponent<Collider2D>();
        float halfEnemyHeight = 0f;

        // ships cant spawn slightly outside the frame
        if (enemyCollider != null)
        {
            halfEnemyHeight = enemyCollider.bounds.extents.y;
        }
        
        minY = cam.transform.position.y - cameraHeight + halfEnemyHeight;
        maxY = cam.transform.position.y + cameraHeight - halfEnemyHeight;

        InvokeRepeating(nameof(SpawnEnemy), startDelay, spawnInterval);
    }

    void SpawnEnemy()
    {
        if (spawnedAmount >= spawnAmount)
        {
            CancelInvoke(nameof(SpawnEnemy));
            SpawnBoss();
            return;
        }

        float randomY = Random.Range(minY, maxY);
        Vector3 spawnPos = new Vector3(transform.position.x, randomY, 0f);

        Instantiate(Enemyspaceship, spawnPos, Quaternion.identity);

        spawnedAmount++;
    }

    void SpawnBoss()
    {
        float randomY = Random.Range(minY, maxY);
        Vector3 spawnPos = new Vector3(transform.position.x, randomY, 0f);
        Instantiate(EnemyBoss, spawnPos, Quaternion.identity);
    }
}