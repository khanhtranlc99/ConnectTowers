using EventDispatcher;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class S_GemCoinSlot : LoadAutoComponents
{
    [SerializeField] ResultType resultType;
    [SerializeField] Image icon;
    [SerializeField] int amount;
    [Space(10)]
    [SerializeField] Button btnBuy;
    [SerializeField] int btnAmount;
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
        this.HandleResult(resultType,amount,icon.sprite);
    }

    void HandleResult(ResultType type, int amount, Sprite icon)
    {
        switch (type)
        {
            case ResultType.Gem:
                GameController.Instance.dataContain.dataUser.AddGems(amount);
                GameController.Instance.dataContain.dataUser.DeductGem(amount);
                break;
            case ResultType.Coin:
                GameController.Instance.dataContain.dataUser.AddCoins(amount);
                GameController.Instance.dataContain.dataUser.DeductCoin(amount);
                break;
        }
        this.panelItemCtrl.PanelResult.SetDisplayResult(icon, amount.ToString());
        this.PostEvent(EventID.PANEL_GEM_COIN);

    }

    public override void LoadComponent()
    {
        base.LoadComponent();
        this.btnBuy = GetComponentInChildren<Button>();
        this.icon = transform.Find("icon").GetComponent<Image>();
        this.panelItemCtrl = GetComponentInParent<S_PanelItemCtrl>();
    }

}
