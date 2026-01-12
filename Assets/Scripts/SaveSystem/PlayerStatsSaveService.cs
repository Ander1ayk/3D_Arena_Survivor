using UnityEngine;

public static class PlayerStatsSaveService
{
   private static string Path = Application.persistentDataPath + "/playerStats.save";
    public static void SavePlayerStats(PlayerStatsSaveData data)
    {
        string json = JsonUtility.ToJson(data);
        System.IO.File.WriteAllText(Path, json);
    }
    public static PlayerStatsSaveData LoadPlayerStats()
    {
        if (System.IO.File.Exists(Path))
        {
            string json = System.IO.File.ReadAllText(Path);
            return JsonUtility.FromJson<PlayerStatsSaveData>(json);
        }
        return null;
    }
    public static void DeletePlayerStats()
    {
        if (System.IO.File.Exists(Path))
        {
            System.IO.File.Delete(Path);
        }
    }
}
