using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageCardCtrl : BaseCardCtrl
{
    [Space(10)]
    [SerializeField] protected List<MageSlot> lsBaseUnitSlot = new();
    public List<MageSlot> LsBaseUnitSlot => lsBaseUnitSlot;

    public override void UpdateTickMarks()
    {
        foreach (var child in this.lsBaseUnitSlot)
        {
            child.SetSelected(child == selectedUnit);

        }
    }
}
