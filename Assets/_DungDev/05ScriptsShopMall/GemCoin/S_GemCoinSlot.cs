using EventDispatcher;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class S_GemCoinSlot : LoadAutoComponents
{
    [SerializeField] RewardItem rewardItem;
    [SerializeField] Image icon;
    [Space(10)]
    [SerializeField] Button btnBuy;
    [SerializeField] S_PanelGem_CoinCtrl panelItemCtrl;

    private void Start()
    {
        this.btnBuy.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        this.HandleResult(rewardItem);
    }

    void HandleResult(RewardItem rewardItemParam)
    {
        DataUserGame dataUser = GameController.Instance.dataContain.dataUser;

        switch (rewardItemParam.costType)
        {
            case CostType.Gem:
                if (rewardItemParam.CostAmount > dataUser.Gem) return;
                dataUser.DeductGem(rewardItemParam.CostAmount);
                break;
            case CostType.Coin:
                if (rewardItemParam.CostAmount > dataUser.Coin) return;
                dataUser.DeductCoin(rewardItemParam.CostAmount);
                break;
            case CostType.Ads:
                break;
        }
        switch (rewardItemParam.resultType)
        {
            case ResultType.Gem:
                dataUser.AddGems(rewardItemParam.amount);
                break;
            case ResultType.Coin:
                dataUser.AddCoins(rewardItemParam.amount);
                break;
        }

        this.panelItemCtrl.PanelResult.SetDisplayResult(icon.sprite, rewardItemParam.amount.ToString());
        //dotween anim panel result
        this.PostEvent(EventID.UPDATE_COIN_GEM);
        this.PostEvent(EventID.PANEL_RESULT_GEM_COIN);

    }

    public override void LoadComponent()
    {
        base.LoadComponent();
        this.btnBuy = GetComponentInChildren<Button>();
        this.icon = transform.Find("icon").GetComponent<Image>();
        this.panelItemCtrl = GetComponentInParent<S_PanelGem_CoinCtrl>();
    }

}
