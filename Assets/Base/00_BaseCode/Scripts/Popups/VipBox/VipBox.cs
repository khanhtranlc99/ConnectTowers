using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VipBox : BaseBox
{
    private static VipBox instance;
    public static VipBox Setup(bool isSaveBox = false, Action actionOpenBoxSave = null)
    {
        if (instance == null)
        {
            instance = Instantiate(Resources.Load<VipBox>(PathPrefabs.VIP_BOX));
            instance.Init();
        }
        instance.InitState();
        return instance;
    }

    public Button btnClose;
    private void Init()
    {
        btnClose.onClick.AddListener(delegate { Close();
            HomeController.Instance.homeScene.canvasHomeScene.SetStateCanvasDynamic(true); });
    }
    private void InitState()
    {

    }
}
