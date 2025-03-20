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
        Debug.Log(lsResults[rand] + "Complete Add card");
    }

    public override void LoadComponent()
    {
        base.LoadComponent();
        this.imgChoose = transform.Find("imgChoose").GetComponent<Image>();
    }


}
