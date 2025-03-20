using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnSummonx1 : BtnUpgradeBase
{
    [SerializeField] Transform trans;
    [SerializeField] CardRandomCtrl cardRandomCtrl;
    public override void OnClick()
    {
        if (!IsCanSummon()) return;
        this.trans.gameObject.SetActive(true);
        StartCoroutine(cardRandomCtrl.SummonRoutine());
    }
    bool IsCanSummon()
    {
        DataUserGame dataUser = GameController.Instance.dataContain.dataUser;
        if (dataUser.Gem < 30) return false;

        dataUser.DeductGem(30);
        return true;
    }

}
