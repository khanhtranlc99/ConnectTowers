using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeastSlot : UnitSlotBase
{
    public override void OnClick()
    {
        this.cardCtrl.SelectUnit(this);
    }
}
