using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PropertiesBase/Super_QuackData")]
public class Super_QuackData : PropertiesUnitsBase
{
    public float increase_Speed;
    public float dupllice;
    public float increase_Attack;
    public float increase_Gold;

    // Các biến rút gọn
    public int bonus_Attack;
    public int dash;
    public int critical_Hit;

    public Super_QuackUpgrade super_QuackUpgrade;

    // Getter cho các giá trị tính toán
    public float GetIncrease_Speed
    {
        get { return increase_Speed * 0.7f * (float)currentLevel; }
    }

    public float GetDupllice
    {
        get { return dupllice * 0.2f * (float)currentLevel; }
    }

    public float GetIncrease_Attack
    {
        get { return increase_Attack * 0.5f * (float)currentLevel; }
    }

    public float GetIncrease_Gold
    {
        get { return increase_Gold * 3f * (float)currentLevel; }
    }

    public float GetDouble_Shield_0
    {
        get { return super_QuackUpgrade.GetValueByStar(starLevel).propertiesSuperQuackUpgradeData.double_Shield_0; }
    }

    public float GetDouble_Shield_1
    {
        get { return super_QuackUpgrade.GetValueByStar(starLevel).propertiesSuperQuackUpgradeData.double_Shield_1; }
    }

    public float GetBonus_Attack
    {
        get { return super_QuackUpgrade.GetValueByStar(starLevel).propertiesSuperQuackUpgradeData.bonus_Attack + bonus_Attack; }
    }

    public float GetDash
    {
        get { return super_QuackUpgrade.GetValueByStar(starLevel).propertiesSuperQuackUpgradeData.dash + dash; }
    }

    public float GetCritical_Hit
    {
        get { return super_QuackUpgrade.GetValueByStar(starLevel).propertiesSuperQuackUpgradeData.critical_Hit + critical_Hit; }
    }
}
