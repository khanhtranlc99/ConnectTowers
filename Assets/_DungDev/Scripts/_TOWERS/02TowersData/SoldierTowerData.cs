using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "PropertiesTowersBase/ SoldierData")]

public class SoldierTowerData : PropertiesTowersBase
{
    public SoldierTowerUpgrade soldierTowerUpgrade;

    public int GetCurrentLevel
    {
        get { return soldierTowerUpgrade.GetUpgradeDataByLevel(currentLevel).level; }
    }
    public GameObject GetModel
    {
        get { return soldierTowerUpgrade.GetUpgradeDataByLevel(currentLevel).propertiesTowerUpgradeData.model; }
    }

    public int GetMaxConnect
    {
        get { return soldierTowerUpgrade.GetUpgradeDataByLevel(currentLevel).propertiesTowerUpgradeData.maxConnect; }
    }

    public int GetEntryThreshHold
    {
        get { return soldierTowerUpgrade.GetUpgradeDataByLevel(currentLevel).propertiesTowerUpgradeData.upgradeThreshold; }
    }
    
}
