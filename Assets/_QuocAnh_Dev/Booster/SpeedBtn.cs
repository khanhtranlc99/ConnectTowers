using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventDispatcher;

public class SpeedBtn : BoosterButton
{
    public override void Init()
    {
        base.Init();
        this.RegisterListener(EventID.CHANGE_SPEED_BOOSTER, delegate
        {
            UpdateUI();
        });
    }
    public override void UpdateUI()
    {
        turn = UseProfile.SpeedUp_Booster;
        if (turn == 0)
        {
            turnObj.SetActive(false);
            plusObj.SetActive(true);
        }
        else
        {
            turnObj.SetActive(true);
            plusObj.SetActive(false);
            turnIdx.text = turn.ToString();
        }
    }
    public void OnDisable()
    {
        this.RemoveListener(EventID.CHANGE_SPEED_BOOSTER, delegate
        {
            UpdateUI();
        });
    }
    public void OnDestroy()
    {
        this.RemoveListener(EventID.CHANGE_SPEED_BOOSTER, delegate
        {
            UpdateUI();
        });
    }
}