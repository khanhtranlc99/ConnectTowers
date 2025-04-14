using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterSpeed : BoosterBase
{
    private float curTime = 0f;
    public override void OnActive()
    {
        timer = cooldown;
        UseProfile.Speed_Booster--;
        StartCoroutine(ActiveSpawn());
    }

    private IEnumerator ActiveSpawn()
    {
        ActiveBuff(true);
        while (curTime < duration)
        {
            if (GamePlayController.Instance.isPlay)
            {
                curTime += Time.deltaTime;
            }
            else
            {
                yield return null;
            }
            yield return null;
        }
        ActiveBuff(false);
    }

    private void ActiveBuff(bool v)
    {
        GamePlayController.Instance.playerContain.buildingCtrl.isSpeedBuff = v;
    }
}
