using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheelSpinBox : BaseBox
{
    private static WheelSpinBox instance;
    public static WheelSpinBox Setup(bool isSaveBox = false, Action actionOpenBoxSave = null)
    {
        if (instance == null)
        {
            instance = Instantiate(Resources.Load<WheelSpinBox>(PathPrefabs.WHEEL_SPIN_BOX));
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
