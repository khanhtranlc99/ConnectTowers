using EventDispatcher;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class S_GemCoinSlot : LoadAutoComponents
{
    public int idSlot;
    [SerializeField] RewardItem rewardItem;
    public RewardItem RewardItem => rewardItem;
    [SerializeField] Image icon;
    [Space(10)]
    public Button btnBuy;
    public Image imgBought;
    [SerializeField] S_PanelGem_CoinCtrl panelItemCtrl;

    private void Start()
    {
        this.btnBuy.onClick.AddListener(OnClick);
    }

    public void Init(bool state)
    {
        this.imgBought?.gameObject.SetActive(state);
    }


    void OnClick()
    {
        this.HandleResult(rewardItem);
    }

    void HandleResult(RewardItem rewardItemParam)
    {
        DataUserGame dataUser = GameController.Instance.dataContain.dataUser;
        dataUser.DataShop.LsIsRewardCollected[idSlot].isCollected = true;

        ShopMallSave_Json.SaveDataShopMallCoin_Gem(dataUser.DataShop);
        this.Init(dataUser.DataShop.LsIsRewardCollected[idSlot].isCollected);
        GameController.Instance.musicManager.PlayClickSound();

        switch (rewardItemParam.costType)
        {
            case CostType.Gem:
                if (rewardItemParam.CostAmount > UseProfile.D_GEM) return;
                dataUser.DeductGem(rewardItemParam.CostAmount);
                break;
            case CostType.Coin:
                if (rewardItemParam.CostAmount > UseProfile.D_COIN) return;
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
        this.imgBought.transform.Find("btnBought").GetComponent<Image>();
    }

}
