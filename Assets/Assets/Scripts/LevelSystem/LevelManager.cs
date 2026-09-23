using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Level")]
    public LevelData levelData;

    [Header("References")]
    public SpawnManager spawnManager;

    private int currentChunkIndex = -1;
    private int activeEnemies = 0;

    private float chunkTimer = 0f;
    private bool spawningFinished = false;
    private bool chunkFinished = false;

    private void Start()
    {
        StartNextChunk();
    }

    private void Update()
    {
        if (chunkFinished)
            return;

        chunkTimer += Time.deltaTime;

        ChunkData chunk = levelData.chunks[currentChunkIndex];

        // Timer forceert de volgende chunk
        if (chunkTimer >= chunk.maxDuration)
        {
            Debug.Log("Chunk timer finished.");
            FinishChunk();
            return;
        }

        // Alleen controleren nadat alle enemies gespawned zijn
        if (spawningFinished && activeEnemies <= chunk.maxActiveEnemies)
        {
            Debug.Log("Enough enemies destroyed.");
            FinishChunk();
        }
    }

    private void StartNextChunk()
    {
        currentChunkIndex++;

        if (currentChunkIndex >= levelData.chunks.Count)
        {
            LevelComplete();
            return;
        }

        ChunkData chunk = levelData.chunks[currentChunkIndex];

        Debug.Log("Starting chunk: " + chunk.chunkName);

        chunkTimer = 0f;
        spawningFinished = false;
        chunkFinished = false;
        int instructionsRemaining = chunk.spawnInstructions.Count;

        foreach (SpawnInstruction instruction in chunk.spawnInstructions)
        {
            spawnManager.ExecuteSpawnInstruction(instruction, () =>
            {
                instructionsRemaining--;

                if (instructionsRemaining <= 0)
                {
                    spawningFinished = true;
                    Debug.Log("All spawning finished.");
                }
            });
        }
    }

    private void FinishChunk()
    {
        if (chunkFinished)
            return;

        chunkFinished = true;

        Debug.Log("Chunk finished!");

        StartNextChunk();
    }

    public void EnemySpawned()
    {
        activeEnemies++;

        Debug.Log("Enemy spawned. Active enemies: " + activeEnemies);
    }

    public void EnemyDestroyed()
    {
        activeEnemies--;

        Debug.Log("Enemy destroyed. Active enemies: " + activeEnemies);
    }

    private void LevelComplete()
    {
        Debug.Log("LEVEL COMPLETE!");
    }
}