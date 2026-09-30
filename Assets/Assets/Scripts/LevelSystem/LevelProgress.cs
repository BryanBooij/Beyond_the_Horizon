using Assets.Scripts.SaveSystem;
using UnityEngine;

public static class LevelProgress
{
    static readonly string[] PlanetOrder = { "Nestara", "Barcune", "Verminia", "Sun" };
    public static bool IsPlanetCompleted(string planet)
    {
        return PlayerPrefs.GetInt(planet + "_Completed", 0) == 1;
    }

    public static bool IsPlanetUnlocked(string planet)
    {
        int index = System.Array.IndexOf(PlanetOrder, planet);
        if (index <= 0) return true; // first planet (or unknown name) is always open
        return IsPlanetCompleted(PlanetOrder[index - 1]);
    }
    public static void ResetAllProgress()
    {
        SaveManager.Instance.ResetAllProgress();
    }
    public static bool IsUnlocked(string planet, int level)
    {
        if (level <= 1)
            return true;

        return SaveManager.Instance.GetUnlockedLevel(planet) >= level;
    }
    
    public static int GetStars(string planet, int level)
    {
        return SaveManager.Instance.GetStars(planet, level);
    }

    public static int GetHighscore(string planet, int level)
    {
        return SaveManager.Instance.GetLevelHighScore(planet, level);
    }
    
    // in scene call LevelProgress.CompleteLevel("Nestara", 1); to update this
    public static void CompleteLevel(string planet, int level, int starsEarned, int score)
    {
        SaveManager.Instance.SaveStars(planet, level, starsEarned);

        SaveManager.Instance.SaveLevelHighScore(planet, level, score);

        if (level < 5)
        {
            SaveManager.Instance.UnlockLevel(planet, level + 1);
        }

        if (level == 5)
        {
            PlayerPrefs.SetInt(planet + "_Completed", 1);
        }
        Debug.Log($"[Complete] score={score} saved highscore now={LevelProgress.GetHighscore(planet, level)}");
        PlayerPrefs.Save();
    }
}