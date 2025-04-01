using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class VipRewardSaveSystem
{
    static string saveKey = "VipRewardData";
    public static void SaveData(List<V_RewardSystem> rewardSystems)
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

    public static VipRewardSaveData LoadData()
    {
        if (!PlayerPrefs.HasKey(saveKey)) return new VipRewardSaveData();
        string json = PlayerPrefs.GetString(saveKey);
        return JsonConvert.DeserializeObject<VipRewardSaveData>(json);
    }
}

[System.Serializable]
public class VipRewardSaveData
{
    public Dictionary<int, List<bool>> dictRewardStates = new();
}
