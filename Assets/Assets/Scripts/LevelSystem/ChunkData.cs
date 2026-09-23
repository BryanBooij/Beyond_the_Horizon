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
}