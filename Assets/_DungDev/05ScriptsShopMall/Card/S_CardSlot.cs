using EventDispatcher;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class S_CardSlot : LoadAutoComponents
{
    [SerializeField] protected RewardItem rewardItem;
    [SerializeField] protected List<UnitRank> cardRanks;
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


    private void OnEnable()
    {
        if (this.dataUnit == null) return;
        this.SetInfoCard(dataUnit);
    }
    private void Start()
    {
        this.btnBuy.onClick.AddListener(OnClick);
    }
    void OnClick()
    {
        if (this.dataUnit == null) return;
        this.HandleResult( dataUnit,rewardItem);
        this.btnBought.gameObject.SetActive(true);
    }

    void HandleResult(PropertiesUnitsBase dataUnitParam ,RewardItem rewardItemParam)
    {
        var dataUser = GameController.Instance.dataContain.dataUser;

        switch (rewardItem.costType)
        {
            case CostType.Gem:
                dataUser.DeductGem(rewardItemParam.CostAmount);
                dataUser.AddCards(dataUnitParam, rewardItemParam.amount);
                break;
            case CostType.Coin:
                dataUser.DeductCoin(rewardItemParam.CostAmount);
                dataUser.AddCards(dataUnitParam, rewardItemParam.amount);
                break;
            case CostType.Ads:
                break;
        }
        //this.panelItemCtrl.PanelResult.SetDisplayResult(icon, amount.ToString());
        this.UpdateProgessBar(dataUnit);
        this.PostEvent(EventID.PANEL_RESULT_GEM_COIN);
    }

    public void GetRandomInfoCard()
    {
        DataUnits dataUnits = GameController.Instance.dataContain.dataUnits;
        List<PropertiesUnitsBase> lsPro = new();
        foreach(var child in dataUnits.lsPropertiesBases)
        {
            if (cardRanks.Contains(child.unitRank)) lsPro.Add(child);
        }
        if (lsPro.Count < 1) return;

        Debug.Log("GET REROLL COMPLETE");
        int rand = Random.Range(0, lsPro.Count);
        this.dataUnit = lsPro[rand];
        this.SetInfoCard(dataUnit);
        this.btnBought.gameObject.SetActive(false);
    }

    public void SetInfoCard(PropertiesUnitsBase dataUnitParam)
    {
        this.imgFrame_0.sprite = dataUnitParam.FrameRank;
        this.imgFrame_1.sprite = dataUnitParam.FrameRank;
        this.imgFrame_top.sprite = dataUnitParam.FrameRank;

        this.imgBoxCenter.sprite = dataUnitParam.BoxRank;
        this.imgBoxUnit.sprite = dataUnitParam.BoxRank;

        this.imgIconUnit.sprite = dataUnitParam.SpriteUnit;
        this.imgIconUnit.SetNativeSize();

        this.txtNameUnit.text = dataUnitParam.unitType.ToString();

        this.imgFrameCenter.sprite = dataUnitParam.FrameRank;
        this.imgBoxCenter.sprite = dataUnitParam.BoxRank;
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
    }
}
