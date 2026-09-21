using TMPro;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance;
        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI finalScoreText;
        private int _score;
        private int nextDifficultyScore = 500;

        private void Awake()
        {
            Instance = this;
        }
        void Start()
        {
            scoreText.text = "Score: " + _score.ToString();
        
        }

        // Update is called once per frame
        public void AddPoints(float amount)
        {
            _score += (int)amount;
            scoreText.text = "Score: " + _score.ToString();
            finalScoreText.text = "Final Score: " + _score.ToString();
            
            if (_score >= nextDifficultyScore)
            {
                DifficultyManager.Instance.IncreaseDifficulty();
                nextDifficultyScore += 500;
            }
        }
    }
}