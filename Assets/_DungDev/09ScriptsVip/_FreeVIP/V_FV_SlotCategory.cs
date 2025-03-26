using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class V_FV_SlotCategory : MonoBehaviour
{
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
        this.btnClaim.gameObject.SetActive(false);
        this.imgCollected.gameObject.SetActive(true);
    }
    public void UpdateUI(V_RewardDailySystem rewardDailySystem)
    {
        if (rewardDailySystem.LsRewardSlots.Count > 2) return;
        Debug.Log("complete Category");

        for (int i = 0; i < rewardDailySystem.LsRewardSlots.Count; i++)
        {
            this.lsItemSlots[i].UpdateUI(rewardDailySystem.LsRewardSlots[i]);
        }

        this.txtDay.text = rewardDailySystem.Day.ToString();
    }

    void HandleStateBtnClaim(bool state)
    {
        this.btnClaim.gameObject.SetActive(state);
        this.imgCollected.gameObject.SetActive(state);
    }
}
