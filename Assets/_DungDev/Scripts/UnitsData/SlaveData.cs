using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PropertiesBase/ SlaveData")]

public class SlaveData : PropertiesBase
{
    //TODO: note sua, da xong
    [SerializeField] float increase_Speed;
    [SerializeField] int bonus_Move_Speed;
    [SerializeField] int bonus_Speed_To_All;
    [SerializeField] int bonus_Gold_To_All;
    [SerializeField] SlaveUpgrade slaveUpgrade;


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
