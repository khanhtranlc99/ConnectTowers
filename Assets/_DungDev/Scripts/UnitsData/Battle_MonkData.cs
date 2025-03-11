using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PropertiesBase/Battle_MonkData")]
public class Battle_MonkData : PropertiesBase
{
    public float increase_Speed;
    public float increase_Attack;
    public float dupllice;

    // Các biến rút gọn
    public int bonus_Move_Speed;
    public int bonus_Attack_To_All;
    public int gain_Shield;

    public Battle_MonkUpgrade battle_MonkUpgrade;

    // Getter cho các giá trị tính toán
    public float GetIncrease_Speed
    {
        get { return increase_Speed * 0.5f * (float)currentLevel; }
    }

    public float GetIncrease_Attack
    {
        get { return increase_Attack * 0.5f * (float)currentLevel; }
    }

    public float GetDupllice
    {
        get { return dupllice * 0.2f * (float)currentLevel; }
    }

    public float GetCounter_Hit
    {
        get { return battle_MonkUpgrade.GetValueByStar(starLevel).propertiesBattleMonkUpgradeData.counter_Hit; }
    }

    public float GetBonus_Move_Speed_0
    {
        get { return battle_MonkUpgrade.GetValueByStar(starLevel).propertiesBattleMonkUpgradeData.bonus_Move_Speed_0 + bonus_Move_Speed; }
    }

    public float GetBonus_Move_Speed_1
    {
        get { return battle_MonkUpgrade.GetValueByStar(starLevel).propertiesBattleMonkUpgradeData.bonus_Move_Speed_1 + bonus_Move_Speed; }
    }

    public float GetGain_Shield
    {
        get { return battle_MonkUpgrade.GetValueByStar(starLevel).propertiesBattleMonkUpgradeData.gain_Shield + gain_Shield; }
    }

    public float GetBonus_Attack_To_All
    {
        get { return battle_MonkUpgrade.GetValueByStar(starLevel).propertiesBattleMonkUpgradeData.bonus_Attack_To_All + bonus_Attack_To_All; }
    }
}
