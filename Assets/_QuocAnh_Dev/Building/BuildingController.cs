using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    public List<BuildingContain> towerList;
    public List<ArmyTower> armyTowerList;

    public void Init()
    {
        towerList = towerList.OrderByDescending(tower => tower is ArmyTower).ToList();
        for(int i=0;i<towerList.Count;i++)
        {
            towerList[i].id = i;
            towerList[i].InitTower();
            if (towerList[i] is ArmyTower _army)
            {
                armyTowerList.Add(_army);
            }
        }
    }
}

public enum BuildingType
{
    SoldierTower = 0,
    TankTower = 1,
    MageTower = 2,
    ArrowTower = 3,
    CannonTower = 4,
}
