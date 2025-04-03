using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Runtime.CompilerServices;
using System.Data;
using UnityEngine.SocialPlatforms.Impl;


[CreateAssetMenu(menuName = "USER/UserDataGame")]
public class DataUserGame : ScriptableObject
{
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
    public List<DataUnitsCard> LsDataUnitsCard => lsDataUnitsCard;


    #region Json
    public void LoadCardInventoryData()
    {
        CardInventorySystem cardInventorySystem = CardUnitsSaveSystem_Json.GetDataCardInventory();
       
        for(int i = 0; i < this.lsDataUnitsCard.Count; i++)
        {
            this.lsDataUnitsCard[i].cardCount = cardInventorySystem.lsCards[i].cardCount;
            this.lsDataUnitsCard[i].unit.currentLevel = cardInventorySystem.lsCards[i].level;
            this.lsDataUnitsCard[i].unit.starLevel = cardInventorySystem.lsCards[i].star;
        }

        this.currentCardSoldier = GameController.Instance.dataContain.dataUnits.GetPropertiesWithUnitId(cardInventorySystem.id_Soldier);
        this.currentCardBeast = GameController.Instance.dataContain.dataUnits.GetPropertiesWithUnitId(cardInventorySystem.id_Beast);
        this.currentCardMage = GameController.Instance.dataContain.dataUnits.GetPropertiesWithUnitId(cardInventorySystem.id_Mage);

        Debug.LogError("Day la DataUser " + cardInventorySystem.lsCards.Count);
    }
    #endregion
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
        CardUnitsSaveSystem_Json.SaveDataCardInventory(this);

    }
    public void SetCurrentCardBeast(PropertiesUnitsBase unitData)
    {
        this.currentCardBeast = unitData;
        CardUnitsSaveSystem_Json.SaveDataCardInventory(this);

    }
    public void SetCurrentCardMage(PropertiesUnitsBase unitData)
    {
        this.currentCardMage = unitData;
        CardUnitsSaveSystem_Json.SaveDataCardInventory(this);

    }

    public void SetCoinIncrease(int amountIncrease)
    {
        UseProfile.D_INCREASE_COIN = amountIncrease;
    }

    public void SetGemIncrease(int amountIncrease)
    {
        UseProfile.D_INCREASE_GEM = amountIncrease;
    }

    public void AddCoinReduct(int amountReduct)
    {
        UseProfile.D_REDEDUCT_COIN += amountReduct;
    }

    public void AddGemReduct(int amountReduct)
    {
        UseProfile.D_REDEDUCT_GEM += amountReduct;
    }

    public void AddCards(PropertiesUnitsBase unit, int amount)
    {
        DataUnitsCard unitCard = FindUnitCard(unit);
        if (unitCard != null) unitCard.cardCount += amount;
        else lsDataUnitsCard.Add(new DataUnitsCard(unit, amount));
        Debug.Log("Add Card Complete");

        CardUnitsSaveSystem_Json.SaveDataCardInventory(this);
    }
    public void AddCoins(int amount)
    {
        UseProfile.D_COIN += (int)(amount * (UseProfile.D_INCREASE_COIN/100f + 1));
    }
    public void AddGems(int amount)
    {
        UseProfile.D_GEM += (int)(amount * (UseProfile.D_INCREASE_GEM / 100f + 1));
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
        UseProfile.D_COIN -= (int)(coinDeduct * ( 1 - UseProfile.D_REDEDUCT_COIN / 100f));
    }
    public void DeductGem(int gemDeduct)
    {
        UseProfile.D_GEM -= (int)(gemDeduct * (1 - UseProfile.D_REDEDUCT_GEM / 100f));
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
        UseProfile.D_COIN += 2000;
        UseProfile.D_GEM += 2000;
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
    [Button("Json Card", ButtonSizes.Large)]
    void TestJson()
    {
        CardUnitsSaveSystem_Json.SaveDataCardInventory(this);
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
