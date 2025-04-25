using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UNITS/PropertiesBase/QuackData")]
public class QuackData : PropertiesUnitsBase
{

    [Space(10)]
    [Header("QuackData")]
    [SerializeField] float increase_Speed;
    [SerializeField] float dupllice;

    // Biến rút gọn
    [SerializeField] int bonus_Move_Speed;
    [SerializeField] int bonus_Attack_To_All;
    [SerializeField] int bonus_Duplicate_To_All;
    [SerializeField] int bonus_Health_To_All;

    public QuackUpgrade quackUpgrade;

    public override float GetSkillValue(string name)
    {
        if (name == "Increase speed")
            return GetIncrease_Speed;
        if (name == "Dupllice")
            return GetDupllice;
        return base.GetSkillValue(name);
    }

    // Getter cho các giá trị tính toán
    public float GetIncrease_Speed
    {
        get { return increase_Speed + 0.5f * (float)currentLevel; }
    }

    public float GetDupllice
    {
        get { return dupllice + 0.1f * (float)currentLevel; }
    }

    public float GetBonus_Move_Speed_0
    {
        get { return quackUpgrade.GetValueByStar(starLevel).propertiesQuackUpgradeData.bonus_Move_Speed_0 + bonus_Move_Speed; }
    }

    public float GetBonus_Move_Speed_1
    {
        get { return quackUpgrade.GetValueByStar(starLevel).propertiesQuackUpgradeData.bonus_Move_Speed_1 + bonus_Move_Speed; }
    }

    public float GetBonus_Attack_To_All
    {
        get { return quackUpgrade.GetValueByStar(starLevel).propertiesQuackUpgradeData.bonus_Attack_To_All + bonus_Attack_To_All; }
    }

    public float GetBonus_Duplicate_To_All
    {
        get { return quackUpgrade.GetValueByStar(starLevel).propertiesQuackUpgradeData.bonus_Duplicate_To_All + bonus_Duplicate_To_All; }
    }

    public float GetBonus_Health_To_All
    {
        get { return quackUpgrade.GetValueByStar(starLevel).propertiesQuackUpgradeData.bonus_Health_To_All + bonus_Health_To_All; }
    }
}
