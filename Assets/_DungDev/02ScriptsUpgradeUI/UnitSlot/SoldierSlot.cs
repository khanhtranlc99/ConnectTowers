using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierSlot : UnitSlotBase
{
    public override void OnClick()
    {
        this.cardCtrl.SelectUnit(this);
        this.ShowModel();
    }
}
