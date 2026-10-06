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
    private int activeSpaceships = 0;

    private float chunkTimer = 0f;
    private bool spawningFinished = false;
    private bool chunkFinished = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (CutsceneManager.Instance != null &&
            CutsceneManager.Instance.HasStartCutscene())
        {
            return;
        }

        StartNextChunk();
    }

    public void StartLevelAfterCutscene()
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
            dialogueManager.ShowSequence(
                chunk.dialogueLines,
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

        if (activeEnemies < 0)
            activeEnemies = 0;

        Debug.Log("Enemy destroyed. Active enemies: " + activeEnemies);
    }

    // =========================
    // SPACESHIP LIMIT
    // =========================

    public bool CanSpawnSpaceship()
    {
        Debug.Log(
            "CanSpawnSpaceship? Active: " +
            activeSpaceships +
            " / Max: " +
            levelData.maxActiveSpaceships
        );

        return activeSpaceships < levelData.maxActiveSpaceships;
    }

    public void SpaceshipSpawned()
    {
        activeSpaceships++;

        Debug.Log("SPACESHIP SPAWNED → Active: " + activeSpaceships);
    }

    public void SpaceshipDestroyed()
    {
        activeSpaceships--;

        if (activeSpaceships < 0)
            activeSpaceships = 0;

        Debug.Log("SPACESHIP DESTROYED → Active: " + activeSpaceships);
    }

    private void LevelComplete()
    {
        Debug.Log("LEVEL COMPLETE!");

        if (ScoreManager.Instance != null)
        {
            // Get the actual score from ScoreManager
            int currentScore = ScoreManager.Instance.CurrentScore;

            // Update the highscore for this specific level
            ScoreManager.Instance.LevelHighScoreUpdate();

            // Calculate stars based on the actual score
            int starsEarned = 1;

            if (currentScore >= levelData.threeStarScore)
            {
                starsEarned = 3;
            }
            else if (currentScore >= levelData.twoStarScore)
            {
                starsEarned = 2;
            }

            CurrentLevel.starsEarned = starsEarned;

            // Save level progress and level highscore
            LevelProgress.CompleteLevel(
                CurrentLevel.planet,
                CurrentLevel.level,
                CurrentLevel.starsEarned,
                currentScore
            );
        }

        if (CutsceneManager.Instance != null &&
            CutsceneManager.Instance.HasEndCutscene())
        {
            CutsceneManager.Instance.StartEndCutscene();
        }
        else
        {
            ShowVictoryScreen();
        }
    }

    public void ShowVictoryScreen()
    {
        if (victoryScreen != null)
        {
            victoryScreen.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}