using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterHealing : BoosterBase
{
    // vfx cac kieu
    private float interval = 8f;

    private float curTime = 0f;
    public override void OnActive()
    {
        timer = cooldown;
        UseProfile.Healing_Booster--;
        StartCoroutine(SpawnHealing());
    }

    private IEnumerator SpawnHealing()
    {
        while (curTime < duration)
        {
            if (GamePlayController.Instance.isPlay)
            {
                ActiveBuff();
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
    public void ActiveBuff()
    {
        foreach (var item in GamePlayController.Instance.playerContain.buildingCtrl.towerList)
        {
            if (item.teamId == 0)
            {
                item.Hp += 6;
            }
        }
    }
}
