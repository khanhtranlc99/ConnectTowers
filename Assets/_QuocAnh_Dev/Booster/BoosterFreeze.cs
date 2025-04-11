using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterFreeze : BoosterBase
{
    [SerializeField] private GameObject freezePrefab;
    private float interval = 10f;

    private float curTime = 0f;
    private int enemyCount = 0;
    private int curIdx = 0;
    private int randomIdx;
    public override void OnActive()
    {
        timer = cooldown;
        UseProfile.Freeze_Booster--;
        StartCoroutine(SpawnFreeeze());
    }

    private IEnumerator SpawnFreeeze()
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
        BuildingContain targetTow = null;
        foreach (var item in GamePlayController.Instance.playerContain.buildingCtrl.towerList)
        {
            if (item.teamId > 0 && item.Hp > 0)
            {
                enemyCount++;
            }
        }
        if (enemyCount > 0)
        {
            randomIdx = UnityEngine.Random.Range(0, enemyCount);
            foreach (var item in GamePlayController.Instance.playerContain.buildingCtrl.towerList)
            {
                if (item.teamId > 0 && item.Hp > 0)
                {
                    if (curIdx == randomIdx)
                    {
                        targetTow = item;
                        break;
                    }
                    curIdx++;
                }
            }
        }
        Vector3 tmp = targetTow.transform.position;
        GameObject g = SimplePool2.Spawn(freezePrefab);
        g.transform.position = tmp;
        g.SetActive(true);
        // handle vfx
        //DOScale.
        g.transform.DOMoveY(targetTow.transform.position.y, 1.5f).SetEase(Ease.Linear).OnComplete(() =>
        {
            if (!g.activeSelf)
            {
                return;
            }
            curIdx = 0;
            enemyCount = 0;
            SimplePool2.Despawn(g);
        });
    }
}
