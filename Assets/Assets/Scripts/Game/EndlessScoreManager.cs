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

        private int score;
        public int CurrentScore => score;

        private void Awake() => Instance = this;

        private void Start()
        {
            if (scoreText != null) scoreText.text = "Score: " + score;
        }

        public void AddPoints(float amount)
        {
            score += (int)amount;
            if (scoreText != null) scoreText.text = "Score: " + score;
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