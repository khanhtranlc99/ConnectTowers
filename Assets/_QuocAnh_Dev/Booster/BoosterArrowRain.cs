using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterArrowRain : BoosterBase
{
    [SerializeField] private GameObject meteorPrefab;
    private int dame = 5;
    private float interval = 6f;
    [SerializeField] private Transform spawnPos1;
    [SerializeField] private Transform spawnPos2;

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
            if (GamePlayController.Instance.isPlay)
            {
                ActiveSkill();
                float waitTime = 0f;
                while (waitTime < interval)
                {
                    if (GamePlayController.Instance.isPlay)
                        waitTime += Time.deltaTime;
                    yield return null;
                }
                curTime += interval;
            }
        }
    }

    private void ActiveSkill()
    {
        BuildingContain targetTow1 = null;
        BuildingContain targetTow2 = null;
        curIdx = 0;
        enemyCount = 0;
        List<BuildingContain> validTower = new List<BuildingContain>();
        foreach (var item in GamePlayController.Instance.playerContain.buildingCtrl.towerList)
        {
            if (item.teamId > 0 && item.Hp > 0 && item!= null)
            {
                validTower.Add(item);
                enemyCount++;
            }
        }
        if (enemyCount >= 2)
        {
            randomIdx1 = UnityEngine.Random.Range(0, enemyCount);
            do
            {
                randomIdx2 = UnityEngine.Random.Range(0, enemyCount);
            } while (randomIdx1 == randomIdx2);

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
        else if(enemyCount == 1)
        {
            targetTow1 = validTower[0];
            targetTow2 = null;
        }
        else return;
        SpawnArrow(spawnPos1, targetTow1);
        SpawnArrow(spawnPos2, targetTow2);
    }
    private void SpawnArrow(Transform spawnPos, BuildingContain target)
    {
        if (target == null || target.Hp <= 0)
            return;
        Vector3 tmp1 = target.transform.position;
        GameObject g1 = SimplePool2.Spawn(meteorPrefab);
        g1.transform.position = spawnPos.position;
        g1.SetActive(true);
        // handle vfx
        g1.transform.DOMove(target.transform.position, 1.5f).SetEase(Ease.Linear).OnComplete(() =>
        {
            if (!g1.activeSelf || target == null)
                return;

            target.Hp -= dame;
            if (target.Hp <= 0) target.Hp = 0;

            SimplePool2.Despawn(g1);
        });
    }
}
    