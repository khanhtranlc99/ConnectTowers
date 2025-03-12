using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "TOWERS/PropertiesTowersBase/ 01GreenMageTowerData", order = 1)]

public class GreenMageTowerData : PropertiesTowersBase
{
    public GreenMageTowerUpgrade greenMageTowerUpgrade;

    public int GetCurrentLevel
    {
        get { return greenMageTowerUpgrade.GetUpgradeDataByLevel(currentLevel).level; }
    }
    public GameObject GetModel
    {
        get { return greenMageTowerUpgrade.GetUpgradeDataByLevel(currentLevel).propertiesTowerUpgradeData.model; }
    }

    public int GetMaxConnect
    {
        get { return greenMageTowerUpgrade.GetUpgradeDataByLevel(currentLevel).propertiesTowerUpgradeData.maxConnect; }
    }

    public int GetEntryThreshHold
    {
        get { return greenMageTowerUpgrade.GetUpgradeDataByLevel(currentLevel).propertiesTowerUpgradeData.upgradeThreshold; }
    }
}
