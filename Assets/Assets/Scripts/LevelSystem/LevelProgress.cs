using UnityEngine;

public static class LevelProgress
{
    public static void ResetAllProgress()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
    public static bool IsUnlocked(string planet, int level)
    {
        // level 1 is always unlocked
        if (level <= 1) return true;
        return PlayerPrefs.GetInt(planet + "_Unlocked", 1) >= level;
    }
    
    public static int GetStars(string planet, int level)
    {
        return PlayerPrefs.GetInt($"{planet}_L{level}_Stars", 0);
    }

    public static int GetHighscore(string planet, int level)
    {
        return PlayerPrefs.GetInt($"{planet}_L{level}_Highscore", 0);
    }
    
    // in scene call LevelProgress.CompleteLevel("Nestara", 1); to update this
    public static void CompleteLevel(string planet, int level, int starsEarned, int score)
    {
        if (starsEarned > GetStars(planet, level))
        {
            PlayerPrefs.SetInt($"{planet}_L{level}_Stars", starsEarned);
        }

        if (score > GetHighscore(planet, level))
        {
            PlayerPrefs.SetInt($"{planet}_L{level}_Highscore", score);
        }

        int unlocked = PlayerPrefs.GetInt(planet + "_Unlocked", 1);

        if (level < 5 && unlocked < level + 1)
        {
            PlayerPrefs.SetInt(planet + "_Unlocked", level + 1);
        }
        Debug.Log($"[Complete] score={score} saved highscore now={LevelProgress.GetHighscore(planet, level)}");
        PlayerPrefs.Save();
    }
}