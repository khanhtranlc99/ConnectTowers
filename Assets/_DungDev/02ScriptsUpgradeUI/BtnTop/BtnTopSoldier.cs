using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnTopSoldier : BtnUpgradeBase
{

    public override void OnClick()
    {
        UpgradeBoxCtrl.Instance.CenterCtrl.SoldiersCardCtrl.SelectUnit(UpgradeBoxCtrl.Instance.CenterCtrl.SoldiersCardCtrl.EquippedUnitSlot);
        UpgradeBoxCtrl.Instance.CurrentCard.EquippedUnitSlot.ShowModel();
        UpgradeBoxCtrl.Instance.BottomCtrl.BtnUpgradeByCoin.UpdateUI();
        UpgradeBoxCtrl.Instance.BottomCtrl.BtnUpgradeByGem.UpdateUI();

    }


}
