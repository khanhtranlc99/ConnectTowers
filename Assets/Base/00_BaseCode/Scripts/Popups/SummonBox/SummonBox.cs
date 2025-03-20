using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SummonBox : BaseBox
{
    private static SummonBox instance;
    public static SummonBox Setup(bool isSaveBox = false, Action actionOpenBoxSave = null)
    {
        if (instance == null)
        {
            instance = Instantiate(Resources.Load<SummonBox>(PathPrefabs.SUMMON_BOX));
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
