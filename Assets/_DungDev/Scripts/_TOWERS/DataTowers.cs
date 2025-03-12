using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TowerType
{
    Soldier,
    Beast,
    Mage,
}

[CreateAssetMenu(menuName = "DataTowers/ DataTowers")]

public class DataTowers : ScriptableObject
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
