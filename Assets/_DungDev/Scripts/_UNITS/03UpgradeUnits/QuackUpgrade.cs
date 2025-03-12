using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UNITS/UpgradeBase/QuackUpgrade")]
public class QuackUpgrade : UpgradeUnitsBase
{
    public List<QuackUpgradeData> lsQuackUpgradeDatas;

    public QuackUpgradeData GetValueByStar(int idStarParam)
    {
        foreach (var child in lsQuackUpgradeDatas)
        {
            if (child.idStar == idStarParam) return child;
        }
        return null;
    }
}

[System.Serializable]
public class QuackUpgradeData
{
    public int idStar;
    public PropertiesQuackUpgradeData propertiesQuackUpgradeData;
}

[System.Serializable]
public class PropertiesQuackUpgradeData
{
    public int bonus_Move_Speed_0;
    public int bonus_Move_Speed_1;
    public int bonus_Attack_To_All;
    public int bonus_Duplicate_To_All;
    public int bonus_Health_To_All;
}
