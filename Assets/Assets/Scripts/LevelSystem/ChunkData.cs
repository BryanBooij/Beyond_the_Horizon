using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChunkData
{
    public string chunkName;

    public List<SpawnInstruction> spawnInstructions;

    [Header("Chunk Completion")]
    public int maxActiveEnemies = 1;
    public float maxDuration = 20f;
    
    [Header("Dialogue")]
    public bool dialogueEnabled = false;
    [TextArea(2, 5)]
    public string dialogueText;
    public float dialogueDuration = 5f;
}