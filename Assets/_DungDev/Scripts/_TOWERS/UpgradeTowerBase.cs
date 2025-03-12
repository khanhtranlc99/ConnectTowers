using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTowerBase : ScriptableObject
{
    
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