using UnityEngine;

namespace Assets.Scripts.SaveSystem
{
    public class SaveManager : MonoBehaviour
    {
        public static SaveManager Instance;

        private void Awake()
        {
            Instance = this;
        }

        public void SaveHighScore(int score)
        {
            int highScore = PlayerPrefs.GetInt("SavedHighScore", 0);

            if (score > highScore)
            {
                PlayerPrefs.SetInt("SavedHighScore", score);
                PlayerPrefs.Save();
            }
        }

        public int GetHighScore()
        {
            return PlayerPrefs.GetInt("SavedHighScore", 0);
        }
    }
}