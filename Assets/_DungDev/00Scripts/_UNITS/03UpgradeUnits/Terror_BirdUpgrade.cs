using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UNITS/UpgradeBase/Terror_BirdUpgrade")]
public class Terror_BirdUpgrade : UpgradeUnitsBase
{
    public List<TerrorBirdUpgradeData> lsTerrorBirdUpgradeDatas;

    public TerrorBirdUpgradeData GetValueByStar(int idStarParam)
    {
        foreach (var child in lsTerrorBirdUpgradeDatas)
        {
            if (child.idStar == idStarParam) return child;
        }
        return null;
    }
}

[System.Serializable]
public class TerrorBirdUpgradeData
{
    public int idStar;
    public PropertiesTerrorBirdUpgradeData propertiesTerrorBirdUpgradeData;
}

[System.Serializable]
public class PropertiesTerrorBirdUpgradeData
{
    public int bonus_Attack_0;
    public int bonus_Attack_1;
    public int bonus_Move_Speed_0;
    public int bonus_Move_Speed_1;
    public int carnivorous;
}
