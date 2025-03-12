using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UNITS/UpgradeBase/Wounded_MammothUpgrade")]
public class Wounded_MammothUpgrade : UpgradeUnitsBase
{
    public List<WoundedMammothUpgradeData> lsWoundedMammothUpgradeDatas;

    public WoundedMammothUpgradeData GetValueByStar(int idStarParam)
    {
        foreach (var child in lsWoundedMammothUpgradeDatas)
        {
            if (child.idStar == idStarParam) return child;
        }
        return null;
    }
}

[System.Serializable]
public class WoundedMammothUpgradeData
{
    public int idStar;
    public PropertiesWoundedMammothUpgradeData propertiesWoundedMammothUpgradeData;
}

[System.Serializable]
public class PropertiesWoundedMammothUpgradeData
{
    public int gain_Shield_0;
    public int gain_Shield_1;
    public int gain_Shield_2;
    public int bonus_Move_Speed_0;
    public int bonus_Move_Speed_1;
}
