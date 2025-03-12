using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "TOWERS/PropertiesTowersBase/ 02RedSoldierTowerData", order = 2)]

public class RedSoldierTowerData : PropertiesTowersBase
{
    public RedSoldierTowerUpgrade redSoldierTowerUpgrade;

    public int GetCurrentLevel
    {
        get { return redSoldierTowerUpgrade.GetUpgradeDataByLevel(currentLevel).level; }
    }
    public GameObject GetModel
    {
        get { return redSoldierTowerUpgrade.GetUpgradeDataByLevel(currentLevel).propertiesTowerUpgradeData.model; }
    }

    public int GetMaxConnect
    {
        get { return redSoldierTowerUpgrade.GetUpgradeDataByLevel(currentLevel).propertiesTowerUpgradeData.maxConnect; }
    }

    public int GetEntryThreshHold
    {
        get { return redSoldierTowerUpgrade.GetUpgradeDataByLevel(currentLevel).propertiesTowerUpgradeData.upgradeThreshold; }
    }
}
