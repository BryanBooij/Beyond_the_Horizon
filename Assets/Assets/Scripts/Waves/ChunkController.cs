using UnityEngine;

public class ChunkController : MonoBehaviour
{
    public int enemiesRemaining;

    private LevelManager levelManager;
    private bool chunkCompleted = false;
    private bool spawningFinished = false;

    void Start()
    {
        levelManager = FindFirstObjectByType<LevelManager>();

        Debug.Log("Chunk started: " + gameObject.name);
    }

    public void EnemySpawned()
    {
        enemiesRemaining++;

        Debug.Log("Enemy spawned. Remaining: " + enemiesRemaining);
    }

    public void SpawningFinished()
    {
        spawningFinished = true;

        Debug.Log("Spawning finished.");

        CheckChunkComplete();
    }

    public void EnemyDestroyed()
    {
        if (chunkCompleted)
            return;

        enemiesRemaining--;

        Debug.Log("Enemy destroyed. Remaining: " + enemiesRemaining);

        CheckChunkComplete();
    }

    private void CheckChunkComplete()
    {
        Debug.Log(
            "CHECK | SpawningFinished: " + spawningFinished +
            " | Remaining: " + enemiesRemaining
        );

        if (chunkCompleted)
            return;

        if (!spawningFinished)
            return;

        if (enemiesRemaining > 0)
            return;

        chunkCompleted = true;

        Debug.Log("CHUNK COMPLETE! → NEXT CHUNK");

        levelManager.ChunkComplete();
    }
}