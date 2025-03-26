using EventDispatcher;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class V_FV_SlotCategory : MonoBehaviour
{
    public int idCategory;
    [SerializeField] Button btnClaim;
    [SerializeField] Image imgCollected;

    [SerializeField] TextMeshProUGUI txtDay;

    [SerializeField] List<V_ItemSlot> lsItemSlots = new();

    private void Start()
    {
        this.btnClaim.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        GameController.Instance.musicManager.PlayClickSound();

        this.btnClaim.gameObject.SetActive(false);
        this.imgCollected.gameObject.SetActive(true);
        var dataUser = GameController.Instance.dataContain.dataUser;
        var dataVip = dataUser.DataUserVip;
        var rewardDailySystem = dataVip.LsRewardDailySystems[idCategory];
        rewardDailySystem.isCollected = true;

        for(int i = 0; i < rewardDailySystem.LsRewardSlots.Count; i++)
        {
            var rewardSlot = rewardDailySystem.LsRewardSlots[i];
            switch (rewardSlot.ResultType)
            {
                case ResultType.Gem:
                    dataUser.AddGems(rewardSlot.AmountReward);
                    break;
                case ResultType.Vip:
                    dataVip.IncreaseProgress(rewardSlot.AmountReward);
                    break;
            }
        }

        foreach(var child in this.lsItemSlots)
        {
            child.HandleStateImgCollected(true);
        }
        this.PostEvent(EventID.UPDATE_COIN_GEM);
        this.PostEvent(EventID.UPDATE_VIP_BOX);

    }
    public void UpdateUI(V_RewardDailySystem rewardDailySystem)
    {
        if (rewardDailySystem.LsRewardSlots.Count > 2) return;
        Debug.Log("complete Category");

        for (int i = 0; i < rewardDailySystem.LsRewardSlots.Count; i++)
        {
            this.lsItemSlots[i].UpdateUI(rewardDailySystem.LsRewardSlots[i]);
            this.lsItemSlots[i].HandleStateImgCollected(rewardDailySystem.isCollected);
        }

        this.txtDay.text = rewardDailySystem.Day.ToString();
    }

    public void HandleStateBtnClaim(bool state)
    {
        this.btnClaim.gameObject.SetActive(state);
        this.imgCollected.gameObject.SetActive(!state);
    }
}
