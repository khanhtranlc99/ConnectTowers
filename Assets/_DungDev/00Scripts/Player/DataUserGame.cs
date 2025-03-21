using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Runtime.CompilerServices;


[CreateAssetMenu(menuName = "USER/UserDataGame")]
public class DataUserGame : ScriptableObject
{
    [SerializeField] int coin;
    public int Coin => coin;
    [SerializeField] int gem;
    public int Gem => gem;

    [SerializeField] PropertiesUnitsBase currentCardSoldier;
    public PropertiesUnitsBase CurrentCardSoldier => currentCardSoldier;
    [SerializeField] PropertiesUnitsBase currentCardBeast;
    public PropertiesUnitsBase CurrentCardBeast => currentCardBeast;
    [SerializeField] PropertiesUnitsBase currentCardMage;
    public PropertiesUnitsBase CurrentCardMage=> currentCardMage;
    [Space(10)]
    [SerializeField] List<DataUnitsCard> lsDataUnitsCard = new();
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


    public void AddCards(PropertiesUnitsBase unit, int amount)
    {
        DataUnitsCard unitCard = FindUnitCard(unit);
        if (unitCard != null) unitCard.cardCount += amount;
        else lsDataUnitsCard.Add(new DataUnitsCard(unit, amount));

        Debug.Log("Add Card Complete");
    }

    public void AddCoins(int amount)
    {
        this.coin += amount;
    }
    public void AddGems(int amount)
    {
        this.gem += amount;
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
        this.coin -= coinDeduct;
    }
    public void DeductGem(int gem)
    {
        this.gem -= gem;
    }

    #region Odin
    [Button("Buff Gem Coin")]
    void BuffCoinGem()
    {
        this.gem += 1000;
        this.coin += 1000;
    }
    [Button("Buff All Cards")]
    void BuffAllCard()
    {
        foreach (var child in this.lsDataUnitsCard)
            child.cardCount += 10;
        Debug.Log("Buff thanh cong");
    }

    [Button("Reset Value Card")]
    void ResetValueCard()
    {
        foreach (var child in this.lsDataUnitsCard) child.cardCount = 0;
        Debug.Log("Reset thanh cong");
    }

    [Button("Buff Specific Unit")]
    void BuffCardForUnit(PropertiesUnitsBase unit, int amount = 10)
    {
        AddCards(unit, amount);
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
