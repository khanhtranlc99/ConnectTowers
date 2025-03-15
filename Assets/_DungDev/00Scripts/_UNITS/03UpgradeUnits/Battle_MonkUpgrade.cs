using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UNITS/UpgradeBase/Battle_MonkUpgrade")]
public class Battle_MonkUpgrade : UpgradeUnitsBase
{
    public List<BattleMonkUpgradeData> lsBattleMonkUpgradeDatas;

    public BattleMonkUpgradeData GetValueByStar(int idStarParam)
    {
        foreach (var child in lsBattleMonkUpgradeDatas)
        {
            if (child.idStar == idStarParam) return child;
        }
        return null;
    }
}

[System.Serializable]
public class BattleMonkUpgradeData
{
    public int idStar;
    public PropertiesBattleMonkUpgradeData propertiesBattleMonkUpgradeData;
}

[System.Serializable]
public class PropertiesBattleMonkUpgradeData
{
    public int counter_Hit;
    public int bonus_Move_Speed_0;
    public int gain_Shield;
    public int bonus_Move_Speed_1;
    public int bonus_Attack_To_All;
}
