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
}


[CreateAssetMenu(menuName = "USER/DataDailyQuest")]

public class DataDailyQuest : ScriptableObject
{
    [SerializeField] int totalRewardAmount;
    public int TotalRewardAmount => totalRewardAmount;
    [SerializeField] int currentTotalRewardAmount;
    public int CurrentTotalRewardAmount => currentTotalRewardAmount;

    public List<DailyQuest> lsDailyQuests = new();

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
    }


    #region Odin
    [Button("Rest Daily Quest & Set Up Quest")]
    void SetUpQuest()
    {
        for (int i = 0; i < lsDailyQuests.Count; i++)
        {
            lsDailyQuests[i].iDQuest = i;
            lsDailyQuests[i].currentProgess = 0;
            lsDailyQuests[i].amountReward = i * 5;
            lsDailyQuests[i].isClaimed = false;
            switch (i)
            {
                case 0:
                    lsDailyQuests[i].questType = QuestType.DailyLogin;
                    lsDailyQuests[i].questName = "Log in Daily";
                    lsDailyQuests[i].currentProgess = 1;
                    lsDailyQuests[i].amountReward = 5;
                    lsDailyQuests[i].requiredProgess = 1; // Đăng nhập 1 lần mỗi ngày
                    break;
                case 1:
                    lsDailyQuests[i].questType = QuestType.SpinWheel;
                    lsDailyQuests[i].questName = "Spin the Lucky Wheel 3 Times";
                    lsDailyQuests[i].requiredProgess = 3; // Cần quay 3 lần
                    break;
                case 2:
                    lsDailyQuests[i].questType = QuestType.UpgradeUnit;
                    lsDailyQuests[i].questName = "Upgrade a Unit 5 Times";
                    lsDailyQuests[i].requiredProgess = 5; // Cần nâng cấp 5 lần
                    break;
                case 3:
                    lsDailyQuests[i].questType = QuestType.EvolveUnit;
                    lsDailyQuests[i].questName = "Evolve a Unit Once";
                    lsDailyQuests[i].requiredProgess = 1; // Cần tiến hóa 1 lần
                    break;
                case 4:
                    lsDailyQuests[i].questType = QuestType.SummonMulti;
                    lsDailyQuests[i].questName = "Perform One 10x Summon";
                    lsDailyQuests[i].requiredProgess = 1; // Cần triệu hồi x10 một lần
                    break;
                case 5:
                    lsDailyQuests[i].questType = QuestType.SummonSingle;
                    lsDailyQuests[i].questName = "Perform 10 Single Summons";
                    lsDailyQuests[i].requiredProgess = 10; // Cần triệu hồi 10 lần x1
                    break;
                default:
                    lsDailyQuests[i].questType = QuestType.UpgradeUnit;
                    lsDailyQuests[i].questName = "Upgrade a Unit 5 Times";
                    lsDailyQuests[i].requiredProgess = 5;
                    break;
            }
        }
        this.totalRewardAmount = 0;
        this.currentTotalRewardAmount = 0;
        foreach (var child in this.lsDailyQuests) this.totalRewardAmount += child.amountReward;
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
    }
    public bool IsCompleted()
    {
        return currentProgess >= requiredProgess;
    }
}
