using UnityEngine;
using Assets.Scripts.Enemies;

public class Enemyspaceshipspawner : MonoBehaviour
{
    private float minY;
    private float maxY;

    public GameObject Enemyspaceship;
    public float spawnInterval = 15f;
    public float startDelay = 5f;

    public int maxEnemySpaceships = 10;

    private int spawnedEnemySpaceships = 0;

    private ChunkController chunkController;

    void Start()
    {
        chunkController = GetComponentInParent<ChunkController>();

        // keeps spawn area inside the frame
        Camera cam = Camera.main;
        float cameraHeight = cam.orthographicSize;

        Collider2D enemyCollider =
            Enemyspaceship.GetComponent<Collider2D>();

        float halfEnemyHeight = 0f;

        // ships can't spawn slightly outside the frame
        if (enemyCollider != null)
        {
            halfEnemyHeight = enemyCollider.bounds.extents.y;
        }

        minY = cam.transform.position.y
             - cameraHeight
             + halfEnemyHeight;

        maxY = cam.transform.position.y
             + cameraHeight
             - halfEnemyHeight;

        InvokeRepeating(nameof(SpawnEnemy), startDelay, spawnInterval);
    }

    void SpawnEnemy()
    {
        // Stop zodra het maximum bereikt is
        if (spawnedEnemySpaceships >= maxEnemySpaceships)
        {
            CancelInvoke(nameof(SpawnEnemy));
            return;
        }

        Vector3 spawnPos;

        do
        {
            float randomY = Random.Range(minY, maxY);

            spawnPos = new Vector3(
                transform.position.x,
                randomY
            );

        } while (
            ShootButtonCollider.Instance != null &&
            ShootButtonCollider.Instance.OverlapPoint(spawnPos)
        );

        Instantiate(
            Enemyspaceship,
            spawnPos,
            Quaternion.identity
        );

        spawnedEnemySpaceships++;

        if (chunkController != null)
        {
            chunkController.EnemySpawned();

            if (spawnedEnemySpaceships >= maxEnemySpaceships)
            {
                chunkController.SpawningFinished();
            }
        }
    }
}