using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


public enum QuestType
{
    SpinWheel,
    UpgradeUnit,
    EvolveUnit,
    SummonSingle,
    SummonMulti,
    DailyLogin,
    DefeatEnemies,
    RerollShop,
    WatchAd1Time,
    WatchAd10Times,
}


[CreateAssetMenu(menuName = "USER/DataDailyQuest")]

public class DataDailyQuest : ScriptableObject
{
    [SerializeField] int totalRewardAmount;
    public int TotalRewardAmount => totalRewardAmount;
    [SerializeField] int currentTotalRewardAmount;
    public int CurrentTotalRewardAmount => currentTotalRewardAmount;

    public List<DailyQuest> lsDailyQuests = new();
    public List<bool> lsDailyTracker = new();

    #region json
    public void LoadQuestData()
    {
        QuestDailySaveData questDailySaveData = QuestDailySave_Json.GetQuestDailySaveData();

        for(int i = 0; i < this.lsDailyQuests.Count; i++)
        {
            this.lsDailyQuests[i].isClaimed = questDailySaveData.lsDataDailyQuest[i].isClaimed;
            this.lsDailyQuests[i].currentProgess = questDailySaveData.lsDataDailyQuest[i].currentProgress;
        }
        this.currentTotalRewardAmount = questDailySaveData.currentTotalRewardAmount;
    }


    public void LoadQuestTracker()
    {
        QuestDailySaveData questDailySaveData = QuestDailySave_Json.GetQuestDailyTracker();
        for(int i = 0; i < this.lsDailyTracker.Count; i++)
        {
            this.lsDailyTracker[i] = questDailySaveData.lsDataTopTrackers[i];
        }
    }

    #endregion

    public DailyQuest GetQuestByID(int questID)
    {
        foreach(var child in this.lsDailyQuests)
        {
            if (child.iDQuest == questID) return child;
        }
        return null;
    }

    DailyQuest GetQuestByType(QuestType questType)
    {
        foreach (var child in this.lsDailyQuests) if (child.questType == questType) return child;
        return null;
    }
    public void IncreaseQuestProgress(QuestType type, int amount)
    {
        DailyQuest quest = GetQuestByType(type);
        if (quest != null)
        {
            quest.SetCurrentProgess(amount);
        }
    }

    public void SetCurentTotalReward(int total)
    {
        this.currentTotalRewardAmount += total;

    }

    //Method reset qua ngay moi
    public void ResetDailyQuest()
    {

        this.currentTotalRewardAmount = 0;
        foreach (var child in this.lsDailyQuests)
        {
            child.currentProgess = 0;
            child.isClaimed = false;
            if (child.questType == QuestType.DailyLogin)
            {
                child.currentProgess = 1;
            }
        }
        QuestDailySaveData questDailySaveData = QuestDailySave_Json.GetQuestDailySaveData();
        for (int i = 0; i < questDailySaveData.lsDataTopTrackers.Count; i++) questDailySaveData.lsDataTopTrackers[i] = false;
        QuestDailySave_Json.SaveDataQuestDaily(this);
        QuestDailySave_Json.SaveDataQuestTopTracker(this);
        
    }


