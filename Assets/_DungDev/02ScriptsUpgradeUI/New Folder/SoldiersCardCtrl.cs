using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldiersCardCtrl : BaseCardCtrl
{
    [Space(10)]
    [SerializeField] protected List<SoldierSlot> lsBaseUnitSlot = new();
    public List<SoldierSlot> LsBaseUnitSlot => lsBaseUnitSlot;
    protected override UnitSlotBase SetInitCardEquipped()
    {
        PropertiesUnitsBase unitData = GameController.Instance.dataContain.dataUser.CurrentCardSoldier;
        foreach (var child in this.lsBaseUnitSlot)
        {
            if (child.unitsType == unitData.unitType) return child;
        }
        return null;
    }

    public override void UpdateTickMarks()
    {
        Debug.Log("Updating tick marks for Soldier");
        foreach (var child in this.lsBaseUnitSlot)
        {
            child.SetSelected(child == selectedUnit);
        }
    }

}
