using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterSpeed : BoosterBase
{
    public override void OnActive()
    {
        timer = cooldown;
        StartCoroutine(ActiveSpawn());
    }

    private IEnumerator ActiveSpawn()
    {
        ActiveBuff(true);
        yield return new WaitForSeconds(duration);
        ActiveBuff(false);
        isActive = false;
    }

    private void ActiveBuff(bool v)
    {
        GamePlayController.Instance.playerContain.buildingCtrl.isSpeedBuff = v;
    }
}
