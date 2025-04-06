using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSlot_x1 : LoadAutoComponents
{
    [SerializeField] int idCard;
    [SerializeField] UnitRank rankCard;
    public Image imgChoose;
    Sprite spriteResult;
    public Sprite SpriteResult => spriteResult;

    string nameUnit;
    public string NameUnit => nameUnit;

    Color colorNameUnit;
    public Color ColorNameUnit => colorNameUnit;
    public void ResultUnit()
    {
        DataUnits dataUnit = GameController.Instance.dataContain.dataUnits;
        List<PropertiesUnitsBase> lsResults = new();
        //duyet qua list propertiesUnit
        foreach (var child in dataUnit.lsPropertiesBases)
        {
            if (child.unitRank == rankCard) lsResults.Add(child);
        }

        int rand = Random.Range(0, lsResults.Count);
        //add card 
        GameController.Instance.dataContain.dataUser.AddCards(lsResults[rand], 1);
        spriteResult = lsResults[rand].SpriteUnit;
        nameUnit = lsResults[rand].unitType.ToString();
        this.SetColorNameUnit(lsResults[rand]);

        Debug.Log(lsResults[rand] + "Complete Add card");
    }

    public void SetColorNameUnit(PropertiesUnitsBase unitData)
    {
        switch (unitData.unitRank)
        {
            case UnitRank.Uncommon:
                this.colorNameUnit = Color.green;
                break;
            case UnitRank.Rare:
                this.colorNameUnit = new Color32(0, 122, 255, 255);
                break;
            case UnitRank.Epic:
                this.colorNameUnit = new Color32(175, 82, 222, 255);
                break;
            case UnitRank.Legend:
                this.colorNameUnit = new Color32(255, 159, 0, 255);
                break;
            default:
                this.colorNameUnit = Color.white;
                break;
        }
    }

    public override void LoadComponent()
    {
        base.LoadComponent();
        this.imgChoose = transform.Find("imgChoose").GetComponent<Image>();
    }

}
