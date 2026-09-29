using Assets.Scripts.SaveSystem;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance;
        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI finalScoreText;
        public TextMeshProUGUI victoryfinalScoreText;
        public TextMeshProUGUI highScore;
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

        // Update is called once per frame
        public void AddPoints(float amount)
        {
            _score += (int)amount;
            scoreText.text = "Score: " + _score.ToString();
            victoryfinalScoreText.text = "Final Score: " + _score.ToString();
            if (_score >= nextDifficultyScore)
            {
                DifficultyManager.Instance.IncreaseDifficulty();
                nextDifficultyScore += 1000;
            }
        }
        public void HighScoreUpdate()
        {
            SaveManager.Instance.SaveHighScore(_score);
            finalScoreText.text = "Final Score: " + _score;
            highScore.text = "High Score: " + SaveManager.Instance.GetHighScore();
            VictoryhighScore.text = "High Score: " + SaveManager.Instance.GetHighScore();
            
        }
    }
}