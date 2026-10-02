using UnityEngine;

namespace Assets.Scripts.SaveSystem
{
    public class SaveManager : MonoBehaviour
    {
        public static SaveManager Instance;

        public void ResetAllProgress()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
        private void Awake()
        {
            Instance = this;
        }

        // Algemene highscore
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

        // Level highscore
        public void SaveLevelHighScore(string planet, int level, int score)
        {
            string key = planet + "_L" + level + "_Highscore";

            int oldScore = PlayerPrefs.GetInt(key, 0);

            if (score > oldScore)
            {
                PlayerPrefs.SetInt(key, score);
                PlayerPrefs.Save();
            }
        }

        public int GetLevelHighScore(string planet, int level)
        {
            string key = planet + "_L" + level + "_Highscore";

            return PlayerPrefs.GetInt(key, 0);
        }
        // Sterren

        public void SaveStars(string planet, int level, int stars)
        {
            string key = planet + "_L" + level + "_Stars";

            int oldStars = PlayerPrefs.GetInt(key, 0);

            if (stars > oldStars)
            {
                PlayerPrefs.SetInt(key, stars);
                PlayerPrefs.Save();
            }
        }

        public int GetStars(string planet, int level)
        {
            string key = planet + "_L" + level + "_Stars";

            return PlayerPrefs.GetInt(key, 0);
        }
        public void UnlockLevel(string planet, int level)
        {
            string key = planet + "_Unlocked";

            int currentUnlocked = PlayerPrefs.GetInt(key, 1);

            if (level > currentUnlocked)
            {
                PlayerPrefs.SetInt(key, level);
                PlayerPrefs.Save();
            }
        }

        public int GetUnlockedLevel(string planet)
        {
            string key = planet + "_Unlocked";

            return PlayerPrefs.GetInt(key, 1);
        }
        
        public void SaveEndlessHighScore(int score)
        {
            int highScore = PlayerPrefs.GetInt("EndlessHighScore", 0);

            if (score > highScore)
            {
                PlayerPrefs.SetInt("EndlessHighScore", score);
                PlayerPrefs.Save();
            }
        }

        public int GetEndlessHighScore()
        {
            return PlayerPrefs.GetInt("EndlessHighScore", 0);
        }
    }
}