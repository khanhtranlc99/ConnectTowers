using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UNITS/UpgradeBase/ExecutionerUpgrade")]
public class ExecutionerUpgrade : UpgradeUnitsBase
{
    public List<ExecutionerUpgradeData> lsExecutionerUpgradeDatas;

    public ExecutionerUpgradeData GetValueByStar(int idStarParam)
    {
        foreach (var child in lsExecutionerUpgradeDatas)
        {
            if (child.idStar == idStarParam) return child;
        }
        return null;
    }
}

[System.Serializable]
public class ExecutionerUpgradeData
{
    public int idStar;
    public PropertiesExecutionerUpgradeData propertiesExecutionerUpgradeData;
}

[System.Serializable]
public class PropertiesExecutionerUpgradeData
{
    public int heavy_Hit_0;
    public int heavy_Hit_1;
    public int bonus_Attack;
    public int bonus_Duplicate_To_All;
    public int bonus_Speed_To_All;
}
