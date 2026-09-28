using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChunkData
{
    public string chunkName;

    public List<SpawnInstruction> spawnInstructions;

    [Header("Chunk Completion")]
    public bool useMaxActiveEnemies = true;
    public int maxActiveEnemies = 1;

    public bool useTimer = true;
    public float maxDuration = 20f;
    
    [Header("Dialogue")]
    public bool dialogueEnabled = false;
    [TextArea(2, 5)]
    public string dialogueText;
    public float dialogueDuration = 5f;
}