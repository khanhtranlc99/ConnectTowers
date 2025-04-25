using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UNITS/PropertiesBase/ExecutionerData")]
public class ExecutionerData : PropertiesUnitsBase
{

    [Space(10)]
    [Header("ExecutionerData")]
    [SerializeField] float increase_Speed;
    [SerializeField] float dupllice;

    // Rút gọn các biến trùng
    [SerializeField] int heavy_Hit;
    [SerializeField] int bonus_Attack;
    [SerializeField] int bonus_Duplicate_To_All;
    [SerializeField] int bonus_Speed_To_All;

    public ExecutionerUpgrade executionerUpgrade;

    public override float GetSkillValue(string name)
    {
        if (name == "Increase speed")
            return GetIncrease_Speed;
        if (name == "Dupllice")
            return GetDupllice;
        return base.GetSkillValue(name);
    }


    public float GetIncrease_Speed
    {
        get { return increase_Speed + 0.25f * (float)currentLevel; }
    }
    public float GetDupllice
    {
        get { return dupllice + 0.1f * (float)currentLevel; }
    }
    public float GetHeavy_Hit_0
    {
        get { return executionerUpgrade.GetValueByStar(starLevel).propertiesExecutionerUpgradeData.heavy_Hit_0 + heavy_Hit; }
    }

    public float GetHeavy_Hit_1
    {
        get { return executionerUpgrade.GetValueByStar(starLevel).propertiesExecutionerUpgradeData.heavy_Hit_1 + heavy_Hit; }
    }

    public float GetBonus_Attack
    {
        get { return executionerUpgrade.GetValueByStar(starLevel).propertiesExecutionerUpgradeData.bonus_Attack + bonus_Attack; }
    }

    public float GetBonus_Duplicate_To_All
    {
        get { return executionerUpgrade.GetValueByStar(starLevel).propertiesExecutionerUpgradeData.bonus_Duplicate_To_All + bonus_Duplicate_To_All; }
    }

    public float GetBonus_Speed_To_All
    {
        get { return executionerUpgrade.GetValueByStar(starLevel).propertiesExecutionerUpgradeData.bonus_Speed_To_All + bonus_Speed_To_All; }
    }
}
