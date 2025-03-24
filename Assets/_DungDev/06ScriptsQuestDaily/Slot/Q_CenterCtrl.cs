using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Q_CenterCtrl : MonoBehaviour
{
    [SerializeField] List<Q_MissionSlot> lsMissionSlots = new();


    [Button("Set Up ID")]
    void SetUp()
    {
        for (int i = 0; i < lsMissionSlots.Count; i++)
        {
            lsMissionSlots[i].SetIdMission(i);
        }
    }
}
