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
        tmp.y = targetTow.transform.position.y - 0.6f;
        GameObject g = SimplePool2.Spawn(freezePrefab);
        g.transform.position = tmp;
        g.transform.rotation = Quaternion.Euler(60, 0, 0);
        g.transform.localScale = Vector3.zero;
        g.SetActive(true);
        // handle vfx
        g.transform.DOScale(0.8f, 0.5f).SetEase(Ease.OutBack);
        g.transform.DOMoveY(targetTow.transform.position.y+1.33f, 1.5f).SetEase(Ease.Linear).OnComplete(() =>
        {
            if (!g.activeSelf)
            {
                return;
            }
            targetTow.isStun = true;
            curIdx = 0;
            enemyCount = 0;
            StartCoroutine(DespawnAfterDelay(7f, g, targetTow));
        });
    }
    private IEnumerator DespawnAfterDelay(float delay, GameObject g, BuildingContain target)
    {
        float t = 0f;
        while (t < delay)
        {
            if (GamePlayController.Instance.isPlay)
                t += Time.deltaTime;
            yield return null;
        }

        if (g.activeSelf)
        {
            target.isStun = false;
            SimplePool2.Despawn(g);
        }
    }

}
