using System;
using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Spawn Area")]
    [SerializeField] private float topNoSpawnZone = 1.5f;
    [SerializeField] private float bottomNoSpawnZone = 1.5f;

    public void ExecuteSpawnInstruction(
        SpawnInstruction instruction,
        Action onComplete)
    {
        StartCoroutine(SpawnRoutine(instruction, onComplete));
    }

    private IEnumerator SpawnRoutine(
        SpawnInstruction instruction,
        Action onComplete)
    {
        if (instruction.enemyPrefab == null)
        {
            Debug.LogWarning("SpawnInstruction has no enemy prefab assigned.");
            onComplete?.Invoke();
            yield break;
        }

        // Wacht voordat de eerste enemy gespawned wordt
        if (instruction.startDelay > 0f)
        {
            yield return new WaitForSeconds(instruction.startDelay);
        }

        for (int i = 0; i < instruction.amount; i++)
        {
            // Alleen EnemySpaceship moet wachten op de limiet
            if (instruction.enemyPrefab.CompareTag("Enemyspaceship"))
            {
                LevelManager levelManager = FindFirstObjectByType<LevelManager>();

                if (levelManager != null)
                {
                    // Wacht totdat er weer ruimte is
                    yield return new WaitUntil(
                        () => levelManager.CanSpawnSpaceship()
                    );
                }
            }

            SpawnEnemy(instruction);

            // Geen interval nodig na de laatste enemy
            if (i < instruction.amount - 1)
            {
                float interval = UnityEngine.Random.Range(
                    instruction.minInterval,
                    instruction.maxInterval
                );

                yield return new WaitForSeconds(interval);
            }
        }

        onComplete?.Invoke();
    }

    private void SpawnEnemy(SpawnInstruction instruction)
    {
        Camera cam = Camera.main;
        if (cam == null)
        {
            Debug.LogError("No Main Camera found!");
            return;
        }

        LevelManager levelManager = FindFirstObjectByType<LevelManager>();
        if (levelManager == null)
        {
            Debug.LogError("No LevelManager found!");
            return;
        }

        float cameraHeight = cam.orthographicSize;
        Collider2D enemyCollider =
            instruction.enemyPrefab.GetComponent<Collider2D>();
        float halfEnemyHeight = 0f;
        if (enemyCollider != null)
        {
            halfEnemyHeight = enemyCollider.bounds.extents.y;
        }
        float minY = cam.transform.position.y
                     - cameraHeight
                     + halfEnemyHeight
                     + bottomNoSpawnZone;
        float maxY = cam.transform.position.y
                     + cameraHeight
                     - halfEnemyHeight
                     - topNoSpawnZone;
        float randomY = UnityEngine.Random.Range(minY, maxY);
        Vector3 spawnPosition = new Vector3(
            transform.position.x,
            randomY,
            transform.position.z
        );
        GameObject enemy = Instantiate(
            instruction.enemyPrefab,
            spawnPosition,
            Quaternion.identity
        );
        // Only Enemyspaceships count for the limit
        if (enemy.CompareTag("Enemyspaceship"))
        {
            levelManager.SpaceshipSpawned();
        }
        LevelEnemy levelEnemy = enemy.GetComponent<LevelEnemy>();

        if (levelEnemy == null)
        {
            levelEnemy = enemy.AddComponent<LevelEnemy>();
        }
        levelEnemy.levelManager = levelManager;
    }
}