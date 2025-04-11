using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterArrowRain : BoosterBase
{
    [SerializeField] private GameObject meteorPrefab;
    private int dame = 5;
    private float interval = 2f;

    private float curTime = 0f;
    private int enemyCount = 0;
    private int curIdx = 0;
    private int randomIdx1;
    private int randomIdx2;
    public override void OnActive()
    {
        timer = cooldown;
        UseProfile.ArrowRain_Booster--;
        StartCoroutine(SpawnArrow());
    }

    private IEnumerator SpawnArrow()
    {
        while (curTime < duration)
        {
            ActiveSkill();
            yield return new WaitForSeconds(interval);
            curTime += interval;
        }
    }

    private void ActiveSkill()
    {
        BuildingContain targetTow1 = null;
        BuildingContain targetTow2 = null;
        foreach (var item in GamePlayController.Instance.playerContain.buildingCtrl.towerList)
        {
            if (item.teamId > 0 && item.Hp > 0)
            {
                enemyCount++;
            }
        }
        if (enemyCount > 0)
        {
            randomIdx1 = UnityEngine.Random.Range(0, enemyCount);
            do
            {
                randomIdx2 = UnityEngine.Random.Range(0, enemyCount);
            }while(randomIdx1 == randomIdx2);

            foreach (var item in GamePlayController.Instance.playerContain.buildingCtrl.towerList)
            {
                if (item.teamId > 0 && item.Hp > 0)
                {
                    if (curIdx == randomIdx1)
                    {
                        targetTow1 = item;
                    }
                    else if (curIdx == randomIdx2) targetTow2 = item;
                    curIdx++;
                }
            }
        }
        Vector3 tmp1 = targetTow1.transform.position;
        tmp1.y = targetTow1.transform.position.y + 50;
        GameObject g1 = SimplePool2.Spawn(meteorPrefab);
        g1.transform.position = tmp1;
        g1.SetActive(true);
        // handle vfx
        g1.transform.DOMoveY(targetTow1.transform.position.y, 1.5f).SetEase(Ease.Linear).OnComplete(() =>
        {
            if (!g1.activeSelf)
            {
                return;
            }
            targetTow1.Hp -= dame;
            if (targetTow1.Hp <= 0) targetTow1.Hp = 0;
            SimplePool.Despawn(g1);
        });
        Vector3 tmp2 = targetTow2.transform.position;
        tmp2.y = targetTow2.transform.position.y + 50;
        GameObject g2 = SimplePool2.Spawn(meteorPrefab);
        g2.transform.position = tmp2;
        g2.SetActive(true);
        // handle vfx
        g2.transform.DOMoveY(targetTow2.transform.position.y, 1.5f).SetEase(Ease.Linear).OnComplete(() =>
        {
            if (!g2.activeSelf)
            {
                return;
            }
            curIdx = 0;
            enemyCount = 0;
            targetTow2.Hp -= dame;
            if (targetTow2.Hp <= 0) targetTow2.Hp = 0;
            SimplePool.Despawn(g2);
        });
    }
}
