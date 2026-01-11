using System.IO;
using UnityEngine;

public class SaveService
{
    private static string Path => Application.persistentDataPath + "/shop.json";

    public static void SaveShop(ShopSaveData data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Path, json);
    }
    public static ShopSaveData Load()
    {
        if(!File.Exists(Path))
        {
            return new ShopSaveData();
        }
        string json = File.ReadAllText(Path);
        return JsonUtility.FromJson<ShopSaveData>(json);
    }
}
