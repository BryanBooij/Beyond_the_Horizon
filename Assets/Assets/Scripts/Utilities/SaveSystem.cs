using System.IO;
using UnityEngine;

public static class SaveSystem {
    private static string FilePath =>
        Path.Combine(Application.persistentDataPath, "savedata.json");

    public static void Save(SaveData data) {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(FilePath, json);
        Debug.Log($"Saved successfully to: {FilePath}");
    }

    public static SaveData Load() {
        if (!File.Exists(FilePath)) {
            Debug.LogWarning("Save file not found. Returning fresh data.");
            return new SaveData();
        }

        string json = File.ReadAllText(FilePath);
        return JsonUtility.FromJson<SaveData>(json);
    }
}