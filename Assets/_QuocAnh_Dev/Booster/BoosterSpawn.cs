using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterSpawn : BoosterBase
{
    public override void OnActive()
    {
        timer = cooldown;
        UseProfile.SpawnsUp_Booster--;
        StartCoroutine(ActiveSpawn());
    }

    private IEnumerator ActiveSpawn()
    {
        ActiveBuff(true);
        yield return new WaitForSeconds(duration);
        ActiveBuff(false);
    }

    private void ActiveBuff(bool v)
    {
        GamePlayController.Instance.playerContain.buildingCtrl.isSpawnBuff = v;
    }
}
