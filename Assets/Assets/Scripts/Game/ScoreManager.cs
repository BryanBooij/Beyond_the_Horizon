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
        public GameObject victoryscreen;
        private int _score;

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
            finalScoreText.text = "Final Score: " + _score.ToString();
            victoryfinalScoreText.text = "Final Score: " + _score.ToString();
            
            if (_score >= 1000 && victoryscreen != null)
            {
                if (victoryscreen != null)
                {
                    victoryscreen.SetActive(true);
                }
                Time.timeScale = 0f;
            }
        }
    }
}