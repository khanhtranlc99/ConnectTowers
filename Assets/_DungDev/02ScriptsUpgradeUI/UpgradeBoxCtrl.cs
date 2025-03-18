using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeBoxCtrl : Singleton<UpgradeBoxCtrl>
{
    [SerializeField] protected U_CenterCtrl centerCtrl;
    public U_CenterCtrl CenterCtrl => centerCtrl;

    [SerializeField] protected U_BottomCtrl bottomCtrl;
    public U_BottomCtrl BottomCtrl => bottomCtrl;

    [SerializeField] protected U_TopCtrl topCtrl;
    public U_TopCtrl TopCtrl => topCtrl;
    [Space(10)]
    [SerializeField] BaseCardCtrl currentActiveCard;
    //set current card
    

    public void SetCurrentActiveCard(BaseCardCtrl baseCardCtrl)
    {
        this.currentActiveCard = baseCardCtrl;
    }
    public void EquipCurrentUnit()
    {
        if (currentActiveCard != null) this.currentActiveCard.EquipSelectedUnit();
    }

    //lay unit de upgrade
    public UnitSlotBase GetEquippedUnit()
    {
        return this.currentActiveCard?.EquippedUnitSlot;
    }

}
