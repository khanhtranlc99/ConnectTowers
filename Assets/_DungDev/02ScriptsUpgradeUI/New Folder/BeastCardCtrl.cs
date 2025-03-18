using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeastCardCtrl : BaseCardCtrl
{

    [Space(10)]
    [SerializeField] protected List<BeastSlot> lsBaseUnitSlot = new();
    public List<BeastSlot> LsBaseUnitSlot => lsBaseUnitSlot;

    public override void UpdateTickMarks()
    {
        foreach (var child in this.lsBaseUnitSlot)
        {
            child.SetSelected(child == selectedUnit);
        }
    }
}
