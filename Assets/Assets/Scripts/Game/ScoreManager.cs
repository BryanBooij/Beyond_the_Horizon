using TMPro;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance;
        public TextMeshProUGUI scoreText;
        private int _score;

        private void Awake()
        {
            Instance = this;
        }
        void Start()
        {
            scoreText.text = _score.ToString() + " POINTS";
        
        }

        // Update is called once per frame
        public void AddPoints()
        {
            _score += 1;
            scoreText.text = _score.ToString() +" POINTS";
        }
    }
}