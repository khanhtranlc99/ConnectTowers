using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UNITS/PropertiesBase/ SlaveData")]

public class SlaveData : PropertiesUnitsBase
{
    // skill right
    [Space(10)]
    [Header("SlaveData")]
    [SerializeField] float increase_Speed;

    // skill start left
    [SerializeField] int bonus_Move_Speed;
    [SerializeField] int bonus_Speed_To_All;
    [SerializeField] int bonus_Gold_To_All;
    [SerializeField] SlaveUpgrade slaveUpgrade;

    public override float GetSkillValue(string name)
    {
        if (name == "Increase Speed")
            return GetInCrease_Speed;
        return base.GetSkillValue(name);
    }


    public float GetInCrease_Speed
    {
        get { return increase_Speed + 0.2f * (float)currentLevel; }
    }

    public float GetBonus_Move_Speed_0
    {
        get { return slaveUpgrade.GetValueByStar(starLevel).propertiesUpgradeDatas.bonus_Move_Speed_0 + bonus_Move_Speed; }
    }
    public float GetBonus_Move_Speed_1
    {
        get { return slaveUpgrade.GetValueByStar(starLevel).propertiesUpgradeDatas.bonus_Move_Speed_1 + bonus_Move_Speed; }
    }

    public float GetBonus_Speed_To_All
    {
        get { return slaveUpgrade.GetValueByStar(starLevel).propertiesUpgradeDatas.bonus_Speed_To_All + bonus_Speed_To_All; }
    }
    public float GetBonus_Gold_To_All_0
    {
        get { return slaveUpgrade.GetValueByStar(starLevel).propertiesUpgradeDatas.bonus_Gold_To_All_0 + bonus_Gold_To_All; }
    }
    public float GetBonus_Gold_To_All_1
    {
        get { return slaveUpgrade.GetValueByStar(starLevel).propertiesUpgradeDatas.bonus_Gold_To_All_1 + bonus_Gold_To_All; }
    }

}
