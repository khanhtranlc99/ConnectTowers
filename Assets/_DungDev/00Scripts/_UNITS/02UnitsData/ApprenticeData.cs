﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UNITS/PropertiesBase/ApprenticeData")]
public class ApprenticeData : PropertiesUnitsBase
{

    [Space(10)]
    [Header("ApprenticeData")]
    [SerializeField] float dupllice;

    // Các biến rút gọn
    [SerializeField] int bonus_Move_Speed;
    [SerializeField] int bonus_Speed_To_All;
    [SerializeField] int bonus_Gold_To_All;

    public ApprenticeUpgrade apprenticeUpgrade;


    public override float GetSkillValue(string name)
    {
        if (name == "Dupllice")
            return GetDupllice;
        return base.GetSkillValue(name);
    }



    public float GetDupllice
    {
        get { return dupllice + 0.1f * (float)currentLevel; }
    }

    public float GetBonus_Move_Speed_0
    {
        get { return apprenticeUpgrade.GetValueByStar(starLevel).propertiesApprenticeUpgradeData.bonus_Move_Speed_0 + bonus_Move_Speed; }
    }

    public float GetBonus_Move_Speed_1
    {
        get { return apprenticeUpgrade.GetValueByStar(starLevel).propertiesApprenticeUpgradeData.bonus_Move_Speed_1 + bonus_Move_Speed; }
    }

    public float GetBonus_Speed_To_All
    {
        get { return apprenticeUpgrade.GetValueByStar(starLevel).propertiesApprenticeUpgradeData.bonus_Speed_To_All + bonus_Speed_To_All; }
    }

    public float GetBonus_Gold_To_All_0
    {
        get { return apprenticeUpgrade.GetValueByStar(starLevel).propertiesApprenticeUpgradeData.bonus_Gold_To_All_0 + bonus_Gold_To_All; }
    }

    public float GetBonus_Gold_To_All_1
    {
        get { return apprenticeUpgrade.GetValueByStar(starLevel).propertiesApprenticeUpgradeData.bonus_Gold_To_All_1 + bonus_Gold_To_All; }
    }
}
