using EventDispatcher;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BtnRerollShop : BtnUpgradeBase
{
    [SerializeField] S_PanelCardCtrl panelCardCtrl;
    public override void OnClick()
    {
        if (!this.IsCanReroll()) return;

        foreach(var child in this.panelCardCtrl.LsCardSlots)
        {
            child.RerollRandomCard();
        }
        
    }

    bool IsCanReroll()
    {
        DataUserGame dataUser = GameController.Instance.dataContain.dataUser;
        if (dataUser.Gem < 20)
            return false;
        dataUser.DeductGem(20);
        this.PostEvent(EventID.UPDATE_COIN_GEM);
        return true;
    }
}
