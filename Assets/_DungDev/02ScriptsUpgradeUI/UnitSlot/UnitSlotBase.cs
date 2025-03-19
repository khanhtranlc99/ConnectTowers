using BestHTTP.SocketIO;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public abstract class UnitSlotBase : LoadAutoComponents
{
    public UnitsType unitsType;
    [SerializeField] PropertiesUnitsBase unitData;
    public PropertiesUnitsBase GetUnit() => unitData;


    public Image checkMarkImage;
    public Button selectButton;
    [SerializeField] protected BaseCardCtrl cardCtrl;

    [Space(10)]
    [SerializeField] DisplayTopUnit displayTopUnit;
    public DisplayTopUnit DisplayTopUnit => displayTopUnit;
    [Space(10)]
    [SerializeField] Image icon;
    public Image Icon => icon;
    [SerializeField] Image bg;
    public Image BG => bg;
    [SerializeField] Image box_level;
    public Image BoxLevel => box_level;
    [SerializeField] TextMeshProUGUI rankUnit;
    public TextMeshProUGUI RankUnit => rankUnit;
    [SerializeField] TextMeshProUGUI currentLevel;
    public TextMeshProUGUI CurrentLevel => currentLevel;

    [Space(10)]
    [SerializeField] List<Image> lsSpriteStar;

    private void OnEnable()
    {
        this.SetInfoUnit(unitData.unitType);
    }
    private void Start()
    {
        selectButton.onClick.AddListener(OnClick);
    }

    public abstract void OnClick();

    public void UpgradeLevelUnit()
    {
        Debug.Log(transform.name + "level up");
        unitData.currentLevel++;
    }
    public void UpgradeStarUnit()
    {
        Debug.Log(transform.name + "star up");
        unitData.starLevel++;
    }
    public void SetInfoUnit(UnitsType unitsType)
    {
        unitData = GameController.Instance.dataContain.dataUnits.GetPropertiesBases(unitsType);
        this.currentLevel.text = "Level: " + unitData.currentLevel.ToString();
        for (int i = 0; i < 5; i++)
        {
            if (i < unitData.starLevel)
                lsSpriteStar[i].sprite = UpgradeBoxCtrl.Instance.SpriteStarOn;
            else
                lsSpriteStar[i].sprite = UpgradeBoxCtrl.Instance.SpriteStarOff;
        }
    }

    public void SetSelected(bool selected)
    {
        checkMarkImage.gameObject.SetActive(selected);
    }

    public override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadComponentChild();
    }

    public virtual void LoadComponentChild()
    {
        this.checkMarkImage = transform.Find("Equipped").GetComponent<Image>();
        this.selectButton = GetComponent<Button>();
        this.icon = transform.Find("Icon").GetComponent<Image>();
        this.box_level = transform.Find("box_lv").GetComponent<Image>();
        this.rankUnit = transform.Find("rankText").GetComponent<TextMeshProUGUI>();
        this.currentLevel = transform.Find("levelText").GetComponent<TextMeshProUGUI>();
        this.bg = GetComponent<Image>();
        this.cardCtrl = GetComponentInParent<BaseCardCtrl>();
    }

}


