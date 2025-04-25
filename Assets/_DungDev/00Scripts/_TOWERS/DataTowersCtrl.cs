using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TowerType
{
    Soldier,
    Beast,
    Mage,
    GreenSoldider,
    GreenBeast,
    GreenMage,
    RedSoldider,
    RedBeast,
    RedMage,
    Arrow,
}

[CreateAssetMenu(menuName = "TOWERS/DataTowersCtrl/ DataTowersCtrl")]

public class DataTowersCtrl : ScriptableObject
{
    public List<PropertiesTowersBase> lsPropertiesTowersBases;

    public PropertiesTowersBase GetPropertiesBases(TowerType towerType)
    {
        foreach (var child in this.lsPropertiesTowersBases)
        {
            if (child.towerType == towerType) return child;
        }
        return null;
    }
}
