using System;
using System.Collections.Generic;
using NUnit.Framework;

[Serializable]
public class SaveData {
    public string playerName = "Hero";
    public int highscore = 0;
    public int unlockedLevel = 1;
    public List inventory = new();
}