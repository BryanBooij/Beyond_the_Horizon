using TMPro;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance;

        [Header("Score UI")]
        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI finalScoreText;
        public TextMeshProUGUI victoryfinalScoreText;
        public TextMeshProUGUI highScoreText;

        public GameObject victoryscreen;

        private int _score;
        private int _highScore;

        private int nextDifficultyScore = 1000;

        private const string HighScoreKey = "HighScore";

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            // Highscore laden
            _highScore = PlayerPrefs.GetInt(HighScoreKey, 0);

            scoreText.text = "Score: " + _score;

            if (highScoreText != null)
            {
                highScoreText.text = "High Score: " + _highScore;
            }

            if (victoryscreen != null)
            {
                victoryscreen.SetActive(false);
            }
        }

        public void AddPoints(float amount)
        {
            _score += (int)amount;

            scoreText.text = "Score: " + _score;

            if (finalScoreText != null)
            {
                finalScoreText.text = "Final Score: " + _score;
            }

            if (victoryfinalScoreText != null)
            {
                victoryfinalScoreText.text = "Final Score: " + _score;
            }

            if (_score >= nextDifficultyScore)
            {
                DifficultyManager.Instance.IncreaseDifficulty();
                nextDifficultyScore += 1000;
            }
        }

        public void ShowVictoryScore()
        {
            if (victoryfinalScoreText != null)
            {
                victoryfinalScoreText.text = "Final Score: " + _score;
            }

            SaveHighScore();
        }

        private void SaveHighScore()
        {
            if (_score > _highScore)
            {
                _highScore = _score;

                PlayerPrefs.SetInt(HighScoreKey, _highScore);
                PlayerPrefs.Save();

                Debug.Log("New High Score: " + _highScore);
            }
        }

        public int GetScore()
        {
            return _score;
        }

        public int GetHighScore()
        {
            return _highScore;
        }
    }
}