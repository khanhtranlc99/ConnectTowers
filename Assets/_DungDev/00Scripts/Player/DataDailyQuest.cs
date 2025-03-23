using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestType
{
    CollectCoins,
    WatchAds,
    SpinWheel,
    SummonCard,
    UpgradeUnit,
    EvolutionUnit,
    BuyShop,
}
[CreateAssetMenu(menuName = "USER/DataDailyQuest")]

public class DataDailyQuest : ScriptableObject
{
    public List<DailyQuest> lsDailyQuests = new();

    public DailyQuest GetQuestByID(int questID)
    {
        foreach(var child in this.lsDailyQuests)
        {
            if (child.questInfo.iDQuest == questID) return child;
        }
        return null;
    }

}
[System.Serializable]
public class DailyQuest
{
    public QuestInfo questInfo;
    public int currentProgess;
    public int requiredProgess;
    public bool isCompleted;

    public void SetCurrentProgess(int amount)
    {
        this.currentProgess += amount;
    }
}
[System.Serializable]
public class QuestInfo
{
    public int iDQuest;
    public QuestType questType;
    public string questName;
}
