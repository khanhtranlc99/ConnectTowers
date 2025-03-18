using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnTopMage : BtnUpgradeBase
{
    public override void OnClick()
    {
        UpgradeBoxCtrl.Instance.CenterCtrl.MageCardCtrl.UpdateUI();

    }
}
