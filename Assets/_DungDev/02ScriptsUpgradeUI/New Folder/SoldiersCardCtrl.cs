using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldiersCardCtrl : BaseCardCtrl
{
    [Space(10)]
    [SerializeField] protected List<SoldierSlot> lsBaseUnitSlot = new();
    public List<SoldierSlot> LsBaseUnitSlot => lsBaseUnitSlot;

    public override void UpdateTickMarks()
    {
        Debug.Log("Updating tick marks for Soldier");
        foreach (var child in this.lsBaseUnitSlot)
        {
            child.SetSelected(child == selectedUnit);
        }
    }
}
