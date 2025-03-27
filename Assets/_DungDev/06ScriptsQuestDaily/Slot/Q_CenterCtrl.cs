using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Q_CenterCtrl : MonoBehaviour
{
    [SerializeField] List<Q_MissionSlot> lsMissionSlots = new();

    private void OnEnable()
    {
        var dataQuest = GameController.Instance.dataContain.dataUser.DataDailyQuest;
        for (int i = 0; i < lsMissionSlots.Count; i++)
        {
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
