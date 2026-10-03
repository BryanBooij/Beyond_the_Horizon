using Assets.Scripts.SaveSystem;
using Assets.Scripts.LevelSystem;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance;

        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI GameoverFinalscore;
        public TextMeshProUGUI victoryfinalScoreText;
        public TextMeshProUGUI GameOverHighScore;
        public TextMeshProUGUI VictoryhighScore;
        public GameObject victoryscreen;

        private int _score;
        public int CurrentScore => _score;

        private int nextDifficultyScore = 1000;

        private void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            scoreText.text = "Score: " + _score.ToString();

            if (victoryscreen != null)
            {
                victoryscreen.SetActive(false);
            }
        }

        public void AddPoints(float amount)
        {
            _score += (int)amount;

            scoreText.text = "Score: " + _score.ToString();
            victoryfinalScoreText.text = "Score: " + _score.ToString();
            GameoverFinalscore.text = "Score: " + _score.ToString();

            if (_score >= nextDifficultyScore)
            {
                DifficultyManager.Instance.IncreaseDifficulty();
                nextDifficultyScore += 1000;
            }
        }

        // Algemene highscore - NIETS AAN VERANDERD
        public void HighScoreUpdate()
        {
            SaveManager.Instance.SaveHighScore(_score);

            GameOverHighScore.text = "High Score: " + SaveManager.Instance.GetHighScore();
            VictoryhighScore.text = "High Score: " + SaveManager.Instance.GetHighScore();
        }

        // Highscore van het huidige level
        public void LevelHighScoreUpdate()
        {
            SaveManager.Instance.SaveLevelHighScore(
                CurrentLevel.planet,
                CurrentLevel.level,
                _score
            );

            int levelHighScore = SaveManager.Instance.GetLevelHighScore(
                CurrentLevel.planet,
                CurrentLevel.level
            );

            GameOverHighScore.text = "High Score: " + levelHighScore;
            VictoryhighScore.text = "High Score: " + levelHighScore;
        }
    }
}