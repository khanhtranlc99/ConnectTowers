using EventDispatcher;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnSummonx10 : BtnUpgradeBase
{
    [SerializeField] Transform trans;
    [SerializeField] SummonCtrlx10 summonCtrlx10;
    public override void OnClick()
    {
        if (!IsCanSummon()) return;
        this.trans.gameObject.SetActive(true);
        StartCoroutine(summonCtrlx10.SummonRoutine());
    }

    bool IsCanSummon()
    {
        DataUserGame dataUser = GameController.Instance.dataContain.dataUser;
        if (dataUser.Gem < 270) return false;

        dataUser.DeductGem(270);
        this.PostEvent(EventID.UPDATE_COIN_GEM);

        GameController.Instance.dataContain.dataUser.DataDailyQuest.IncreaseQuestProgress(QuestType.SummonSingle, 1);

        return true;
    }
}
