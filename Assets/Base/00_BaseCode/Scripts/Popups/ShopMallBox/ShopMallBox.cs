using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMallBox : BaseBox
{
    private static ShopMallBox instance;
    public static ShopMallBox Setup(bool isSaveBox = false, Action actionOpenBoxSave = null)
    {
        if (instance == null)
        {
            instance = Instantiate(Resources.Load<ShopMallBox>(PathPrefabs.SHOP_MALL_BOX));
            instance.Init();
        }
        instance.InitState();
        return instance;
    }

    public Button btnClose;
    private void Init()
    {
        btnClose.onClick.AddListener(delegate {
            Close();
            HomeController.Instance?.homeScene?.canvasHomeScene?.SetSateThis(true);
        });
    }
    private void InitState()
    {

    }
}
