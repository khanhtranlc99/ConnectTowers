using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnEquiq : BtnUpgradeBase
{
    public override void OnClick()
    {
        UpgradeBoxCtrl.Instance.EquipCurrentUnit();
        this.gameObject.SetActive(false);
    }
}
