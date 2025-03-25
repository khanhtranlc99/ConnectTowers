using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V_SlotCategory : MonoBehaviour
{
    [SerializeField] List<V_ItemSlot> lsItemSlots = new();
    public void UpdateUI(V_RewardCategory rewardCategory)
    {
        //false het
        foreach (var child in this.lsItemSlots) child.gameObject.SetActive(false);
        var dataVip = GameController.Instance.dataContain.dataUser.DataUserVip;

        if (rewardCategory.LsRewardSlots.Count > 4) return;

        for (int i = 0; i < rewardCategory.LsRewardSlots.Count; i++)
        {
            this.lsItemSlots[i].gameObject.SetActive(true);
            this.lsItemSlots[i].UpdateUI(rewardCategory.LsRewardSlots[i]);
        }

        
    }
}
