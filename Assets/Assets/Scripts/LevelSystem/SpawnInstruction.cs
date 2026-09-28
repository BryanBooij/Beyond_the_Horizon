using UnityEngine;

[System.Serializable]
public class SpawnInstruction
{
    [Header("Enemy")]
    public GameObject enemyPrefab;

    [Header("Amount")]
    public int amount = 1;

    [Header("Timing")]
    public float startDelay = 0f;
    public float minInterval = 1.5f;
    public float maxInterval = 3f;
}