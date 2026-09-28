using UnityEngine;

public static class LevelProgress
{
    public static bool IsUnlocked(string planet, int level)
    {
        // level 1 is always unlocked
        if (level <= 1) return true;
        return PlayerPrefs.GetInt(planet + "_Unlocked", 1) >= level;
    }
    
    // in scene call LevelProgress.CompleteLevel("Nestara", 1); to update this
    public static void CompleteLevel(string planet, int level)
    {
        int unlocked = PlayerPrefs.GetInt(planet + "_Unlocked", 1);

        if (level < 5 && unlocked < level + 1)
        {
            PlayerPrefs.SetInt(planet + "_Unlocked", level + 1);
            PlayerPrefs.Save();
        }
    }
}