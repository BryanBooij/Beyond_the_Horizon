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

    public List<ChunkData> chunks;
}