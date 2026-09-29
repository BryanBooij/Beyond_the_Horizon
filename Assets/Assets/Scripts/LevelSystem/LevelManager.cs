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
    public TextMeshProUGUI scoreText;           
    public TextMeshProUGUI victoryFinalScoreText;

    private float chunkTimer = 0f;
    private bool spawningFinished = false;
    private bool chunkFinished = false;
    public int score;

    private void Awake()
    {
        Instance = this;
    }
    
    private void Start()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
        
        StartNextChunk();
    }
    
    public void AddScore(int amount)
    {
        score += amount;

        if (scoreText != null)
            scoreText.text = "Score: " + score;
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
            ScoreManager.Instance.HighScoreUpdate();
        }
        if (victoryScreen != null)
        {
            int starsEarned = 1;
            if (score >= 10) starsEarned = 2;
            if (score >= 50) starsEarned = 3;
            CurrentLevel.starsEarned = starsEarned;

            LevelProgress.CompleteLevel(CurrentLevel.planet, CurrentLevel.level, CurrentLevel.starsEarned, score);
            
            if (victoryFinalScoreText != null)
                victoryFinalScoreText.text = "FinalScore: " + score;
            
            victoryScreen.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}