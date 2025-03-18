using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnUpgradeByCoin : BtnUpgradeBase
{
    public override void OnClick()
    {
        UnitSlotBase unitToUpgrade = UpgradeBoxCtrl.Instance.GetEquippedUnit();
        if(unitToUpgrade != null) unitToUpgrade.UpgradeUnit();
    }
}
