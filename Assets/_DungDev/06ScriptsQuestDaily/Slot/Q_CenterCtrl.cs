using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.SocialPlatforms.Impl;

public class Q_CenterCtrl : MonoBehaviour
{
    [SerializeField] List<Q_MissionSlot> lsMissionSlots = new();

    private void OnEnable()
    {
        System.DateTime oldTime = UseProfile.FirstTimeOpenGame;
        if (TimeManager.IsPassTheDay(oldTime, System.DateTime.Now))
        {
            Debug.LogError("New Day");
            Debug.LogError(oldTime);
            UseProfile.FirstTimeOpenGame = System.DateTime.Now;
        }
        Debug.LogError(oldTime.AddDays(1));

        var dataQuest = GameController.Instance.dataContain.dataUser.DataDailyQuest;
        for (int i = 0; i < lsMissionSlots.Count; i++)
        {
            lsMissionSlots[i].Init(dataQuest);
            lsMissionSlots[i].questType = dataQuest.lsDailyQuests[i].questType;
        }
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
