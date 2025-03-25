using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CostType
{
    Ads,
    Free,
    Coin,
    Gem,
    VND
}
[System.Serializable]

public class RewardItem 
{
    [Title("Reward Settings")]
    public ResultType resultType;
    public int amount;

    [Title("Cost Settings")]
    [OnValueChanged("UpdateCostAmount")]
    public CostType costType;

    [DisableIf("@costType == CostType.Ads || costType == CostType.Free")]
    [SerializeField] int costAmount;
    public int CostAmount => costAmount;
    private void UpdateCostAmount()
    {
        switch (costType)
        {
            case CostType.Ads: costAmount = 0; break;
            case CostType.Free: costAmount = 0; break;
        }
    }
}
