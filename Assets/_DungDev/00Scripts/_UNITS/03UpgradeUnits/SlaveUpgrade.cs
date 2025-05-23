using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "UNITS/UpgradeBase/ SlaveUpgrade")]

public class SlaveUpgrade : UpgradeUnitsBase
{
    public List<SlaveUpradeData> lsSlaveUpradeDatas;

    public SlaveUpradeData GetValueByStar(int idStarParam)
    {
        foreach (var child in this.lsSlaveUpradeDatas)
        {
            if (child.idStar == idStarParam) return child;
        }
        return null;
    }

}

[System.Serializable]
public class SlaveUpradeData
{
    public int idStar;
    public PropertiesSlaveUpgradeData propertiesUpgradeDatas;

}

[System.Serializable]
public class PropertiesSlaveUpgradeData
{
    public int bonus_Move_Speed_0;
    public int bonus_Move_Speed_1;
    public int bonus_Speed_To_All;
    public int bonus_Gold_To_All_0;
    public int bonus_Gold_To_All_1;
}
