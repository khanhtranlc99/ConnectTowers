using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTowerBase : ScriptableObject
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

[System.Serializable]
public class TowerUpgradeData
{
    public int level;
    public PropertiesTowerUpgradeData propertiesTowerUpgradeData;

}
[System.Serializable]
public class PropertiesTowerUpgradeData
{
    public GameObject model;
    public int upgradeThreshold;
    public int maxConnect;
}