using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "PropertiesBase/ GuardData")]

public class GuardData : PropertiesBase
{
    public float dupllice;

    public int gain_Shield;
    public int bonus_Move_Speed;

    public GuardUpgrade guardUpgrade;


    public float GetDupllice
    {
        get { return dupllice * 0.2f * (float)currentLevel; }
    }
    public float GetGain_Shield_0
    {
        get { return guardUpgrade.GetValueByStar(starLevel).propertiesGuardUpgradeDatas.gain_Shield_0 + gain_Shield; }
    }
    public float GetBonus_Move_Speed_0
    {
        get { return guardUpgrade.GetValueByStar(starLevel).propertiesGuardUpgradeDatas.bonus_Move_Speed_0 + bonus_Move_Speed; }
    }
    public float GetGain_Shield_1
    {
        get { return guardUpgrade.GetValueByStar(starLevel).propertiesGuardUpgradeDatas.gain_Shield_1 + gain_Shield; }
    }
    public float GetBonus_Move_Speed_1
    {
        get { return guardUpgrade.GetValueByStar(starLevel).propertiesGuardUpgradeDatas.bonus_Move_Speed_1 + bonus_Move_Speed; }
    }
    public float GetGain_Shield_2
    {
        get { return guardUpgrade.GetValueByStar(starLevel).propertiesGuardUpgradeDatas.gain_Shield_2 + bonus_Move_Speed; }
    }


}
