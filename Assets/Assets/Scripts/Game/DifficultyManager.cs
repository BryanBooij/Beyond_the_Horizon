using UnityEngine;

namespace Assets.Scripts.Game
{
    public class DifficultyManager : MonoBehaviour
    {
        public static DifficultyManager Instance;

        public float astroidSpeedMultiplier = 1f;
        public float astroidSpawnSpeedMultiplier = 2f;
        public float enemySpeedMultiplier = 1f;


        private void Awake()
        {
            Instance = this;
        }

        public void IncreaseDifficulty()
        {
            astroidSpeedMultiplier += 0.25f;
            astroidSpawnSpeedMultiplier += 0.2f;
            enemySpeedMultiplier += 0.1f;
        }
    }
}