using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UNITS/PropertiesBase/TitanData")]
public class TitanData : PropertiesUnitsBase
{
    [Space(10)]
    [Header("TitanData")]
    [SerializeField] float increase_Attack;

    // Biến rút gọn
    [SerializeField] int bonus_Attack;
    [SerializeField] int bonus_Speed_To_All;
    [SerializeField] int bonus_Gold_To_All;

    public TitanUpgrade titanUpgrade;

    public override float GetSkillValue(string name)
    {
        if (name == "Increase attack")
            return GetIncrease_Attack;
        return base.GetSkillValue(name);
    }


    // Getter cho các giá trị tính toán
    public float GetIncrease_Attack
    {
        get { return increase_Attack + 0.2f * (float)currentLevel; }
    }

    public float GetBonus_Attack_0
    {
        get { return titanUpgrade.GetValueByStar(starLevel).propertiesTitanUpgradeData.bonus_Attack_0 + bonus_Attack; }
    }

    public float GetBonus_Attack_1
    {
        get { return titanUpgrade.GetValueByStar(starLevel).propertiesTitanUpgradeData.bonus_Attack_1 + bonus_Attack; }
    }

    public float GetBonus_Speed_To_All
    {
        get { return titanUpgrade.GetValueByStar(starLevel).propertiesTitanUpgradeData.bonus_Speed_To_All + bonus_Speed_To_All; }
    }

    public float GetBonus_Gold_To_All_0
    {
        get { return titanUpgrade.GetValueByStar(starLevel).propertiesTitanUpgradeData.bonus_Gold_To_All_0 + bonus_Gold_To_All; }
    }

    public float GetBonus_Gold_To_All_1
    {
        get { return titanUpgrade.GetValueByStar(starLevel).propertiesTitanUpgradeData.bonus_Gold_To_All_1 + bonus_Gold_To_All; }
    }
}
