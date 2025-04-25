using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OfflineRewardChestBox : BaseBox
{
    private static OfflineRewardChestBox instance;
    public static OfflineRewardChestBox Setup(bool isSaveBox = false, Action actionOpenBoxSave = null)
    {
        if (instance == null)
        {
            instance = Instantiate(Resources.Load<OfflineRewardChestBox>(PathPrefabs.OFFLINE_REWARD_CHEST_BOX));
            instance.Init();
        }
        instance.InitState();
        return instance;
    }

    public Button btnClose;
    private void Init()
    {
        btnClose.onClick.AddListener(Close);
    }
    private void InitState()
    {

    }
}
