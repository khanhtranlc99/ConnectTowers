using Sirenix.OdinInspector;
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

[CreateAssetMenu(menuName = "UNITS/DataUnits/ DataUnits")]
public class DataUnits : ScriptableObject
{
    public List<PropertiesUnitsBase> lsPropertiesBases;

    public PropertiesUnitsBase GetPropertiesWithUnitType(UnitsType unitsType)
    {
        foreach (var child in this.lsPropertiesBases)
        {
            if(child.unitType == unitsType) return child;
        }
        return null;
    }


    [Button("SetUP hp atk speed")]
    void SetUpInfoUnit()
    {
        for(int i = 0; i < lsPropertiesBases.Count; i++)
        {
            if (i < 4)
            {
                lsPropertiesBases[i].atk = 1;
                lsPropertiesBases[i].hp = 1;
                lsPropertiesBases[i].speed = 3;
            }
            else if (i < 8)
            {
                lsPropertiesBases[i].atk = 2;
                lsPropertiesBases[i].hp = 2;
                lsPropertiesBases[i].speed = 4;
            }
            else
            {
                lsPropertiesBases[i].atk = 2;
                lsPropertiesBases[i].hp = 2;
                lsPropertiesBases[i].speed = 4;
            }
        }
    }

}
