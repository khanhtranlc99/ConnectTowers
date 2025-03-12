using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "TOWERS/PropertiesTowersBase/ BeastTowerData", order = 0)]

public class BeastTowerData : PropertiesTowersBase
{
    public BeastTowerUpgrade beastTowerUpgrade;

    public int GetCurrentLevel
    {
        get { return beastTowerUpgrade.GetUpgradeDataByLevel(currentLevel).level; }
    }
    public GameObject GetModel
    {
        get { return beastTowerUpgrade.GetUpgradeDataByLevel(currentLevel).propertiesTowerUpgradeData.model; }
    }

    public int GetMaxConnect
    {
        get { return beastTowerUpgrade.GetUpgradeDataByLevel(currentLevel).propertiesTowerUpgradeData.maxConnect; }
    }

    public int GetEntryThreshHold
    {
        get { return beastTowerUpgrade.GetUpgradeDataByLevel(currentLevel).propertiesTowerUpgradeData.upgradeThreshold; }
    }
}
