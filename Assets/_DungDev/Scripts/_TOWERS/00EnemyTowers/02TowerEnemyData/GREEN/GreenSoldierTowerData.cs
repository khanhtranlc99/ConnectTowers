using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "TOWERS/PropertiesTowersBase/ 01GreenSoldierTowerData", order = 1)]

public class GreenSoldierTowerData : PropertiesTowersBase
{
    public GreenSoldierTowerUpgrade greenSoldierTowerUpgrade;

    public int GetCurrentLevel
    {
        get { return greenSoldierTowerUpgrade.GetUpgradeDataByLevel(currentLevel).level; }
    }
    public GameObject GetModel
    {
        get { return greenSoldierTowerUpgrade.GetUpgradeDataByLevel(currentLevel).propertiesTowerUpgradeData.model; }
    }

    public int GetMaxConnect
    {
        get { return greenSoldierTowerUpgrade.GetUpgradeDataByLevel(currentLevel).propertiesTowerUpgradeData.maxConnect; }
    }

    public int GetEntryThreshHold
    {
        get { return greenSoldierTowerUpgrade.GetUpgradeDataByLevel(currentLevel).propertiesTowerUpgradeData.upgradeThreshold; }
    }
}
