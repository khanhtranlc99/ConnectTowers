using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnRewardAdsHandle : BtnUpgradeBase
{

    public ActionWatchVideo actionType = ActionWatchVideo.Daily;

    public override void OnClick()
    {
        GameController.Instance.admobAds.ShowVideoReward(
                OnRewardSuccess,
                OnRewardNotLoaded,
                OnRewardClose,
                actionType,
                1.ToString()
            );
    }

    void OnRewardSuccess()
    {
        Debug.LogError("Reward Success - give reward to player");
    }

    void OnRewardNotLoaded()
    {
        Debug.LogError("Reward video not loaded");
    }

    void OnRewardClose()
    {
        Debug.LogError("Reward video closed");
    }
}
