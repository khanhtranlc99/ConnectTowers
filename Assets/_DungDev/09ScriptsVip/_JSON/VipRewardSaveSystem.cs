using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class VipRewardSaveSystem
{
    static string saveKey = "VipRewardData";
    /// <summary>
    /// 
    /// </summary>
    /// <param name="rewardSystems"></param>
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

        string json = JsonConvert.SerializeObject(saveData);
        PlayerPrefs.SetString(saveKey, json);
        PlayerPrefs.Save();
    }

    public static VipRewardSaveData GetDataReward()
    {
        if (!PlayerPrefs.HasKey(saveKey)) return new VipRewardSaveData();
        string json = PlayerPrefs.GetString(saveKey);
        return JsonConvert.DeserializeObject<VipRewardSaveData>(json);
    }

    ///
    ///
    static string saveKeyDaily = "VipRewardDailyData";

    public static void SaveDataRewardDaily(List<V_RewardDailySystem> rewardDailySystems)
    {
        VipRewardDailySaveData saveData = new();
        foreach (var rewardDaily in rewardDailySystems)
        {
            saveData.lsRewardDailyStates.Add(rewardDaily.isCollected);
        }

        string json = JsonConvert.SerializeObject(saveData);
        PlayerPrefs.SetString(saveKeyDaily, json);
        PlayerPrefs.Save();
    }

    public static VipRewardDailySaveData GetDataRewardDaily()
    {
        if (!PlayerPrefs.HasKey(saveKeyDaily)) return new VipRewardDailySaveData();
        string json = PlayerPrefs.GetString(saveKeyDaily);
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
