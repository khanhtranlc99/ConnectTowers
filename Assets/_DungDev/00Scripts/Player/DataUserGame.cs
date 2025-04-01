using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Runtime.CompilerServices;
using System.Data;


[CreateAssetMenu(menuName = "USER/UserDataGame")]
public class DataUserGame : ScriptableObject
{
    [SerializeField] float oldTime;
    [SerializeField] float currentTime;

    [Space(10)]

    [SerializeField] int coin;
    public int Coin => coin;
    [SerializeField] int gem;
    public int Gem => gem;

    [SerializeField] int coinIncrease;
    public int CoinIncrease => coinIncrease;

    [SerializeField] int gemIncrease;
    public int GemIncrease => gemIncrease;

    [SerializeField] int coinReduct;
    public int CoinReduct => coinReduct;

    [SerializeField] int gemReduct;
    public int GemReduct => gemReduct;


    [Header("DataShop")]
    [SerializeField] DataUserShop dataShop;
    public DataUserShop DataShop => dataShop;
    [Header("Data Quest Daily")]
    [SerializeField] DataDailyQuest dataDailyQuest;
    public DataDailyQuest DataDailyQuest => dataDailyQuest;
    [Header("Data Setting Box")]
    [SerializeField] DataSettingBoxGame dataSettingBoxGame;
    public DataSettingBoxGame DataSettingBoxGame => dataSettingBoxGame;

    [Header("Data Offline Reward Chest")]
    [SerializeField] DataOfflineRewardChest dataOfflineRewardChest;
    public DataOfflineRewardChest DataOfflineRewardChest => dataOfflineRewardChest;

    [Header("Data Vip")]
    [SerializeField] DataUserVip dataUserVip;
    public DataUserVip DataUserVip => dataUserVip;

    [Header("Data Profile")]
    [SerializeField] DataUserProfileGame dataUserProfileGame;
    public DataUserProfileGame DataUserProfileGame => dataUserProfileGame;

    [Space(10)]
    [SerializeField] PropertiesUnitsBase currentCardSoldier;
    public PropertiesUnitsBase CurrentCardSoldier => currentCardSoldier;
    [SerializeField] PropertiesUnitsBase currentCardBeast;
    public PropertiesUnitsBase CurrentCardBeast => currentCardBeast;
    [SerializeField] PropertiesUnitsBase currentCardMage;
    public PropertiesUnitsBase CurrentCardMage=> currentCardMage;
    [Space(10)]
    [SerializeField] List<DataUnitsCard> lsDataUnitsCard = new();

    public void InitDataVip()
    {
        dataUserVip.LoadVipData();
    }



    public DataUnitsCard FindUnitCard(PropertiesUnitsBase unit)
    {
        foreach (var child in this.lsDataUnitsCard)
        {
            if(child.unit == unit) return child;
        }
        return null;
    }

    public void SetCurrentCardSoldier(PropertiesUnitsBase unitData)
    {
        this.currentCardSoldier = unitData;
    }
    public void SetCurrentCardBeast(PropertiesUnitsBase unitData)
    {
        this.currentCardBeast = unitData;
    }
    public void SetCurrentCardMage(PropertiesUnitsBase unitData)
    {
        this.currentCardMage = unitData;
    }

    public void SetCoinIncrease(int amountIncrease)
    {
        this.coinIncrease = amountIncrease;
    }

    public void SetGemIncrease(int amountIncrease)
    {
        this.gemIncrease = amountIncrease;
    }

    public void AddCoinReduct(int amountReduct)
    {
        this.coinReduct += amountReduct;
    }

    public void AddGemReduct(int amountReduct)
    {
        this.gemReduct += amountReduct;
    }

    public void AddCards(PropertiesUnitsBase unit, int amount)
    {
        DataUnitsCard unitCard = FindUnitCard(unit);
        if (unitCard != null) unitCard.cardCount += amount;
        else lsDataUnitsCard.Add(new DataUnitsCard(unit, amount));

        Debug.Log("Add Card Complete");
    }

    public void AddCoins(int amount)
    {
        this.coin += (int)(amount * (this.coinIncrease/100f + 1));
    }
    public void AddGems(int amount)
    {
        this.gem += (int)(amount * (this.gemIncrease / 100f + 1));
    }
    public void DeductCard(int amount)
    {
        foreach(var child in this.lsDataUnitsCard)
        {
            if(child.cardCount >= amount)
            {
                child.cardCount -= amount;
                return;
            }
        }
    }

    public void DeductCoin(int coinDeduct)
    {
        this.coin -= (int)(coinDeduct * ( 1 - this.coinReduct / 100f));
    }
    public void DeductGem(int gemDeduct)
    {
        this.gem -= (int)(gemDeduct * (1 - this.gemReduct / 100f));
    }

    #region Odin
    [Button("Set Last Login To Yesterday", ButtonSizes.Large),GUIColor(0.827f, 0.294f, 0.333f)]
    void SetLastLoginToYesterday()
    {
        UseProfile.FirstTimeOpenGame = System.DateTime.Now.AddDays(-1);
        Debug.LogError("Chuyen thoi gian ve ngay hom qua");
    }

    [Button("Buff Gem Coin",ButtonSizes.Large),GUIColor(1f, 0.8f, 0f)]
    void BuffCoinGem()
    {
        this.gem += 2000;
        this.coin += 2000;
    }
    [Button("Buff All Cards", ButtonSizes.Large), GUIColor(0.5f, 1f, 0.5f)]
    void BuffAllCard()
    {
        foreach (var child in this.lsDataUnitsCard)
            child.cardCount += 10;
    }

    [Button("Reset Value Card", ButtonSizes.Large)]
    void ResetValueCard()
    {
        foreach (var child in this.lsDataUnitsCard) child.cardCount = 0;
    }
    #endregion


    #region ResetDaily

    public void ResetDailyDay()
    {
        if (!TimeManager.IsPassTheDay(UseProfile.FirstTimeOpenGame, System.DateTime.Now))
            return;
        dataDailyQuest.ResetDailyQuest();
        dataUserVip.IncreaseDay();
        dataShop.ResetDailyShop();
        UseProfile.FirstTimeOpenGame = System.DateTime.Now;
        Debug.LogError("Reset thanh cong");
    }
    #endregion
}

[System.Serializable]
public class DataUnitsCard
{
    public PropertiesUnitsBase unit;
    public int cardCount;

    public DataUnitsCard(PropertiesUnitsBase unitParam, int countParam)
    {
        this.unit = unitParam;
        this.cardCount = countParam;
    }
}
