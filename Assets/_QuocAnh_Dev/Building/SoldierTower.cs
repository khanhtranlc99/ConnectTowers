using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierTower : ArmyTower
{
    public override void CallChangeLevelTower()
    {
        if (lvTowerList.Count > 0)
        {
            switch (level)
            {
                case 0:
                    lvTowerList[1].transform.DOPause();
                    lvTowerList[0].transform.DOPause();
                    lvTowerList[1].transform.DOLocalMoveY(-0.03f, timeChangeLevelTower).SetEase(Ease.OutQuad).OnComplete(() =>
                    {
                        lvTowerList[0].transform.DOLocalMoveY(-0.0365f, timeChangeLevelTower).SetEase(Ease.OutQuad);
                    });
                    break;
                case 1:
                    lvTowerList[1].transform.DOPause();
                    lvTowerList[0].transform.DOPause();
                    if (lvTowerList[1].transform.localPosition.y == 0)
                    {
                        lvTowerList[1].transform.DOLocalMoveY(-0.03f, timeChangeLevelTower).SetEase(Ease.OutQuad).OnComplete(() =>
                        {
                            lvTowerList[0].transform.DOLocalMoveY(0, timeChangeLevelTower).SetEase(Ease.OutQuad);
                        });
                    }
                    else
                    {
                        lvTowerList[0].transform.DOLocalMoveY(0, timeChangeLevelTower).SetEase(Ease.OutQuad);
                    }
                    break;
                default:
                    lvTowerList[1].transform.DOPause();
                    lvTowerList[0].transform.DOPause();
                    lvTowerList[0].transform.DOLocalMoveY(0, timeChangeLevelTower).SetEase(Ease.OutQuad).OnComplete(() =>
                    {
                        lvTowerList[1].transform.DOLocalMoveY(0, timeChangeLevelTower).SetEase(Ease.OutQuad);
                    });
                    break;
            }
        }
    }
}
