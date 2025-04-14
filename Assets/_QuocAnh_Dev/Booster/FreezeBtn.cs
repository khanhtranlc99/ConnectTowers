using EventDispatcher;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeBtn : BoosterButton
{
    public override void Init()
    {
        base.Init();
        this.RegisterListener(EventID.CHANGE_FREEZE_BOOSTER, delegate
        {
            UpdateUI();
        });
    }
    public override void UpdateUI()
    {
        turn = UseProfile.Freeze_Booster;
        base.UpdateUI();
    }
    public override void CheckIdx()
    {
        base.CheckIdx();
        if (turn == 0)
        {
            GamePlayController.Instance.isPlay = false;
            PurchaseBooster.Setup(GiftType.Freeze_Booster).Show();
        }
        else
        {
            isActive = true;
            GamePlayController.Instance.playerContain.boosterCtrl.ActiveBooster(boosterType);
        }
    }
    public void OnDisable()
    {
        this.RemoveListener(EventID.CHANGE_FREEZE_BOOSTER, delegate
        {
            UpdateUI();
        });
    }

    public void OnDestroy()
    {
        this.RemoveListener(EventID.CHANGE_FREEZE_BOOSTER, delegate
        {
            UpdateUI();
        });
    }

}
