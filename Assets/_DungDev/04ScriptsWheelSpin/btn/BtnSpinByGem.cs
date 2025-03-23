using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnSpinByGem : BtnUpgradeBase
{
    [SerializeField] WheelSpinCtrl spinCtrl;
    public override void OnClick()
    {
        if (!this.IsCanSpin()) return;
        StartCoroutine(this.spinCtrl.SpinningWheel());
    }

    bool IsCanSpin()
    {
        DataUserGame dataUser = GameController.Instance.dataContain.dataUser;
        if(dataUser.Gem < 50) return false;
        dataUser.DeductGem(50);
        GameController.Instance.dataContain.dataUser.DataDailyQuest.IncreaseQuestProgress(QuestType.SpinWheel,1);
        return true;
    }
}
