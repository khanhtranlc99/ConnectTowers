using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "AIData", menuName = "Quoc_Dev/AIData", order =1)]
public class AIData : SingletonScriptableObject<AIData>
{
    [TableList]
    public List<HardRateData> AIConfigList = new List<HardRateData>();

    public HardRateData GetRankAI(int id)
    {
        foreach(var item in AIConfigList)
        {
            if(item.rankId == id)
            {
                return item;
            }
        }
        return AIConfigList[AIConfigList.Count - 1];
    }
    private void OnValidate()
    {
        int idx = 1;
        foreach(var item in AIConfigList)
        {
            item.rankId = idx;
            AI_Behaviour[] enumValues = (AI_Behaviour[])Enum.GetValues(typeof(AI_Behaviour));
            int i = 0;
            foreach(AI_Behaviour value in enumValues)
            {
                while (item.configList.Count < i + 1)
                {
                    item.configList.Add(new AI_Config());
                }
                item.configList[i].name = value;
                i++;
            }
            item.rankId = idx++;
        }
    }
}

[System.Serializable]   
public class HardRateData
{
    [TableColumnWidth(200, false)]
    [VerticalGroup("Main")]
    [Min(0)]
    public int rankId; // cấp độ của AI
    [VerticalGroup("Main")]
    [Min(0.5f)]
    public float interavalMin; // thời gian tối thiểu của hành động
    [VerticalGroup("Main")]
    [Min(0.5f)]
    public float interavalMax; // thời gian tối đa của hành động
    [VerticalGroup("Main")]
    [Min(0.5f)]
    public int actionCount; // số lần AI có thể hành động mỗi lượt
    [ListDrawerSettings(IsReadOnly = true)]
    [TableList]
    public List<AI_Config> configList = new List<AI_Config>();
}