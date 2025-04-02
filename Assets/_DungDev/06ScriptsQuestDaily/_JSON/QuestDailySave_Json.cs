using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class QuestDailySave_Json
{
    static string QUEST_DAILY_SAVE = "QUEST_DAILY_SAVE";
    static string QUEST_TOP_TRACKER = "QUEST_TOP_TRACKER";
    public static void SaveDataQuestDaily(DataDailyQuest dataDailyQuest)
    {   
        QuestDailySaveData questDailySaveData = new QuestDailySaveData();

        foreach(var dailyQuest in dataDailyQuest.lsDailyQuests)
        {
            DataQuestDaily dataQuestDaily = new DataQuestDaily(dailyQuest.iDQuest,
                dailyQuest.isClaimed, dailyQuest.currentProgess);
            questDailySaveData.lsDataDailyQuest.Add(dataQuestDaily);
        }
        questDailySaveData.currentTotalRewardAmount = dataDailyQuest.CurrentTotalRewardAmount;

        string json = JsonConvert.SerializeObject(questDailySaveData);
        PlayerPrefs.SetString(QUEST_DAILY_SAVE, json);
        PlayerPrefs.Save();
    }

    public static void SaveDataQuestTopTracker(DataDailyQuest dataDailyQuest)
    {
        QuestDailySaveData questDailySaveData = new QuestDailySaveData();
        for (int i = 0; i < dataDailyQuest.lsDailyTracker.Count; i++)
            questDailySaveData.lsDataTopTrackers.Add(dataDailyQuest.lsDailyTracker[i]);

        string json = JsonConvert.SerializeObject(questDailySaveData);
        PlayerPrefs.SetString(QUEST_TOP_TRACKER, json);
        PlayerPrefs.Save();
    }

    public static QuestDailySaveData GetQuestDailyTracker()
    {
        if (!PlayerPrefs.HasKey(QUEST_TOP_TRACKER)) return new QuestDailySaveData();
        string json = PlayerPrefs.GetString(QUEST_TOP_TRACKER);
        return JsonConvert.DeserializeObject<QuestDailySaveData>(json);
    }

    public static QuestDailySaveData GetQuestDailySaveData()
    {
        if (!PlayerPrefs.HasKey(QUEST_DAILY_SAVE)) return new QuestDailySaveData();
        string json = PlayerPrefs.GetString(QUEST_DAILY_SAVE);
        return JsonConvert.DeserializeObject<QuestDailySaveData>(json);

    }


}
[System.Serializable]
public class QuestDailySaveData
{
    public List<DataQuestDaily> lsDataDailyQuest = new();
    // thang nay dieu kien thanh control tren quest
    public List<bool> lsDataTopTrackers = new();
    public int currentTotalRewardAmount;

}

[System.Serializable]
public class DataQuestDaily
{
    public int idQuest;
    public bool isClaimed;
    public int currentProgress;

    public DataQuestDaily(int idQuest, bool isClaimed, int currentProgress)
    {
        this.idQuest = idQuest;
        this.isClaimed = isClaimed;
        this.currentProgress = currentProgress;
    }
}
