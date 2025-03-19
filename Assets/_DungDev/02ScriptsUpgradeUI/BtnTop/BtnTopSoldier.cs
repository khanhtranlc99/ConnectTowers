using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnTopSoldier : BtnUpgradeBase
{
    public override void OnClick()
    {
        UpgradeBoxCtrl.Instance.CenterCtrl.SoldiersCardCtrl.SelectUnit(UpgradeBoxCtrl.Instance.CenterCtrl.SoldiersCardCtrl.EquippedUnitSlot);
        Debug.Log("Btn Soldier");
    }


}
