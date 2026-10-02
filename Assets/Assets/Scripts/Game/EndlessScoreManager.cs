using TMPro;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class EndlessScoreManager : MonoBehaviour
    {
        public static EndlessScoreManager Instance;

        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI finalScoreText;

        private int score;

        public int CurrentScore => score;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            if (scoreText != null)
            {
                scoreText.text = "Score: " + score;
            }
        }

        public void AddPoints(int amount)
        {
            score += amount;

            if (scoreText != null)
            {
                scoreText.text = "Score: " + score;
            }

            if (finalScoreText != null)
            {
                finalScoreText.text = "Score: " + score;
            }
        }
    }
}