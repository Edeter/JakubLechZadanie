using System.Collections.Generic;
using UnityEngine;

public static class SaveDataManager
{
    public static void SaveJsonScore(int score)
    {
        SaveData sd = new SaveData();
        sd.m_Score = score;

        if (FileManager.WriteToFile("SaveData", sd.ToJson()))
        {
            Debug.Log("Save successful");
        }
    }

    public static void LoadJsonScore(out int score)
    {
        if (FileManager.LoadFromFile("SaveData", out var json))
        {
            SaveData sd = new SaveData();
            sd.LoadFromJson(json);

            score = sd.m_Score;

            Debug.Log("Load complete");
        }
        else
        {
            score = 0;
        }
    }
}