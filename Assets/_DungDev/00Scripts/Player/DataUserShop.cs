
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
    }
    #endregion

    #region Odin
    [Button("Reset reward", ButtonSizes.Medium)]
    void ResetReward()
    {
        foreach (var child in this.lsIsRewardCollected) child.isCollected = false;
    }

    [Button("SetUP ID lsDataUnis",ButtonSizes.Medium)]
    void SetUpID()
    {
        for(int i = 0; i < lsDataShopReroll.Count; i++)
        {
            lsDataShopReroll[i].idCard = i;
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

}
[System.Serializable]
public class DataRewardCollected
{
    public bool isCollected;
}