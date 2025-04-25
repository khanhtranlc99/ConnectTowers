using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageCardCtrl : BaseCardCtrl
{
    [Space(10)]
    [SerializeField] protected List<MageSlot> lsBaseUnitSlot = new();
    public List<MageSlot> LsBaseUnitSlot => lsBaseUnitSlot;
    protected override UnitSlotBase SetInitCardEquipped()
    {
        PropertiesUnitsBase unitData = GameController.Instance.dataContain.dataUser.CurrentCardMage;
        foreach (var child in this.lsBaseUnitSlot)
        {
            if (child.unitsType == unitData.unitType) return child;
        }
        return null;
    }
    public override void UpdateTickMarks()
    {
        Debug.Log("Updating tick marks for Mage");
        foreach (var child in this.lsBaseUnitSlot)
        {
            child.SetSelected(child == selectedUnit);

        }
    }
}
