using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UNITS/UpgradeBase/ApprenticeUpgrade")]
public class ApprenticeUpgrade : UpgradeUnitsBase
{
    public List<ApprenticeUpgradeData> lsApprenticeUpgradeDatas;

    public ApprenticeUpgradeData GetValueByStar(int idStarParam)
    {
        foreach (var child in lsApprenticeUpgradeDatas)
        {
            if (child.idStar == idStarParam) return child;
        }
        return null;
    }
}

[System.Serializable]
public class ApprenticeUpgradeData
{
    public int idStar;
    public PropertiesApprenticeUpgradeData propertiesApprenticeUpgradeData;
}

[System.Serializable]
public class PropertiesApprenticeUpgradeData
{
    public int bonus_Move_Speed_0;
    public int bonus_Move_Speed_1;
    public int bonus_Speed_To_All;
    public int bonus_Gold_To_All_0;
    public int bonus_Gold_To_All_1;
}
