using UnityEngine;
using Assets.Scripts.Enemies;

public class Enemyspaceshipspawner : MonoBehaviour
{
    public GameObject[] Enemyspaceship;
    public float spawnInterval = 15f;
    public float startDelay = 5f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), startDelay, spawnInterval);
    }

    void SpawnEnemy()
    {
        if (Enemyspaceship == null || Enemyspaceship.Length == 0)
        {
            return;
        }
        // pick a random enemy prefab
        GameObject prefabToSpawn = Enemyspaceship[Random.Range(0, Enemyspaceship.Length)];
        // keeps spawn area inside the frame
        Camera cam = Camera.main;
        float cameraHeight = cam.orthographicSize;
        Collider2D enemyCollider = prefabToSpawn.GetComponent<Collider2D>();
        float halfEnemyHeight = 0f;
        // ships cant spawn slightly outside the frame
        if (enemyCollider != null)
        {
            halfEnemyHeight = enemyCollider.bounds.extents.y;
        }
        float minY = cam.transform.position.y - cameraHeight + halfEnemyHeight;
        float maxY = cam.transform.position.y + cameraHeight - halfEnemyHeight;
        Vector3 spawnPos;
        do
        {
            float randomY = Random.Range(minY, maxY);
            spawnPos = new Vector3(transform.position.x, randomY, 0f);

        } while (ShootButtonCollider.Instance != null && ShootButtonCollider.Instance.OverlapPoint(spawnPos));

        Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
    }
}