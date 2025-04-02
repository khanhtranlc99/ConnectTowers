using EventDispatcher;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class Q_QuestBoxCtrl : MonoBehaviour
{
    [Header("Top - Daily")]
    [SerializeField] Q_TopCtrl topCtrl;
    public Q_TopCtrl TopCtrl => topCtrl;
    [Header("Center - Mission")]
    [SerializeField] List<Q_MissionSlot> lsMissionSlots = new();
    public List<Q_MissionSlot> LsMissionSlots => lsMissionSlots;
    public void Init()
    {
        var dataUser = GameController.Instance.dataContain.dataUser;
        dataUser.ResetDailyDay();
        dataUser.DataDailyQuest.LoadQuestData();
        dataUser.DataDailyQuest.LoadQuestTracker();

        for (int i = 0; i < lsMissionSlots.Count; i++)
        {
            lsMissionSlots[i].Init();
            lsMissionSlots[i].questType = dataUser.DataDailyQuest.lsDailyQuests[i].questType;
        }
        this.RegisterListener(EventID.UPDATE_PROGESSBAR_QUEST, topCtrl.UpdateUI);

        this.topCtrl.Init();

    }

    private void OnDestroy()
    {
        this.RemoveListener(EventID.UPDATE_PROGESSBAR_QUEST, topCtrl.UpdateUI);
    }
    [Button("Set Up")]
    void SetUp()
    {
        for (int i = 0; i < lsMissionSlots.Count; i++)
        {
            lsMissionSlots[i].SetIdMission(i);
        }
    }

}
