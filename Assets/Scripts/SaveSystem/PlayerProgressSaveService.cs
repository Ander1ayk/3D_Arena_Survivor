using System.IO;
using UnityEngine;

public static class PlayerProgressSaveService
{
   private static string Path => Application.persistentDataPath + "/playerProgress.json";

    public static void SavePlayerProgress(PlayerProgressData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Path, json);
    }
    public static PlayerProgressData LoadPlayerProgress()
    {
        if (File.Exists(Path))
        {
            string json = File.ReadAllText(Path);
            return JsonUtility.FromJson<PlayerProgressData>(json);
        }
        return null;
    }
    public static void DeletePlayerProgress()
    {
        if (File.Exists(Path))
        {
            File.Delete(Path);
        }
    }
}
