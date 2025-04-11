using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventDispatcher;

public class ArrowBtn : BoosterButton
{
    public override void Init()
    {
        base.Init();
        this.RegisterListener(EventID.CHANGE_ARROWRAIN_BOOSTER, delegate
        {
            UpdateUI();
        });
    }
    public override void UpdateUI()
    {
        turn = UseProfile.ArrowRain_Booster;
        base.UpdateUI();
    }
    public override void CheckIdx()
    {
        base.CheckIdx();
        if(turn == 0)
        {
            PurchaseBooster.Setup(GiftType.ArrowRain_Booster).Show();
        }
        else
        {
            GamePlayController.Instance.playerContain.boosterCtrl.ActiveBooster(boosterType);
        }
    }
    public void OnDisable()
    {
        this.RemoveListener(EventID.CHANGE_ARROWRAIN_BOOSTER, delegate
        {
            UpdateUI();
        });
    }
    public void OnDestroy()
    {
        this.RemoveListener(EventID.CHANGE_ARROWRAIN_BOOSTER, delegate
        {
            UpdateUI();
        });
    }
}
