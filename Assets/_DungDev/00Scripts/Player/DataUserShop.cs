
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "USER/UserDataShop")]

public class DataUserShop : ScriptableObject
{
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

    [Button("SetUP ID lsDataUnis")]
    void SetUpID()
    {
        for(int i = 0; i < lsDataShopReroll.Count; i++)
        {
            lsDataShopReroll[i].idCard = i;
        }

    }

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
