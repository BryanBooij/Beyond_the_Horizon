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

        // =========================
        // ALGEMENE HIGHSCORE
        // =========================

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


        // =========================
        // LEVEL HIGHSCORE
        // =========================

        public void SaveLevelHighScore(
            string planet,
            int level,
            int score)
        {
            string key = planet + "_L" + level + "_Highscore";

            int oldScore = PlayerPrefs.GetInt(key, 0);

            if (score > oldScore)
            {
                PlayerPrefs.SetInt(key, score);
                PlayerPrefs.Save();
            }
        }

        public int GetLevelHighScore(
            string planet,
            int level)
        {
            string key = planet + "_L" + level + "_Highscore";

            return PlayerPrefs.GetInt(key, 0);
        }


        // =========================
        // STERREN
        // =========================

        public void SaveStars(
            string planet,
            int level,
            int stars)
        {
            string key = planet + "_L" + level + "_Stars";

            int oldStars = PlayerPrefs.GetInt(key, 0);

            if (stars > oldStars)
            {
                PlayerPrefs.SetInt(key, stars);
                PlayerPrefs.Save();
            }
        }

        public int GetStars(
            string planet,
            int level)
        {
            string key = planet + "_L" + level + "_Stars";

            return PlayerPrefs.GetInt(key, 0);
        }


        // =========================
        // LEVEL UNLOCKS
        // =========================

        public bool IsLevelUnlocked(
            string planet,
            int level)
        {
            if (level <= 1)
                return true;

            int unlockedLevel =
                PlayerPrefs.GetInt(
                    planet + "_Unlocked",
                    1
                );

            return unlockedLevel >= level;
        }

        public void UnlockNextLevel(
            string planet,
            int completedLevel)
        {
            int unlockedLevel =
                PlayerPrefs.GetInt(
                    planet + "_Unlocked",
                    1
                );

            if (completedLevel < 5 &&
                unlockedLevel < completedLevel + 1)
            {
                PlayerPrefs.SetInt(
                    planet + "_Unlocked",
                    completedLevel + 1
                );

                PlayerPrefs.Save();
            }
        }
    }
}