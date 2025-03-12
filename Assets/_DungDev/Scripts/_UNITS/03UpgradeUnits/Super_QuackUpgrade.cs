using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UpgradeBase/Super_QuackUpgrade")]
public class Super_QuackUpgrade : UpgradeBase
{
    public List<SuperQuackUpgradeData> lsSuperQuackUpgradeDatas;

    public SuperQuackUpgradeData GetValueByStar(int idStarParam)
    {
        foreach (var child in lsSuperQuackUpgradeDatas)
        {
            if (child.idStar == idStarParam) return child;
        }
        return null;
    }
}

[System.Serializable]
public class SuperQuackUpgradeData
{
    public int idStar;
    public PropertiesSuperQuackUpgradeData propertiesSuperQuackUpgradeData;
}

[System.Serializable]
public class PropertiesSuperQuackUpgradeData
{
    public int double_Shield_0;
    public int double_Shield_1;
    public int bonus_Attack;
    public int dash;
    public int critical_Hit;
}
