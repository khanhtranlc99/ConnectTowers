using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnTopBeast : BtnUpgradeBase
{

    public override void OnClick()
    {
        UpgradeBoxCtrl.Instance.CenterCtrl.BeastCardCtrl.SelectUnit(UpgradeBoxCtrl.Instance.CenterCtrl.BeastCardCtrl.EquippedUnitSlot);
        UpgradeBoxCtrl.Instance.CurrentCard.EquippedUnitSlot.ShowModel();
        UpgradeBoxCtrl.Instance.BottomCtrl.BtnUpgradeByCoin.UpdateUI();
        UpgradeBoxCtrl.Instance.BottomCtrl.BtnUpgradeByGem.UpdateUI();

    }
}
