using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeastCardCtrl : BaseCardCtrl
{

    [Space(10)]
    [SerializeField] protected List<BeastSlot> lsBaseUnitSlot = new();
    public List<BeastSlot> LsBaseUnitSlot => lsBaseUnitSlot;
    protected override UnitSlotBase SetInitCardEquipped()
    {
        PropertiesUnitsBase unitData = GameController.Instance.dataContain.dataUser.CurrentCardBeast;
        foreach (var child in this.lsBaseUnitSlot)
        {
            if (child.unitsType == unitData.unitType) return child;
        }
        return null;
    }
    public override void UpdateTickMarks()
    {
        Debug.Log("Updating tick marks for Beast");

        foreach (var child in this.lsBaseUnitSlot)
        {
            child.SetSelected(child == selectedUnit);
        }
    }
}
