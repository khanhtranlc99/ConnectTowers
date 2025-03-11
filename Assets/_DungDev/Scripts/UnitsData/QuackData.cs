using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PropertiesBase/QuackData")]
public class QuackData : PropertiesBase
{
    public float increase_Speed;
    public float dupllice;

    // Biến rút gọn
    public int bonus_Move_Speed;
    public int bonus_Attack_To_All;
    public int bonus_Duplicate_To_All;
    public int bonus_Health_To_All;

    public QuackUpgrade quackUpgrade;

    // Getter cho các giá trị tính toán
    public float GetIncrease_Speed
    {
        get { return increase_Speed * 0.5f * (float)currentLevel; }
    }

    public float GetDupllice
    {
        get { return dupllice * 0.1f * (float)currentLevel; }
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
