using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "UpgradeBase/ GuardUpgrade")]

public class GuardUpgrade : UpgradeBase
{
    public List<GuardUpradeData> lsGuardUpradeDatas;
    public GuardUpradeData GetValueByStar(int idStarParam)
    {
        foreach (var child in this.lsGuardUpradeDatas)
        {
            if (child.idStar == idStarParam) return child;
        }
        return null;
    }
}

[System.Serializable]
public class GuardUpradeData
{
    public int idStar;
    public PropertiesGuardUpgradeData propertiesGuardUpgradeDatas;

}

[System.Serializable]
public class PropertiesGuardUpgradeData
{
    public int gain_Shield_0;
    public int bonus_Move_Speed_0;
    public int gain_Shield_1;
    public int bonus_Move_Speed_1;
    public int gain_Shield_2;
}
