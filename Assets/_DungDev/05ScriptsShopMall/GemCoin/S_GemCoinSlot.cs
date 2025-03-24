using EventDispatcher;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class S_GemCoinSlot : LoadAutoComponents
{
    [SerializeField] RewardItem rewardType;
    [SerializeField] Image icon;
    [Space(10)]
    [SerializeField] Button btnBuy;
    [SerializeField] S_PanelItemCtrl panelItemCtrl;

    private void Start()
    {
        this.btnBuy.onClick.AddListener(OnClick);
    }

    public void UpdateUI()
    {
        //if(GameController.Instance.dataContain.dataUser.DataShop.CanReceiveItem())
    }


    void OnClick()
    {
        this.HandleResult(rewardType);
    }

    void HandleResult(RewardItem rewardItem)
    {
        DataUserGame dataUser = GameController.Instance.dataContain.dataUser;

        switch (rewardItem.resultType)
        {
            case ResultType.Gem:
                dataUser.AddGems(rewardItem.amount);
                break;
            case ResultType.Coin:
                dataUser.AddCoins(rewardItem.amount);
                break;
        }
        switch (rewardItem.costType)
        {
            case CostType.Gem:
                dataUser.DeductGem(rewardItem.CostAmount);
                break;
            case CostType.Coin:
                dataUser.DeductCoin(rewardItem.CostAmount);
                break;
            case CostType.Ads:
                break;
        }

        this.panelItemCtrl.PanelResult.SetDisplayResult(icon.sprite, rewardItem.amount.ToString());
        //dotween anim panel result
        this.PostEvent(EventID.UPDATE_COIN_GEM);
        this.PostEvent(EventID.PANEL_RESULT_GEM_COIN);

    }

    public override void LoadComponent()
    {
        base.LoadComponent();
        this.btnBuy = GetComponentInChildren<Button>();
        this.icon = transform.Find("icon").GetComponent<Image>();
        this.panelItemCtrl = GetComponentInParent<S_PanelItemCtrl>();
    }

}
