using EventDispatcher;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V_FV_CenterCtrl : MonoBehaviour
{
    [SerializeField] List<V_FV_SlotCategory> lsSlotCategorys = new();

    private void OnEnable()
    {
        var dataVip = GameController.Instance.dataContain.dataUser.DataUserVip;
        this.UpdateUI(dataVip.LsRewardDailySystems[dataVip.CurrentDay]);

        this.RegisterListener(EventID.UPDATE_FREE_VIP_BOX, this.UpdateUI);
    }

    private void OnDisable()
    {
        this.RemoveListener(EventID.UPDATE_FREE_VIP_BOX, this.UpdateUI);

    }

    private void OnDestroy()
    {
        this.RemoveListener(EventID.UPDATE_FREE_VIP_BOX, this.UpdateUI);

    }

    public void UpdateUI(object obj)
    {
        if(!(obj is V_RewardDailySystem rewardDailySystem)) return;

        foreach (var child in this.lsSlotCategorys) child.gameObject.SetActive(false);

        var dataVip = GameController.Instance.dataContain.dataUser.DataUserVip;
        rewardDailySystem = dataVip.LsRewardDailySystems[dataVip.CurrentDay];

        for(int i = 0; i < rewardDailySystem.LsRewardSlots.Count; i++)
        {
            this.lsSlotCategorys[i].gameObject.SetActive(true);
            this.lsSlotCategorys[i].UpdateUI(rewardDailySystem);
        }

    }
}
