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

    public void IncreaseTotalPerHour(int hourParam)
    {
        this.gemTotal = this.gemPerHour * hourParam;
        this.coinTotal = this.coinPerHour * hourParam;
    }

    public void CaculateHourOffine()
    {
        var secondOffline = TimeManager.CaculateTime(UseProfile.OffineRewardTime,System.DateTime.Now);
        var minuteOffline = (int)(secondOffline / 60);
        var hourOffline = (int)(minuteOffline / 60);
        this.currentHour = hourOffline;
        this.IncreaseTotalPerHour(this.currentHour);
    }

    #region Odin
    [Button("Increase 1 hour")]
    void IncreaseTimeOneHour()
    {
        this.currentHour += 1;
        this.IncreaseTotalPerHour(currentHour);
    }

    [Button("Reset hour")]
    void ResetTimeHour()
    {
        this.currentHour = 0;
    }

    #endregion
}
