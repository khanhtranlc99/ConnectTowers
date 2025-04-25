using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankTower : ArmyTower
{
    public override void CallChangeLevelTower()
    {
        base.UpdateTower();
        if (lvTowerList.Count > 0 && level != -1)
        {
            int oldLevel = 0;
            for (int i = 0; i < lvTowerList.Count; i++)
            {
                if (lvTowerList[i].activeSelf)
                {
                    oldLevel = i;
                }
                lvTowerList[i].SetActive(false);
            }
            lvTowerList[level].SetActive(true);
        }
    }
}
