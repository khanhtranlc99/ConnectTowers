using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEditor.Experimental.GraphView;

[CreateAssetMenu(menuName = "USER/DataUserVip")]

public class DataUserVip : ScriptableObject
{
    [SerializeField] List<V_RewardSystem> lsRewardSystems = new();
    public List<V_RewardSystem> LsRewardSystems => lsRewardSystems;
}

[System.Serializable]
public class V_RewardSystem
{
    [HorizontalGroup("Main", 0.8f)] // 80% bên trái
    [SerializeField] int levelVip;
    [SerializeField] private int currentProgress;
    [SerializeField] private int totalProgress;
    [HorizontalGroup("Main", 0.2f)] // 20% bên phải
    [PreviewField(50, ObjectFieldAlignment.Center)]
    [HideLabel]
    [SerializeField] private Sprite iconVip;
    [SerializeField] List<V_RewardCategory> lsRewardCetegorys = new();
    public List<V_RewardCategory> LsRewardCategorys => lsRewardCetegorys;
}

[System.Serializable]
public class V_RewardCategory
{
    [SerializeField] private List<V_RewardSlot> lsRewardSlots = new();
    public List<V_RewardSlot> LsRewardSlots => lsRewardSlots;


}

[System.Serializable]
public class V_RewardSlot
{
    [PreviewField(50)]
    [HideLabel]
    [SerializeField] Sprite iconReward;
    public Sprite IconReward => iconReward;
    [SerializeField] int amountReward;
    public int AmountReward => amountReward;
}

