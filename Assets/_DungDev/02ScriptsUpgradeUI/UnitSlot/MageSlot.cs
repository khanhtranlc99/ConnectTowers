using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageSlot : UnitSlotBase
{
    public override void OnClick()
    {
        this.cardCtrl.SelectUnit(this);
        this.ShowModel();
    }
}
