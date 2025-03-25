using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class O_OfflineRewardSlot : LoadAutoComponents
{
    [SerializeField] ResultType resultType;
    [Header("Left")]
    [SerializeField] TextMeshProUGUI gemRateText; //  gem / hour
    [SerializeField] TextMeshProUGUI gemTotalText; // tong gem
    [SerializeField] TextMeshProUGUI coinRateText;
    [SerializeField] TextMeshProUGUI coinTotalText;



    private void OnEnable()
    {
        var DataOffline = GameController.Instance.dataContain.dataUser.DataOfflineRewardChest;

        switch (resultType)
        {
            case ResultType.Coin:
                this.coinRateText.text = DataOffline.CoinPerHour.ToString() + "/Hour";
                this.coinTotalText.text = DataOffline.CoinTotal.ToString();
                break;
            case ResultType.Gem:
                this.gemRateText.text = DataOffline.GemPerHour.ToString() + "/Hour";
                this.gemTotalText.text = DataOffline.GemTotal.ToString();
                break;
        }

    }


    public override void LoadComponent()
    {
        base.LoadComponent();
        this.gemRateText = transform.Find("imgInfo").Find("txtAmount").GetComponent<TextMeshProUGUI>();
        this.coinRateText = transform.Find("imgInfo").Find("txtAmount").GetComponent<TextMeshProUGUI>();
        this.coinTotalText = transform.Find("txtCount").GetComponent<TextMeshProUGUI>();
        this.gemTotalText = transform.Find("txtCount").GetComponent<TextMeshProUGUI>();
    }

}
