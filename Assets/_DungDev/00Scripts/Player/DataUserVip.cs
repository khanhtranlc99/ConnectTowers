using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;


[CreateAssetMenu(menuName = "USER/DataUserVip")]

public class DataUserVip : ScriptableObject
{
    [SerializeField] int currentVip;
    public int CurrentVip => currentVip;

    [SerializeField] int currentProgress;
    public int CurrentProgress => currentProgress;
    [Header("Reward Daily")]
    [SerializeField] int currentDay;
    public int CurrentDay => currentDay;
    [Header("Result Sprite Reward")]
    [PreviewField(50)]
    [HideLabel]
    [SerializeField] Sprite imgGem;
    [PreviewField(50)]
    [HideLabel]
    [SerializeField] Sprite imgVipPoint;

    [Header("RewardSystem VIP")]
    [SerializeField] List<V_RewardSystem> lsRewardSystems = new();
    public List<V_RewardSystem> LsRewardSystems => lsRewardSystems;

    [Header("RewardSystem FREEVIP")]
    [SerializeField] List<V_RewardDailySystem> lsRewardDailySystems = new();
    public List<V_RewardDailySystem> LsRewardDailySystems => lsRewardDailySystems;


    public void IncreaseDay()
    {
        this.currentDay++;
    }

    public void IncreaseVip()
    {
        this.currentVip++;
        EventDispatcher.EventDispatcher.Instance.PostEvent(EventID.UPDATE_VIPPARAM);
        EventDispatcher.EventDispatcher.Instance.PostEvent(EventID.UPDATE_TILE_VIPBOX, currentVip);
        EventDispatcher.EventDispatcher.Instance.PostEvent(EventID.UPDATE_AVATAR_VIP, lsRewardSystems[currentVip].IconVip);
        EventDispatcher.EventDispatcher.Instance.PostEvent(EventID.UPDATE_CENTER_VIP_BOX);
        Debug.LogError("Reset btn roi nhe");
    }


    public void IncreaseProgress(int amount)
    {
        this.currentProgress += amount;
        this.GetRewardSystem(currentVip).HandleUpVipProgress(this);
        EventDispatcher.EventDispatcher.Instance.PostEvent(EventID.UPDATE_VIP_BOX);

    }

    public void DeDuctProgress(int deduct)
    {
        this.currentProgress -= deduct;
    }


    public V_RewardSystem GetRewardSystem(int levelParam)
    {
        foreach(var child in this.lsRewardSystems) if(child.LevelVip == levelParam) return child;
        return null;
    }

    #region OdinInpec
    [TabGroup("RESET")]
    [Button("Reset Vip & CurrentDay", ButtonSizes.Large), GUIColor(1f, 0.8f, 0f)]
    void ResetVip_CurrentDay()
    {
        this.currentVip = 0;
        this.currentProgress = 0;
        this.currentDay = 1;
        foreach(var child in this.lsRewardSystems)
        {
            foreach(var category in child.LsRewardCategorys)
            {
                category.isClaim = false;
            }
        }
    }
    [TabGroup("RESET")]
    [Button("Reset FreeVip Reward", ButtonSizes.Large), GUIColor(0.2f, 0.8f, 1f)]
    void ResetFreeVip()
    {
        foreach (var child in this.lsRewardDailySystems)
        {
            child.isCollected = false;
        }
    }


    [TabGroup("SETUP SYSTEM")]
    [Button("SetUp VIP", ButtonSizes.Large)]
    void SetUp()
    {
        float baseValue = 10f;
        float growthFactor = 1.5f;

        int baseCoinIncrease = 5, incrementCoinIncrease = 1;
        int baseGemIncrease = 3, incrementGemIncrease = 1;
        int baseCoinReduct =2, incrementCoinReduct = 1;
        int baseGemReduct = 1, incrementGemReduct = 1;

        for(int i = 0; i < this.lsRewardSystems.Count; i++)
        {
            this.lsRewardSystems[i].SetUpLevelVip(i);
            int totalProgess = Mathf.RoundToInt(baseValue * Mathf.Pow(growthFactor, i));
            this.lsRewardSystems[i].SetUpTotalProgess(totalProgess);

            //SetUp Reward
            int coinIncrease = (this.lsRewardSystems[i].LevelVip == 0) ? 0 : baseCoinIncrease + (incrementCoinIncrease * (this.lsRewardSystems[i].LevelVip - 1));
            int gemIncrease = (this.lsRewardSystems[i].LevelVip == 0) ? 0 : baseGemIncrease + (incrementGemIncrease * (this.lsRewardSystems[i].LevelVip - 1));
            int coinReduct = (this.lsRewardSystems[i].LevelVip == 0) ? 0 : baseCoinReduct + (incrementCoinReduct * (this.lsRewardSystems[i].LevelVip - 1));
            int gemReduct = (this.lsRewardSystems[i].LevelVip == 0) ? 0 : baseGemReduct + (incrementGemReduct * (this.lsRewardSystems[i].LevelVip - 1));


            var rewardSlot = this.lsRewardSystems[i].RewardIncreaseSlot;
            rewardSlot.SetUpValues(coinIncrease, gemIncrease,coinReduct,gemReduct);
        }
    }

