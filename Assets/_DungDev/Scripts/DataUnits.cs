using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum UnitsType
{
    Slave,
    Guard,
    Executioner,
    Battle_Monk,
    Mammoth,
    Wounded_Mammoth,
    Titan,
    Terror_Bird,
    Apprentice,
    Quack,
    Super_Quack
}

public enum UnitRank
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legend,
}

[CreateAssetMenu(menuName = "DataUnits/ DataUnits")]
public class DataUnits : ScriptableObject
{
    public List<PropertiesBase> lsPropertiesBases;

    public PropertiesBase GetPropertiesBases(UnitsType unitsType)
    {
        foreach (var child in this.lsPropertiesBases)
        {
            if(child.unitType == unitsType) return child;
        }
        return null;
    }


}
