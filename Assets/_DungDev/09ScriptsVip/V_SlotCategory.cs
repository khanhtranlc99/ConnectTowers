using EventDispatcher;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class V_SlotCategory : MonoBehaviour
{
    public int idSlot;
    public Button btnClaim;

    public int vipParam;
    [SerializeField] List<V_ItemSlot> lsItemSlots = new();

    public void HandleBtnState(bool state)
    {
        this.btnClaim.interactable = state;
    }

    private void Start()
    {
        this.btnClaim.onClick.AddListener(() => OnClick(vipParam));
    }
    public void OnClick(int vipParam)
    {
        var dataUser = GameController.Instance.dataContain.dataUser;
        var rewardSystem = dataUser.DataUserVip.LsRewardSystems[vipParam];
        var rewardCategory = rewardSystem.LsRewardCategorys[idSlot];

        rewardCategory.isClaim = true;
        this.HandleBtnState(!rewardCategory.isClaim);

        /// result reward
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

        
    }

    public void UpdateUI(V_RewardCategory rewardCategory)
    {
        //false het
        foreach (var child in this.lsItemSlots) child.gameObject.SetActive(false);
        for (int i = 0; i < rewardCategory.LsRewardSlots.Count; i++)
        {
            this.lsItemSlots[i].gameObject.SetActive(true);
            this.lsItemSlots[i].UpdateUI(rewardCategory.LsRewardSlots[i]);
        }
    }
}
