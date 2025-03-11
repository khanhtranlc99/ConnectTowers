using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PropertiesBase/Wounded_MammothData")]
public class Wounded_MammothData : PropertiesBase
{
    public float increase_Attack;

    // Biến rút gọn
    public int gain_Shield;
    public int bonus_Move_Speed;

    public Wounded_MammothUpgrade wounded_MammothUpgrade;

    // Getter cho các giá trị tính toán
    public float GetIncrease_Attack
    {
        get { return increase_Attack * 0.3f * (float)currentLevel; }
    }

    public float GetGain_Shield_0
    {
        get { return wounded_MammothUpgrade.GetValueByStar(starLevel).propertiesWoundedMammothUpgradeData.gain_Shield_0 + gain_Shield; }
    }

    public float GetGain_Shield_1
    {
        get { return wounded_MammothUpgrade.GetValueByStar(starLevel).propertiesWoundedMammothUpgradeData.gain_Shield_1 + gain_Shield; }
    }

    public float GetGain_Shield_2
    {
        get { return wounded_MammothUpgrade.GetValueByStar(starLevel).propertiesWoundedMammothUpgradeData.gain_Shield_2 + gain_Shield; }
    }

    public float GetBonus_Move_Speed_0
    {
        get { return wounded_MammothUpgrade.GetValueByStar(starLevel).propertiesWoundedMammothUpgradeData.bonus_Move_Speed_0 + bonus_Move_Speed; }
    }

    public float GetBonus_Move_Speed_1
    {
        get { return wounded_MammothUpgrade.GetValueByStar(starLevel).propertiesWoundedMammothUpgradeData.bonus_Move_Speed_1 + bonus_Move_Speed; }
    }
}
