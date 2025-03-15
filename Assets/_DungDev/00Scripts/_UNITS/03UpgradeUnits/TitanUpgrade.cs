using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UNITS/UpgradeBase/TitanUpgrade")]
public class TitanUpgrade : UpgradeUnitsBase
{
    public List<TitanUpgradeData> lsTitanUpgradeDatas;

    public TitanUpgradeData GetValueByStar(int idStarParam)
    {
        foreach (var child in lsTitanUpgradeDatas)
        {
            if (child.idStar == idStarParam) return child;
        }
        return null;
    }
}

[System.Serializable]
public class TitanUpgradeData
{
    public int idStar;
    public PropertiesTitanUpgradeData propertiesTitanUpgradeData;
}

[System.Serializable]
public class PropertiesTitanUpgradeData
{
    public int bonus_Attack_0;
    public int bonus_Attack_1;
    public int bonus_Speed_To_All;
    public int bonus_Gold_To_All_0;
    public int bonus_Gold_To_All_1;
}
