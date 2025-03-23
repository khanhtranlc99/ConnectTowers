using EventDispatcher;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public enum ResultType
{
    Ads,
    Gem,
    Coin,
}

public class S_CardSlot : LoadAutoComponents
{
    [SerializeField] protected ResultType resultType;
    [SerializeField] protected List<UnitRank> cardRanks;
    [SerializeField] protected int cost;
    [SerializeField] protected PropertiesUnitsBase dataUnit;
    [SerializeField] protected int amount;
    [Space(10)]
    [SerializeField] protected TextMeshProUGUI txtCurrentCardCount;
    [SerializeField] protected TextMeshProUGUI txtRequiredCardText;
    [SerializeField] protected Image progessBar;

    [SerializeField] protected Button btnBuy;

    [Space(10)]
    [SerializeField] protected Image imgFrame_0;
    [SerializeField] protected Image imgFrame_1;
    [SerializeField] protected Image imgFrame_top;
    [SerializeField] protected Image imgIconUnit;
    [SerializeField] protected Image imgBoxUnit;
    [SerializeField] protected TextMeshProUGUI txtNameUnit;

    [SerializeField] protected Image imgFrameCenter;
    [SerializeField] protected Image imgBoxCenter;

    private void Start()
    {
        this.btnBuy.onClick.AddListener(OnClick);
    }
    void OnClick()
    {
        this.HandleResult(resultType, cost, amount, imgIconUnit.sprite);
    }

    void HandleResult(ResultType type, int cost, int amount, Sprite icon)
    {
        switch (type)
        {

            case ResultType.Gem:
                GameController.Instance.dataContain.dataUser.AddCards(dataUnit, amount);
                GameController.Instance.dataContain.dataUser.DeductGem(cost);
                break;
            case ResultType.Coin:
                GameController.Instance.dataContain.dataUser.AddCards(dataUnit, amount);
                GameController.Instance.dataContain.dataUser.DeductCoin(cost);
                break;
            case ResultType.Ads:
                GameController.Instance.dataContain.dataUser.AddCards(dataUnit, amount);
                break;
        }
        //this.panelItemCtrl.PanelResult.SetDisplayResult(icon, amount.ToString());
        this.PostEvent(EventID.PANEL_GEM_COIN);
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
        this.SetInfoCard(lsPro[rand]);
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

        this.btnBuy = GetComponentInChildren<Button>();

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
