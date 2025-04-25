using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "USER/DataOfflineRewardChest")]

public class DataOfflineRewardChest : ScriptableObject
{
    [SerializeField] int gemPerHour;
    public int GemPerHour => gemPerHour;
    [SerializeField] int coinPerHour;
    public int CoinPerHour => coinPerHour;


    [SerializeField] int gemTotal;
    public int GemTotal => gemTotal;
    [SerializeField] int coinTotal;
    public int CoinTotal => coinTotal;

    [SerializeField] int currentHour;


    public void DeductClaimReward()
    {
        this.gemTotal = 0;
        this.coinTotal = 0;
    }

    public void IncreaseTotalPerHour()
    {
        this.gemTotal = this.gemPerHour * this.currentHour;
        this.coinTotal = this.coinPerHour * this.currentHour;
    }

    #region Odin
    [Button("Increase 1 hour")]
    void IncreaseTimeOneHour()
    {
        this.currentHour += 1;
        this.IncreaseTotalPerHour();
    }

    [Button("Reset hour")]
    void ResetTimeHour()
    {
        this.currentHour = 0;
    }

    #endregion
}
