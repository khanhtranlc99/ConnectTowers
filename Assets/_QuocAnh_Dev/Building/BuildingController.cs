using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    public List<BuildingContain> towerList;
    public List<ArmyTower> armyTowerList;
    [HideInInspector] public List<ArmyTower> allyTower;
    public bool isSpawnBuff = false;
    public bool isSpeedBuff = false;

    public void Init()
    {
        towerList = towerList.OrderByDescending(tower => tower is ArmyTower).ToList();
        for(int i=0;i<towerList.Count;i++)
        {
            towerList[i].id = i;
            towerList[i].InitTower();
            if (towerList[i] is ArmyTower _army)
            {
                if(towerList[i].teamId == 0)
                {
                    allyTower.Add(_army);
                }
                armyTowerList.Add(_army);
            }
        }
    }
    private void Update()
    {
        if (GamePlayController.Instance.isPlay)
        {
            for(int i= 0; i < allyTower.Count; i++)
            {
                allyTower[i].spawnBuff = isSpawnBuff ? 0.5f : 1f;
                allyTower[i].isSpeedBuff = this.isSpeedBuff ? true:false;
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
    GoldPack=5
}
