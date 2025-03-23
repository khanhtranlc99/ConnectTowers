using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnRerollShop : BtnUpgradeBase
{
    [SerializeField] S_PanelCardCtrl panelCardCtrl;
    public override void OnClick()
    {
        foreach(var child in this.panelCardCtrl.LsCardSlots)
        {
            child.GetRandomInfoCard();
        }
        
    }
}
