using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class V_ItemSlot : LoadAutoComponents
{
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI txtAmount;

    public void UpdateUI(V_RewardSlot rewardSlot)
    {
        this.icon.sprite = rewardSlot.IconReward;
        this.icon.SetNativeSize();
        this.txtAmount.text = rewardSlot.AmountReward.ToString();
    }

    public override void LoadComponent()
    {
        base.LoadComponent();
        this.icon = transform.Find("icon").GetComponent<Image>();
        this.txtAmount = transform.Find("txtCount").GetComponent <TextMeshProUGUI>();
    }
}
