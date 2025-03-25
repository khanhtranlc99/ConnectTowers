using EventDispatcher;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V_CenterCtrl : MonoBehaviour
{
    [SerializeField] List<V_SlotCategory> lsSlotCategorys = new();

    [SerializeField] List<V_ItemInfoSlot> lsItemInfoSlots = new();

    private void OnEnable()
    {
        var dataVip = GameController.Instance.dataContain.dataUser.DataUserVip;
        this.UpdateUI(dataVip.LsRewardSystems[dataVip.CurrentVip]);

        this.RegisterListener(EventID.UPDATE_VIP_BOX, UpdateUI);
    }

    private void OnDisable()
    {
        this.RemoveListener(EventID.UPDATE_VIP_BOX, UpdateUI);
    }

    private void OnDestroy()
    {
        this.RemoveListener(EventID.UPDATE_VIP_BOX, UpdateUI);
    }

    public void UpdateUI(object rewardSystemObj)
    {
        if(rewardSystemObj == null)
        {
            var dataVip = GameController.Instance.dataContain.dataUser.DataUserVip;
            rewardSystemObj = dataVip.LsRewardSystems[dataVip.CurrentVip];
        }

        V_RewardSystem rewardSystem = rewardSystemObj as V_RewardSystem;

        foreach (var child in this.lsSlotCategorys) child.gameObject.SetActive(false);
        foreach(var child in this.lsItemInfoSlots) child.gameObject.SetActive(false);

        if (rewardSystem.LsRewardCategorys.Count > 3) return;

        //duyet list categorys
        for (int i = 0; i < rewardSystem.LsRewardCategorys.Count; i++)
        {
            Debug.LogWarning(rewardSystem.LsRewardCategorys.Count);
            this.lsSlotCategorys[i].gameObject.SetActive(true);
            this.lsSlotCategorys[i].UpdateUI(rewardSystem.LsRewardCategorys[i]);
        }

        this.HandleItemInfoSlot(rewardSystem.RewardIncreaseSlot);

    }

    void HandleItemInfoSlot(V_RewardIncreaseSlot rewardIncreaseSlot)
    {
        (int value, string text)[] slotData =
        {
            (rewardIncreaseSlot.CoinIncreaseAmount, "<sprite=0> Coin earnings increased by {0}%"),
            (rewardIncreaseSlot.GemIncreaseAmount, "<sprite=0> Gem earnings increased by {0}%"),
            (rewardIncreaseSlot.CoinReductAmount, "<sprite=0> Coin cost reduced by {0}%"),
            (rewardIncreaseSlot.GemReductAmount, "<sprite=0> Gem cost reduced by {0}%")
        };


        for (int i = 0; i < lsItemInfoSlots.Count; i++)
        {
            bool isActive = slotData[i].value != 0;
            lsItemInfoSlots[i].gameObject.SetActive(isActive);

            if (isActive)
                lsItemInfoSlots[i].UpdateUI(string.Format(slotData[i].text, slotData[i].value));
        }
    }

    [Button("Set up ID V_SlotCategory")]
    void SetUp()
    {
        for (int i = 0; i < this.lsSlotCategorys.Count;i++)
        {
            this.lsSlotCategorys[i].idSlot = i;
        }
    }

}
