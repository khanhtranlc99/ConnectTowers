using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "USER/DataUserVip")]

public class DataUserVip : ScriptableObject
{
    [SerializeField] int currentVip;
    public int CurrentVip => currentVip;
    public void IncreaseVip()
    {
        this.currentVip++;
    }

    [SerializeField] List<V_RewardSystem> lsRewardSystems = new();
    public List<V_RewardSystem> LsRewardSystems => lsRewardSystems;

    public V_RewardSystem GetRewardSystem(int levelParam)
    {
        foreach(var child in this.lsRewardSystems) if(child.LevelVip == levelParam) return child;
        return null;
    }


    [Button("Reset Vip")]
    void ResetVip()
    {
        this.currentVip = 0;
    }
}

[System.Serializable]
public class V_RewardSystem
{
    [HorizontalGroup("Main", 0.8f)] // 80% bên trái
    [SerializeField] int levelVip;
    public int LevelVip => levelVip;
    [SerializeField] private int currentProgress;
    public int CurrentProgress => currentProgress;
    [SerializeField] private int totalProgress;
    public int TotalProgress => totalProgress;


    public void SetCurrentProgress()
    {
        this.currentProgress++;
        if (this.currentProgress < this.totalProgress) return;
        GameController.Instance.dataContain.dataUser.DataUserVip.IncreaseVip();
    }


    [HorizontalGroup("Main", 0.2f)] // 20% bên phải
    [PreviewField(50, ObjectFieldAlignment.Center)]
    [HideLabel]
    [SerializeField] private Sprite iconVip;
    public Sprite IconVip => iconVip;
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

