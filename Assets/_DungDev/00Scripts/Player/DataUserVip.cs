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

    [SerializeField] int currentProgress;
    public int CurrentProgress => currentProgress;

    public void IncreaseProgress(int amount)
    {
        this.currentProgress += amount;
        this.GetRewardSystem(currentVip).HandleUpVipProgress(this);
    }

    public void DeDuctProgress(int deduct)
    {
        this.currentProgress -= deduct;
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
    [Button("Btn buff vip")]
    void Buff()
    {
        //var rewardSytem = this.GetRewardSystem(this.currentVip);
        ////rewardSytem.IncreaseCurrentProgess(10);
        //rewardSytem.HandleUpVipProgress();
    }
}

[System.Serializable]
public class V_RewardSystem
{
    [HorizontalGroup("Main", 0.8f)] // 80% bên trái
    [SerializeField] int levelVip;
    public int LevelVip => levelVip;
    [SerializeField] private int totalProgress;
    public int TotalProgress => totalProgress;

    //[SerializeField] string 

    public void HandleUpVipProgress(DataUserVip dataVip)
    {
        if (dataVip.CurrentProgress < this.totalProgress) return;
        dataVip.DeDuctProgress(dataVip.CurrentProgress - this.totalProgress);

        var dataUserVip = GameController.Instance.dataContain.dataUser.DataUserVip;
        dataUserVip.IncreaseVip();
        var dataUserWithVip = dataUserVip.LsRewardSystems[dataUserVip.CurrentVip];
        GameController.Instance.dataContain.dataUser.SetCoinIncrease(dataUserWithVip.RewardIncreaseSlot.CoinIncreaseAmount);
        GameController.Instance.dataContain.dataUser.SetGemIncrease(dataUserWithVip.RewardIncreaseSlot.CoinIncreaseAmount);

    }


    [HorizontalGroup("Main", 0.2f)] // 20% bên phải
    [PreviewField(50, ObjectFieldAlignment.Center)]
    [HideLabel]
    [SerializeField] private Sprite iconVip;
    public Sprite IconVip => iconVip;

    [SerializeField] V_RewardIncreaseSlot rewardIncreaseSlot;
    public V_RewardIncreaseSlot RewardIncreaseSlot => rewardIncreaseSlot;

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
    [SerializeField] ResultType resultType;
    public ResultType ResultType => resultType;
    [SerializeField] int amountReward;
    public int AmountReward => amountReward;
}

[System.Serializable]
public class V_RewardIncreaseSlot
{
    [SerializeField] int coinIncreaseAmount;
    public int CoinIncreaseAmount => coinIncreaseAmount;

    [SerializeField] int gemIncreaseAmount;
    public int GemIncreaseAmount => gemIncreaseAmount;

    [SerializeField] int coinReductAmount;
    public  int CoinReductAmount => coinReductAmount;

    [SerializeField] int gemReductAmount;
    public int GemReductAmount => gemReductAmount;
}

