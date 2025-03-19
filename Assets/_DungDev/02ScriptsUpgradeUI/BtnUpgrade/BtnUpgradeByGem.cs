using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnUpgradeByGem : BtnUpgradeBase
{
    public override void OnClick()
    {
        UnitSlotBase unitToUpgrade = UpgradeBoxCtrl.Instance.GetEquippedUnit();
        if (unitToUpgrade != null) unitToUpgrade.UpgradeStarUnit();

        UpgradeBoxCtrl.Instance.CurrentCard.UpdateUI();
    }
}
