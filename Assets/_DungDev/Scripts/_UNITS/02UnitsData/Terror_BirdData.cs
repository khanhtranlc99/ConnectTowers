using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UNITS/PropertiesBase/Terror_BirdData")]
public class Terror_BirdData : PropertiesUnitsBase
{
    public float increase_Speed;

    // Biến rút gọn
    public int bonus_Attack;
    public int bonus_Move_Speed;
    public int carnivorous;

    public Terror_BirdUpgrade terror_BirdUpgrade;

    // Getter cho các giá trị tính toán
    public float GetIncrease_Speed
    {
        get { return increase_Speed * 0.2f * (float)currentLevel; }
    }

    public float GetBonus_Attack_0
    {
        get { return terror_BirdUpgrade.GetValueByStar(starLevel).propertiesTerrorBirdUpgradeData.bonus_Attack_0 + bonus_Attack; }
    }

    public float GetBonus_Attack_1
    {
        get { return terror_BirdUpgrade.GetValueByStar(starLevel).propertiesTerrorBirdUpgradeData.bonus_Attack_1 + bonus_Attack; }
    }

    public float GetBonus_Move_Speed_0
    {
        get { return terror_BirdUpgrade.GetValueByStar(starLevel).propertiesTerrorBirdUpgradeData.bonus_Move_Speed_0 + bonus_Move_Speed; }
    }

    public float GetBonus_Move_Speed_1
    {
        get { return terror_BirdUpgrade.GetValueByStar(starLevel).propertiesTerrorBirdUpgradeData.bonus_Move_Speed_1 + bonus_Move_Speed; }
    }

    public float GetCarnivorous
    {
        get { return terror_BirdUpgrade.GetValueByStar(starLevel).propertiesTerrorBirdUpgradeData.carnivorous + carnivorous; }
    }
}