    #region Odin
    [Button("Rest Daily Quest & Set Up Quest", ButtonSizes.Large), GUIColor(0.122f, 1f, 0.514f)]
    void SetUpQuest()
    {
        for (int i = 0; i < lsDailyQuests.Count; i++)
        {
            lsDailyQuests[i].iDQuest = i;
            lsDailyQuests[i].currentProgess = 0;
            lsDailyQuests[i].isClaimed = false;

            switch (i)
            {
                case 0:
                    lsDailyQuests[i].questType = QuestType.DailyLogin;
                    lsDailyQuests[i].questName = "Log in Daily";
                    lsDailyQuests[i].currentProgess = 1;
                    lsDailyQuests[i].requiredProgess = 1;
                    lsDailyQuests[i].amountReward = 5;  // Nhiệm vụ dễ, thưởng thấp
                    break;
                case 1:
                    lsDailyQuests[i].questType = QuestType.SpinWheel;
                    lsDailyQuests[i].questName = "Spin the Lucky Wheel 3 Times";
                    lsDailyQuests[i].requiredProgess = 3;
                    lsDailyQuests[i].amountReward = 10; // Dễ, thưởng trung bình
                    break;
                case 2:
                    lsDailyQuests[i].questType = QuestType.UpgradeUnit;
                    lsDailyQuests[i].questName = "Upgrade a Unit 5 Times";
                    lsDailyQuests[i].requiredProgess = 5;
                    lsDailyQuests[i].amountReward = 15; // Trung bình
                    break;
                case 3:
                    lsDailyQuests[i].questType = QuestType.EvolveUnit;
                    lsDailyQuests[i].questName = "Evolve a Unit Once";
                    lsDailyQuests[i].requiredProgess = 1;
                    lsDailyQuests[i].amountReward = 25; // Khó, thưởng cao
                    break;
                case 4:
                    lsDailyQuests[i].questType = QuestType.SummonMulti;
                    lsDailyQuests[i].questName = "Perform One 10x Summon";
                    lsDailyQuests[i].requiredProgess = 1;
                    lsDailyQuests[i].amountReward = 20; // Triệu hồi x10 khá tốn, thưởng cao
                    break;
                case 5:
                    lsDailyQuests[i].questType = QuestType.SummonSingle;
                    lsDailyQuests[i].questName = "Perform 10 Single Summons";
                    lsDailyQuests[i].requiredProgess = 10;
                    lsDailyQuests[i].amountReward = 15; // Trung bình
                    break;
                case 6:
                    lsDailyQuests[i].questType = QuestType.DefeatEnemies;
                    lsDailyQuests[i].questName = "Defeat 5 Enemies";
                    lsDailyQuests[i].requiredProgess = 5;
                    lsDailyQuests[i].amountReward = 20; // Tiêu diệt kẻ địch, thưởng cao
                    break;
                case 7:
                    lsDailyQuests[i].questType = QuestType.RerollShop;
                    lsDailyQuests[i].questName = "Reroll the Shop 5 Times";
                    lsDailyQuests[i].requiredProgess = 5;
                    lsDailyQuests[i].amountReward = 10; // Dễ, thưởng trung bình
                    break;
                case 8:
                    lsDailyQuests[i].questType = QuestType.WatchAd1Time;
                    lsDailyQuests[i].questName = "Watch 1 Advertisement";
                    lsDailyQuests[i].requiredProgess = 1;
                    lsDailyQuests[i].amountReward = 5; // Nhiệm vụ dễ nhất, thưởng ít
                    break;
                case 9:
                    lsDailyQuests[i].questType = QuestType.WatchAd10Times;
                    lsDailyQuests[i].questName = "Watch 10 Advertisements";
                    lsDailyQuests[i].requiredProgess = 10;
                    lsDailyQuests[i].amountReward = 30; // Khó, thưởng cao
                    break;
                default:
                    lsDailyQuests[i].questType = QuestType.UpgradeUnit;
                    lsDailyQuests[i].questName = "Upgrade a Unit 5 Times";
                    lsDailyQuests[i].requiredProgess = 5;
                    lsDailyQuests[i].amountReward = 15;
                    break;
            }
        }
        this.totalRewardAmount = 0;
        this.currentTotalRewardAmount = 0;
        foreach (var child in this.lsDailyQuests) this.totalRewardAmount += child.amountReward;
        for (int i = 0; i < this.lsDailyTracker.Count; i++) this.lsDailyTracker[i] = false;
    }

    [Button("Json SetUp", ButtonSizes.Large)]
    void SetUpJson()
    {
        QuestDailySave_Json.SaveDataQuestDaily(this);


        QuestDailySave_Json.SaveDataQuestTopTracker(this);
    }
    #endregion
}
[System.Serializable]
public class DailyQuest
{
    public int iDQuest;
    public QuestType questType;
    public string questName;
    public int currentProgess;
    public int requiredProgess;
    public int amountReward;
    public bool isClaimed = false;
    public void SetCurrentProgess(int amount)
    {
        this.currentProgess += amount;
        QuestDailySave_Json.SaveDataQuestDaily(GameController.Instance.dataContain.dataUser.DataDailyQuest);
    }
    public bool IsCompleted()
    {
        return currentProgess >= requiredProgess;
    }
}
