using System;
using System.IO;
using UnityEngine;

[System.Serializable]
public class PlayerSaveData
{
    public int currentLevel = 1;
    public string game3Ctypes = "topDown";
    public int maxArenaLevel = 1;
    public int maxArenaXP = 0;
}

public class JsonSave : MonoBehaviour
{
    public static JsonSave main;

    void Awake()
    {
        main = this;
    }

    public void ResetFilePlayerSaveData()
    {
        PlayerSaveData playerData = new PlayerSaveData
        {
            currentLevel = 1,
            game3Ctypes = "topDown",
            maxArenaLevel = 1,
            maxArenaXP = 0
        };

        SaveData(playerData, "playerData");
    }

    public static bool SaveData<T>(T data, string fileName)
    {
        try
        {
            string json = JsonUtility.ToJson(data, true);
            string path = GetSavePath(fileName);
            
            File.WriteAllText(path, json);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    // Loading data
    public static T LoadData<T>(string fileName) where T : new()
    {
        try
        {
            string path = GetSavePath(fileName);
            
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                return JsonUtility.FromJson<T>(json);
            }
            
            return new T();
        }
        catch (Exception e)
        {
            return new T();
        }
    }

    // Getting the path to save
    static string GetSavePath(string fileName)
    {
        if (!fileName.EndsWith(".json"))
        {
            fileName += ".json";
        }
        
        return Path.Combine(Application.persistentDataPath, fileName);
    }
}