    [TabGroup("SETUP SYSTEM")]
    [Button("SetUp FreeVIP",ButtonSizes.Large)]
    void SetUpFreeVip()
    {
        int[] dayOffsets = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 13, 16, 20, 25, 31, 38, 46, 55, 65, 76, 90 };

        for (int i = 0; i < lsRewardDailySystems.Count; i++)
        {
            var rewardDailySytem = lsRewardDailySystems[i];

            rewardDailySytem.SetUpDay(dayOffsets[i]);

            //set up slot phan thuong
            if(rewardDailySytem.LsRewardSlots.Count < 2)
            {
                while(rewardDailySytem.LsRewardSlots.Count < 2)
                {
                    rewardDailySytem.LsRewardSlots.Add(new V_RewardSlot());
                }
            }

            //set up phan thuong
            int gemReward = 10 + (i * 10);
            int vipPointReward = 5 + (i * 5);

            rewardDailySytem.LsRewardSlots[0].SetUpReward(ResultType.Gem, gemReward, this.imgGem);
            rewardDailySytem.LsRewardSlots[1].SetUpReward(ResultType.Vip, vipPointReward, this.imgVipPoint);
        }
    }

    #endregion
}

[System.Serializable]
public class V_RewardSystem
{
    [HorizontalGroup("Main", 0.8f)] // 80% bên trái
    [SerializeField] int levelVip;
    public int LevelVip => levelVip;
    [SerializeField] private int totalProgress;
    public int TotalProgress => totalProgress;

    [HorizontalGroup("Main", 0.2f)] // 20% bên phải
    [PreviewField(50, ObjectFieldAlignment.Center)]
    [HideLabel]
    [SerializeField] private Sprite iconVip;
    public Sprite IconVip => iconVip;

    [SerializeField] V_RewardIncreaseSlot rewardIncreaseSlot;
    public V_RewardIncreaseSlot RewardIncreaseSlot => rewardIncreaseSlot;

    [SerializeField] List<V_RewardCategory> lsRewardCetegorys = new();
    public List<V_RewardCategory> LsRewardCategorys => lsRewardCetegorys;


    public void HandleUpVipProgress(DataUserVip dataVip)
    {
        if (dataVip.CurrentProgress < this.totalProgress) return;
        dataVip.DeDuctProgress(this.totalProgress);

        var dataUserVip = GameController.Instance.dataContain.dataUser.DataUserVip;
        dataUserVip.IncreaseVip();
        var dataUserWithVip = dataUserVip.LsRewardSystems[dataUserVip.CurrentVip];
        GameController.Instance.dataContain.dataUser.SetCoinIncrease(dataUserWithVip.RewardIncreaseSlot.CoinIncreaseAmount);
        GameController.Instance.dataContain.dataUser.SetGemIncrease(dataUserWithVip.RewardIncreaseSlot.GemIncreaseAmount);
    }

    public void SetUpLevelVip(int idParam)
    {
        this.levelVip = idParam;
    }
    public void SetUpTotalProgess(int totalProgress)
    {
        this.totalProgress = totalProgress;
    }
}


[System.Serializable]
public class V_RewardDailySystem
{
    [SerializeField] int day;
    public int Day => day;
    public bool isCollected;
    [SerializeField] List<V_RewardSlot> lsRewardSlots = new();
    public List<V_RewardSlot> LsRewardSlots => lsRewardSlots;


    public void SetUpDay(int dayParam)
    {
        this.day = dayParam;
    }

}

[System.Serializable]
public class V_RewardCategory
{
    [SerializeField] private List<V_RewardSlot> lsRewardSlots = new();
    public List<V_RewardSlot> LsRewardSlots => lsRewardSlots;

    [Header("Bool")]
    public bool isClaim;
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


    //Odin SetUP =)) toi cam on chatgpt
    public void SetUpReward(ResultType resultType, int amountReward, Sprite iconReward)
    {
        this.resultType = resultType;
        this.amountReward = amountReward;
        this.iconReward = iconReward;
    }
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

    public void SetUpValues(int coinIncrease, int gemIncrease, int coinReduct, int gemReduct)
    {
        this.coinIncreaseAmount = coinIncrease;
        this.gemIncreaseAmount= gemIncrease;
        this.coinReductAmount = coinReduct;
        this.gemReductAmount = gemReduct;
    }
}

