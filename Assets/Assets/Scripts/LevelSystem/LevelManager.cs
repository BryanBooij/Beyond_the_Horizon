using Assets.Scripts.Game;
using Assets.Scripts.LevelSystem;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [Header("Level")]
    public LevelData levelData;

    [Header("References")]
    public SpawnManager spawnManager;
    public DialogueManager dialogueManager;

    [Header("UI")]
    public GameObject victoryScreen;

    private int currentChunkIndex = -1;
    private int activeEnemies = 0;

    private float chunkTimer = 0f;
    private bool spawningFinished = false;
    private bool chunkFinished = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartNextChunk();
    }

    private void Update()
    {
        if (chunkFinished)
            return;

        ChunkData chunk = levelData.chunks[currentChunkIndex];

        chunkTimer += Time.deltaTime;

        // TIMER
        if (chunk.useTimer && chunkTimer >= chunk.maxDuration)
        {
            Debug.Log("Chunk timer finished.");
            FinishChunk();
            return;
        }

        // ACTIVE ENEMIES
        if (chunk.useMaxActiveEnemies &&
            spawningFinished &&
            activeEnemies <= chunk.maxActiveEnemies)
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

        ChunkData chunk = levelData.chunks[currentChunkIndex];

        if (chunk.dialogueEnabled)
        {
            dialogueManager.ShowDialogue(
                chunk.dialogueText,
                chunk.dialogueDuration,
                ContinueAfterChunk
            );
        }
        else
        {
            ContinueAfterChunk();
        }
    }

    private void ContinueAfterChunk()
    {
        if (currentChunkIndex >= levelData.chunks.Count - 1)
        {
            LevelComplete();
            return;
        }

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

        if (ScoreManager.Instance != null)
        {
            // Haal de echte score op uit ScoreManager
            int currentScore = ScoreManager.Instance.CurrentScore;

            // Update algemene highscore + Victory Screen highscore
            ScoreManager.Instance.HighScoreUpdate();

            // Sterren berekenen op basis van de echte score
            int starsEarned = 1;

            if (currentScore >= 10)
                starsEarned = 2;

            if (currentScore >= 50)
                starsEarned = 3;

            CurrentLevel.starsEarned = starsEarned;

            // Level-progress + level-highscore opslaan
            LevelProgress.CompleteLevel(
                CurrentLevel.planet,
                CurrentLevel.level,
                CurrentLevel.starsEarned,
                currentScore
            );
        }

        if (victoryScreen != null)
        {
            victoryScreen.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}