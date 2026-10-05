using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Game/Level Data")]
public class LevelData : ScriptableObject
{
    public string levelName;

    [Header("Star Requirements")]
    public int oneStarScore;
    public int twoStarScore;
    public int threeStarScore;
    [Header("Enemy Limit")]
    public int maxActiveSpaceships = 10;
    public List<ChunkData> chunks;
}