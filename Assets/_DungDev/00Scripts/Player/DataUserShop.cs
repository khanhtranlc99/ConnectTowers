
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "USER/UserDataShop")]

public class DataUserShop : ScriptableObject
{
    [Header("Reward Daily")]
    [SerializeField] List<DataRewardCollected> lsIsRewardCollected = new();
    public List<DataRewardCollected> LsIsRewardCollected => lsIsRewardCollected;
    [Header("Data reroll")]
    [SerializeField] List<DataShopReroll> lsDataShopReroll = new();
    public List<DataShopReroll> LsDataShopReroll => lsDataShopReroll;

    public void SetListDataUnits(int id,PropertiesUnitsBase propertiesUnitsBase, int amountParam)
    {
        
        for (int i = 0; i < lsDataShopReroll.Count; i++)
        {
            if (lsDataShopReroll[i].idCard == id)
            {
                lsDataShopReroll[i].propertiesUnits = propertiesUnitsBase;
                lsDataShopReroll[i].currentCostAmount = amountParam;
                return;
            }
        }
    }

    public DataShopReroll GetDataShopReroll(int id)
    {
        foreach (var child in this.lsDataShopReroll) if (child.idCard == id) return child;
        return null;
    }


    #region  REset Daily
    public void ResetDailyShop()
    {

        foreach (var child in this.lsIsRewardCollected) child.isCollected = false;
        //Random card khi qua ngay moi
        this.RandomCardDaily();

    }
    #endregion

    #region Odin
    [Button("Test Random Card Daily", ButtonSizes.Large)]
    void RandomCardDaily()
    {
        var dataUnit = GameController.Instance.dataContain.dataUnits;
        Dictionary<UnitRank, List<PropertiesUnitsBase>> unitRankDict = new();
        foreach(var unit in dataUnit.lsPropertiesBases)
        {
            if(!unitRankDict.ContainsKey(unit.unitRank))
                unitRankDict[unit.unitRank] = new List<PropertiesUnitsBase>();

            unitRankDict[unit.unitRank].Add(unit);
        }

        foreach(var slot in this.lsDataShopReroll)
        {
            List<PropertiesUnitsBase> lsUnits = new();

            foreach(var rank in slot.lsUnitRanks)
            {
                if (unitRankDict.ContainsKey(rank)) lsUnits.AddRange(unitRankDict[rank]);
            }

            slot.propertiesUnits = lsUnits[Random.Range(0, lsUnits.Count)];
        }
    }

    [Button("Reset reward", ButtonSizes.Large)]
    void ResetReward()
    {
        foreach (var child in this.lsIsRewardCollected) child.isCollected = false;
    }

    [Button("SetUp lsDataUnis",ButtonSizes.Large)]
    void SetUpID()
    {
        for(int i = 0; i < lsDataShopReroll.Count; i++)
        {
            lsDataShopReroll[i].idCard = i;
            if(i < 5)
            {
                lsDataShopReroll[i].lsUnitRanks = new List<UnitRank>() {UnitRank.Common, UnitRank.Uncommon, UnitRank.Rare};
            }
            else if(i < 7)
            {
                lsDataShopReroll[i].lsUnitRanks = new List<UnitRank>() { UnitRank.Common, UnitRank.Uncommon, UnitRank.Rare, UnitRank.Epic };
            }
            else if(i < 8)
            {
                lsDataShopReroll[i].lsUnitRanks = new List<UnitRank>() {UnitRank.Uncommon, UnitRank.Rare, UnitRank.Epic, UnitRank.Legend };
            }
            else
            {
                lsDataShopReroll[i].lsUnitRanks = new List<UnitRank>() { UnitRank.Rare, UnitRank.Epic, UnitRank.Legend };

            }
        }

    }
    #endregion
}
[System.Serializable]
public class DataShopReroll
{
    public int idCard;
    public int currentCostAmount;
    [SerializeField] int defaultCostAmout;
    public int DefaultCostAmount => defaultCostAmout;
    public PropertiesUnitsBase propertiesUnits;
    public List<UnitRank> lsUnitRanks;

}
[System.Serializable]
public class DataRewardCollected
{
    public bool isCollected;
}