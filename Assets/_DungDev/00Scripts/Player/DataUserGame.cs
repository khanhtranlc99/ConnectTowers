using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Runtime.CompilerServices;
using System.Data;


[CreateAssetMenu(menuName = "USER/UserDataGame")]
public class DataUserGame : ScriptableObject
{
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
    public List<DataUnitsCard> LsDataUnitsCard => lsDataUnitsCard;
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
