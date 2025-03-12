using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UpgradeTowerBase/ SoldierTowerUpgrade")]

public class SoldierTowerUpgrade : UpgradeTowerBase
{
    public List<TowerUpgradeData> lsTowerUpgradeDatas;

    public TowerUpgradeData GetUpgradeDataByLevel(int levelParam)
    {
        foreach (var child in this.lsTowerUpgradeDatas)
        {
            if (child.level == levelParam) return child;
        }
        return null;
    }
}


