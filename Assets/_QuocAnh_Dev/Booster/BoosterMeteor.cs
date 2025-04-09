using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterMeteor : BoosterBase
{
    [SerializeField] private GameObject meteorPrefab;
    private int dame = 15;
    private float interval = 10f;

    private float curTime = 0f;
    private int enemyCount = 0;
    private int curIdx = 0;
    private int randomIdx;

    public override void OnActive()
    {
        timer = cooldown;
        UseProfile.Meteor_Booster--;
        StartCoroutine(SpawnMeteor());
    }
    private IEnumerator SpawnMeteor()
    {
        while (curTime<duration)
        {
            ActiveSkill();
            yield return new WaitForSeconds(interval);
            curTime += interval;
        }
    }

    private void ActiveSkill()
    {
        BuildingContain targetTow = null;
        foreach(var item in GamePlayController.Instance.playerContain.buildingCtrl.towerList)
        {
            if (item.teamId > 0 && item.Hp > 0)
            {
                enemyCount++;
            }
        }
        if(enemyCount > 0)
        {
            randomIdx = UnityEngine.Random.Range(0, enemyCount);
            foreach (var item in GamePlayController.Instance.playerContain.buildingCtrl.towerList)
            {
                if (item.teamId > 0 && item.Hp > 0)
                {
                    if(curIdx == randomIdx)
                    {
                        targetTow = item;
                        break;
                    }
                    curIdx++;
                }
            }
        }
        Vector3 tmp = targetTow.transform.position;
        tmp.y = targetTow.transform.position.y + 50;
        GameObject g = Instantiate(meteorPrefab);
        g.transform.position = tmp;
        g.SetActive(true);
        // handle vfx
        g.transform.DOMoveY(targetTow.transform.position.y, 1.5f).SetEase(Ease.Linear).OnComplete(() =>
        {
            if (!g.activeSelf)
            {
                return;
            }
            curIdx = 0;
            enemyCount = 0;
            targetTow.Hp -= dame;
            if(targetTow.Hp <= 0) targetTow.Hp = 0;
            Destroy(g);
        });
    }
}
