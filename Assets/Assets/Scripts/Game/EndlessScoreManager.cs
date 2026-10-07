using Assets.Scripts.SaveSystem;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class EndlessScoreManager : MonoBehaviour
    {
        public static EndlessScoreManager Instance;

        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI finalScoreText;
        public TextMeshProUGUI highScoreText;

        [Header("Difficulty")]
        [SerializeField] private int difficultyScoreStep = 1000;
        private int score;
        public int CurrentScore => score;
        private int nextDifficultyScore;

        private void Awake()
        {
            Instance = this;
            nextDifficultyScore = difficultyScoreStep;
        }

        private void Start()
        {
            if (scoreText != null) scoreText.text = "Score: " + score;
        }

        public void AddPoints(float amount)
        {
            score += (int)amount;
            if (scoreText != null) scoreText.text = "Score: " + score;
            
            while (score >= nextDifficultyScore)
            {
                if (DifficultyManager.Instance != null)
                    DifficultyManager.Instance.IncreaseDifficulty();

                nextDifficultyScore += difficultyScoreStep;
            }
        }

        public void HighScoreUpdate()
        {
            SaveManager.Instance.SaveEndlessHighScore(score);

            if (finalScoreText != null) finalScoreText.text = "Score: " + score;
            if (highScoreText != null)
                highScoreText.text = "High Score: " + SaveManager.Instance.GetEndlessHighScore();
        }
    }
}