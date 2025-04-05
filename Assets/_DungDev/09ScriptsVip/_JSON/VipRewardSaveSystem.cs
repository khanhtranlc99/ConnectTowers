using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class VipRewardSaveSystem
{
    static string SAVE_KEY_VIP = "SAVE_KEY_VIP";

    static string GetFilePath(string filePath)
    {
        return Path.Combine(Application.persistentDataPath,filePath);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="rewardSystems"></param>
    /// 


    public static void SaveDataReward(List<V_RewardSystem> rewardSystems)
    {
        VipRewardSaveData saveData = new();

        foreach(var rewardSystem in rewardSystems)
        {
            List<bool> claimStates = new();
            foreach(var category in rewardSystem.LsRewardCategorys)
            {
                claimStates.Add(category.isClaim);
            }

            saveData.dictRewardStates[rewardSystem.LevelVip] = claimStates;
        }

        string json = JsonConvert.SerializeObject(saveData,Formatting.Indented);
        File.WriteAllText(GetFilePath(SAVE_KEY_VIP), json);
    }

    public static VipRewardSaveData GetDataReward()
    {
        var filePath = GetFilePath(SAVE_KEY_VIP);
        if (!File.Exists(filePath)) return new VipRewardSaveData();
        string json = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<VipRewardSaveData>(json);
    }

    ///
    ///
    static string SAVE_KEY_FREEVIP = "SAVE_KEY_FREEVIP";

    public static void SaveDataRewardDaily(List<V_RewardDailySystem> rewardDailySystems)
    {
        VipRewardDailySaveData saveData = new();
        foreach (var rewardDaily in rewardDailySystems)
        {
            saveData.lsRewardDailyStates.Add(rewardDaily.isCollected);
        }
        Debug.LogError("REward Daily COmpleeteee");
        string json = JsonConvert.SerializeObject(saveData, Formatting.Indented);
        File.WriteAllText(GetFilePath(SAVE_KEY_FREEVIP), json);
    }

    public static VipRewardDailySaveData GetDataRewardDaily()
    {
        var filePath = GetFilePath(SAVE_KEY_FREEVIP);
        if (!File.Exists(filePath)) return new VipRewardDailySaveData();
        string json = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<VipRewardDailySaveData>(json);
    }


}

[System.Serializable]
public class VipRewardSaveData
{
    public Dictionary<int, List<bool>> dictRewardStates = new();
}

[System.Serializable]
public class VipRewardDailySaveData
{
    public List<bool> lsRewardDailyStates = new();
}
