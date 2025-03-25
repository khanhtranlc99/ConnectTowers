using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V_CenterCtrl : MonoBehaviour
{
    [SerializeField] List<V_SlotCategory> lsSlotCategorys = new();

    private void OnEnable()
    {
        var dataVip = GameController.Instance.dataContain.dataUser.DataUserVip;
        this.UpdateUI(dataVip.LsRewardSystems[dataVip.CurrentVip]);
    }

    public void UpdateUI(V_RewardSystem rewardSystem)
    {
        foreach (var child in this.lsSlotCategorys) child.gameObject.SetActive(false);
        if (rewardSystem.LsRewardCategorys.Count > 3) return;

        for (int i = 0; i < rewardSystem.LsRewardCategorys.Count; i++)
        {
            this.lsSlotCategorys[i].gameObject.SetActive(true);
            this.lsSlotCategorys[i].UpdateUI(rewardSystem.LsRewardCategorys[i]);
        }
    }
}
