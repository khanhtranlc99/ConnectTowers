using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Q_DailySlot : LoadAutoComponents
{
    [SerializeField] Button btnClaim;
    [SerializeField] TextMeshProUGUI textRewardAmount;
    [SerializeField] Image imgReward;
    [SerializeField] int rewardAmount;
    private void Start()
    {
        this.btnClaim.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        GameController.Instance.dataContain.dataUser.AddGems(rewardAmount);
        this.DisableClaimButton();
    }

    public void DisableClaimButton()
    {
        this.btnClaim.interactable = false;
        this.textRewardAmount.gameObject.SetActive(false);
        this.imgReward.gameObject.SetActive(false);
    }
    // goi toi khi qua ngay moi =))     
    public void ResetDailyReward()
    {
        this.btnClaim.interactable = true;
        this.textRewardAmount.gameObject.SetActive(true);
        this.imgReward.gameObject.SetActive(true);
    }

    public override void LoadComponent()
    {
        base.LoadComponent();
        this.btnClaim = GetComponent<Button>();
        this.textRewardAmount = GetComponentInChildren<TextMeshProUGUI>();
        this.imgReward = GetComponentInChildren<Image>();
    }
}
