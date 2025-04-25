using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UNITS/PropertiesBase/MammothData")]
public class MammothData : PropertiesUnitsBase
{

    [Space(10)]
    [Header("MammothData")]
    [SerializeField] float dupllice;

    // Biến rút gọn
    [SerializeField] int bonus_Move_Speed;
    [SerializeField] int bonus_Speed_To_All;
    [SerializeField] int bonus_Gold_To_All;

    public MammothUpgrade mammothUpgrade;

    public override float GetSkillValue(string name)
    {
        if (name == "Dupllice")
            return GetDupllice;
        return base.GetSkillValue(name);
    }



    // Getter cho các giá trị tính toán
    public float GetDupllice
    {
        get { return dupllice + 0.2f * (float)currentLevel; }
    }

    public float GetBonus_Move_Speed_0
    {
        get { return mammothUpgrade.GetValueByStar(starLevel).propertiesMammothUpgradeData.bonus_Move_Speed_0 + bonus_Move_Speed; }
    }

    public float GetBonus_Move_Speed_1
    {
        get { return mammothUpgrade.GetValueByStar(starLevel).propertiesMammothUpgradeData.bonus_Move_Speed_1 + bonus_Move_Speed; }
    }

    public float GetBonus_Speed_To_All
    {
        get { return mammothUpgrade.GetValueByStar(starLevel).propertiesMammothUpgradeData.bonus_Speed_To_All + bonus_Speed_To_All; }
    }

    public float GetBonus_Gold_To_All_0
    {
        get { return mammothUpgrade.GetValueByStar(starLevel).propertiesMammothUpgradeData.bonus_Gold_To_All_0 + bonus_Gold_To_All; }
    }

    public float GetBonus_Gold_To_All_1
    {
        get { return mammothUpgrade.GetValueByStar(starLevel).propertiesMammothUpgradeData.bonus_Gold_To_All_1 + bonus_Gold_To_All; }
    }
}
