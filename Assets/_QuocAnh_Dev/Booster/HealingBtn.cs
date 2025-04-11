using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventDispatcher;

public class HealingBtn : BoosterButton
{
    public override void Init()
    {
        base.Init();
        this.RegisterListener(EventID.CHANGE_HEALING_BOOSTER, delegate
        {
            UpdateUI();
        });
    }
    public override void UpdateUI()
    {
        turn = UseProfile.HealingUp_Booster;
        base.UpdateUI();
    }
    public override void CheckIdx()
    {
        base.CheckIdx();
        if (turn == 0)
        {
            PurchaseBooster.Setup(GiftType.HealingUp_Booster).Show();
        }
        else
        {
            GamePlayController.Instance.playerContain.boosterCtrl.ActiveBooster(boosterType);
        }
    }
    public void OnDisable()
    {
        this.RemoveListener(EventID.CHANGE_HEALING_BOOSTER, delegate
        {
            UpdateUI();
        });
    }
    public void OnDestroy()
    {
        this.RemoveListener(EventID.CHANGE_HEALING_BOOSTER, delegate
        {
            UpdateUI();
        });
    }

}
