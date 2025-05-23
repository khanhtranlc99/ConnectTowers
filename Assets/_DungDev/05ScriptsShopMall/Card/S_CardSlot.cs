﻿using EventDispatcher;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class S_CardSlot : LoadAutoComponents
{
    [Header("ID Slot Card")]
    public int iD;
    [Space(5)]
    [SerializeField] protected RewardItem rewardItem;
    [SerializeField] protected PropertiesUnitsBase dataUnit;
    [Space(10)]
    [SerializeField] protected TextMeshProUGUI txtCurrentCardCount;
    [SerializeField] protected TextMeshProUGUI txtRequiredCardText;
    [SerializeField] protected Image progessBar;

    [SerializeField] protected Button btnBuy;
    [SerializeField] protected Button btnBought;

    [Space(10)]
    [SerializeField] protected Image imgFrame_0;
    [SerializeField] protected Image imgFrame_1;
    [SerializeField] protected Image imgFrame_top;
    [SerializeField] protected Image imgIconUnit;
    [SerializeField] protected Image imgBoxUnit;
    [SerializeField] protected TextMeshProUGUI txtNameUnit;

    [SerializeField] protected Image imgFrameCenter;
    [SerializeField] protected Image imgBoxCenter;

    [Space(10)]
    [SerializeField] protected TextMeshProUGUI textCoinAmount;
    [SerializeField] Image iconSale;
    [SerializeField] TextMeshProUGUI textSaleAmount;
    [Space(10)]
    public List<UnitRank> lsCardRanks;
    public List<PropertiesUnitsBase> lsPro = new();
    int costAmount;
    PropertiesUnitsBase lastDataUnit;
    public void Init(DataShopReroll dataShopReroll)
    {
        this.costAmount = dataShopReroll.currentCostAmount;
    }

    private void Start()
    {
        this.btnBuy.onClick.AddListener(OnClick);
    }
    void OnClick()
    {
        if (this.dataUnit == null) return;
        this.HandleResult( dataUnit,rewardItem);
        this.SetInfoCard(dataUnit);
    }

    public void SetPropertiesCard(PropertiesUnitsBase propertiesUnits)
    {
        this.dataUnit = propertiesUnits;
    }

    void HandleResult(PropertiesUnitsBase dataUnitParam ,RewardItem rewardItemParam)
    {
        var dataUser = GameController.Instance.dataContain.dataUser;

        switch (rewardItem.costType)
        {
            case CostType.Gem:
                if (costAmount > UseProfile.D_GEM) break;
                dataUser.DeductGem(costAmount);
                dataUser.AddCards(dataUnitParam, 1);
                this.btnBought.gameObject.SetActive(true);
                break;
            case CostType.Coin:
                if (costAmount > UseProfile.D_COIN) break;
                dataUser.DeductCoin(costAmount);
                dataUser.AddCards(dataUnitParam, 1);
                this.btnBought.gameObject.SetActive(true);
                break;
            case CostType.Ads:
                dataUser.AddCards(dataUnitParam, 1);
                this.btnBought.gameObject.SetActive(true);
                break;
        }
        this.PostEvent(EventID.UPDATE_COIN_GEM);
        this.UpdateProgessBar(dataUnitParam);
    }

    public void RerollRandomCard()
    {
        if (lsPro.Count < 1) return;
        Debug.Log("GET REROLL COMPLETE");
        int rand = Random.Range(0, lsPro.Count);
        this.dataUnit = lsPro[rand];
        this.SetInfoCard(dataUnit);
        this.HandleSaleIcon(rewardItem);
        this.btnBought.gameObject.SetActive(false);

        GameController.Instance.dataContain.dataUser.DataShop.SetListDataUnits(iD,this.dataUnit,this.costAmount);

    }

    public void ShowIconSale(int costSale)
    {
        if (rewardItem.costType == CostType.Ads || rewardItem.costType == CostType.Free) return;
        this.textSaleAmount.text = "-" + costSale.ToString() + "%";
        this.iconSale.gameObject.SetActive(true);
    }
    int HandleRandomCostSale()
    {
        int rand = Random.Range(0, 100);
        switch (rand)
        {
            case < 40:  // 40% cho 0%
                return 0;
            case < 52:  // 12% cho 10%
                return 10;
            case < 64:  // 12% cho 20%
                return 20;
            case < 76:  // 12% cho 30%
                return 30;
            case < 83:  // 7% cho 40%
                return 40;
            case < 90:  // 7% cho 50%
                return 50;
            case < 96:  // 6% cho 60%
                return 60;
            case < 98:  // 2% cho 70%
                return 70;
            default:    // 2% còn lại cho 80%
                return 80;
        }
    }

    // xu li hien thi icon sale
    private void HandleSaleIcon(RewardItem rewardItem)
    {
        int randSale = HandleRandomCostSale();
        this.costAmount = rewardItem.CostAmount;
        this.iconSale.gameObject.SetActive(false);
        this.ShowTextCoinAmount(this.costAmount);

        if (randSale < 1) return;

        int newCostAmount = rewardItem.CostAmount - (rewardItem.CostAmount * randSale / 100);
        this.costAmount = newCostAmount;
        ShowIconSale(randSale);
        this.ShowTextCoinAmount(this.costAmount);
    }

    public void ShowTextCoinAmount(int cost)
    {
        this.textCoinAmount.text = cost.ToString();
    }

    public void SetInfoCard(PropertiesUnitsBase dataUnitParam)
    {
        if (this.lastDataUnit == dataUnitParam) return;
        lastDataUnit = dataUnitParam;

        if (this.imgFrame_0.sprite != dataUnitParam.FrameRank)
        {
            this.imgFrame_0.sprite = dataUnitParam.FrameRank;
            this.imgFrame_1.sprite = dataUnitParam.FrameRank;
            this.imgFrame_top.sprite = dataUnitParam.FrameRank;
        }

        if (this.imgBoxCenter.sprite != dataUnitParam.BoxRank)
        {
            this.imgBoxCenter.sprite = dataUnitParam.BoxRank;
            this.imgBoxUnit.sprite = dataUnitParam.BoxRank;
        }

        if (this.imgIconUnit.sprite != dataUnitParam.SpriteUnit)
        {
            this.imgIconUnit.sprite = dataUnitParam.SpriteUnit;
            this.imgIconUnit.SetNativeSize();
        }

        if (this.txtNameUnit.text != dataUnitParam.unitType.ToString())
        {
            this.txtNameUnit.text = dataUnitParam.unitType.ToString();
        }

        this.UpdateProgessBar(dataUnitParam);

    }

    void UpdateProgessBar(PropertiesUnitsBase dataUnitParam)
    {
        this.txtCurrentCardCount.text = GameController.Instance.dataContain.dataUser.FindUnitCard(dataUnitParam).cardCount.ToString();
        this.txtRequiredCardText.text = "/" + dataUnitParam.GetCostCard.ToString();
        this.progessBar.fillAmount = (float)GameController.Instance.dataContain.dataUser.FindUnitCard(dataUnitParam).cardCount / (float)dataUnitParam.GetCostCard;
    }


    public override void LoadComponent()
    {
        base.LoadComponent();
        this.txtCurrentCardCount = transform.Find("progess").Find("txtLeft").GetComponent<TextMeshProUGUI>();
        this.txtRequiredCardText = transform.Find("progess").Find("txtRight").GetComponent<TextMeshProUGUI>();
        this.progessBar = transform.Find("progess").Find("progessBar").GetComponent<Image>();

        this.btnBuy = transform.Find("btnBuy").GetComponent<Button>();
        this.btnBought = transform.Find("btnBought").GetComponent<Button>();
        this.btnBought.gameObject.SetActive(false);

        this.imgFrame_0 = transform.Find("top").Find("Image").GetComponent<Image>();
        this.imgFrame_1 = transform.Find("top").Find("Image (1)").GetComponent<Image>();
        this.imgFrame_top = transform.Find("top").Find("Image (2)").GetComponent<Image>();

        this.imgIconUnit = transform.Find("top").Find("icon").GetComponent<Image>();
        this.imgBoxUnit = transform.Find("top").Find("Image (2)").Find("box").GetComponent<Image>();

        this.txtNameUnit = transform.Find("top").Find("nameUnit").GetComponent<TextMeshProUGUI>();

        this.imgFrameCenter = transform.Find("center").Find("frame").GetComponent<Image>();
        this.imgBoxCenter = transform.Find("center").Find("box").GetComponent<Image>();

        this.textCoinAmount = transform.Find("btnBuy").Find("count").GetComponent<TextMeshProUGUI>();
        this.iconSale = transform.Find("iconSale").GetComponent<Image>();
        this.textSaleAmount = transform.Find("iconSale").Find("saleCount").GetComponent<TextMeshProUGUI>();

    }
}
