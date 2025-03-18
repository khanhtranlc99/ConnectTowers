using BestHTTP.SocketIO;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public abstract class UnitSlotBase : LoadAutoComponents
{
    public UnitsType unitsType;
    public PropertiesUnitsBase unitData;
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

    private void Start()
    {
        selectButton.onClick.AddListener(OnClick);
    }
    public abstract void OnClick();

    public void UpgradeUnit()
    {
        Debug.Log(transform.name + "level up");
    }

    public void SetUnit(PropertiesUnitsBase unit)
    {
        unitData = unit;
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
        this.currentLevel = transform.Find("rankText").GetComponent<TextMeshProUGUI>();
        this.bg = GetComponent<Image>();
        this.cardCtrl = GetComponentInParent<BaseCardCtrl>();
    }

}


