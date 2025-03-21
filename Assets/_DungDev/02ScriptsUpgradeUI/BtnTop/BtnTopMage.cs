using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnTopMage : BtnUpgradeBase
{

    public override void OnClick()
    {
        UpgradeBoxCtrl.Instance.CenterCtrl.MageCardCtrl.SelectUnit(UpgradeBoxCtrl.Instance.CenterCtrl.MageCardCtrl.EquippedUnitSlot);
        UpgradeBoxCtrl.Instance.BottomCtrl.BtnUpgradeByCoin.UpdateUI();
        UpgradeBoxCtrl.Instance.BottomCtrl.BtnUpgradeByGem.UpdateUI();
    }
}
