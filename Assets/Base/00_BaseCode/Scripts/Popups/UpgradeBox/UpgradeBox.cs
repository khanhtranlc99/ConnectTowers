using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpgradeBox : BaseBox
{
    
    private static UpgradeBox instance;
    public static UpgradeBox Setup( bool isSaveBox = false, Action actionOpenBoxSave = null)
    {
        if (instance == null)
        {
            instance = Instantiate(Resources.Load<UpgradeBox>(PathPrefabs.UPGRADE_BOX));
            instance.Init();
        }
        instance.InitState();
        return instance;
    }

    public Button btnClose;
    private void Init()
    {
        btnClose.onClick.AddListener(delegate { Close(); 
            HomeController.Instance.homeScene.canvasHomeScene.SetSateThis(true); });
    }
    private void InitState()
    {
        //GameController.Instance.dataContain.dataUser.LoadCardInventoryData();
    }
}
