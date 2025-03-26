using EventDispatcher;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class V_SlotCategory : MonoBehaviour
{
    public int idSlot;
    [SerializeField] Button btnClaim;

    [SerializeField] List<V_ItemSlot> lsItemSlots = new();

    private void Start()
    {
        var dataVip = GameController.Instance.dataContain.dataUser.DataUserVip;
        btnClaim.onClick.AddListener(()=> OnClick(dataVip.LsRewardSystems[dataVip.CurrentVip]));
    }

    void OnClick(V_RewardSystem rewardSystem)
    {

        var dataUser = GameController.Instance.dataContain.dataUser;
        var rewardCategory = rewardSystem.LsRewardCategorys[idSlot];
        //duyet qua tat ca thang con trong categor
        for (int i = 0; i < rewardCategory.LsRewardSlots.Count; i++)
        {
            var rewardSlot = rewardCategory.LsRewardSlots[i];
            switch (this.lsItemSlots[i].ResultType)
            {
                case ResultType.Coin:
                    dataUser.AddCoins(rewardSlot.AmountReward);
                    break;
                case ResultType.Gem:
                    dataUser.AddGems(rewardSlot.AmountReward);
                    break;
                case ResultType.Vip:
                    dataUser.DataUserVip.IncreaseProgress(rewardSlot.AmountReward);
                    break;
            }

        }
        this.PostEvent(EventID.UPDATE_COIN_GEM);
        this.PostEvent(EventID.UPDATE_VIP_BOX);
    }

    public void UpdateUI(V_RewardCategory rewardCategory)
    {
        //false het
        foreach (var child in this.lsItemSlots) child.gameObject.SetActive(false);
        var dataVip = GameController.Instance.dataContain.dataUser.DataUserVip;

        if(dataVip == null)
        {
            Debug.Log("null roi");
        }

        if (rewardCategory.LsRewardSlots.Count > 4) return;

        for (int i = 0; i < rewardCategory.LsRewardSlots.Count; i++)
        {
            this.lsItemSlots[i].gameObject.SetActive(true);
            this.lsItemSlots[i].UpdateUI(rewardCategory.LsRewardSlots[i]);
        }

        
    }
}
