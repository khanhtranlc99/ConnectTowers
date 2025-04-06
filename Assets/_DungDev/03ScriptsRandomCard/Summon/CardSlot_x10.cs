using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardSlot_x10 : LoadAutoComponents
{
    [SerializeField] Image bg;
    [SerializeField] Image iconUnit;
    [SerializeField] TextMeshProUGUI unitName;

    UnitRank unitRank;
    PropertiesUnitsBase currentUnitResult;
    List<PropertiesUnitsBase> lsResultCards = new();
    public void GenerateRandomUnit()
    {
        this.currentUnitResult = GetRandomCard(); // Random và lưu lại 1 lần
    }
    public void ResultUnit()
    {
        if (this.currentUnitResult == null) return;

        GameController.Instance.dataContain.dataUser.AddCards_WaitSave(currentUnitResult, 1);
        this.SetDisPlayCard(currentUnitResult);
        Debug.Log("add thanh cong");
    }
    public void ResultBG()
    {
        if (currentUnitResult == null) return;
        this.bg.sprite = currentUnitResult.FrameRank;
    }

    PropertiesUnitsBase GetRandomCard()
    {
        DataUnits dataUnit = GameController.Instance.dataContain.dataUnits;
        lsResultCards.Clear();

        int rand = GetRandomCardIndex();
        switch (rand)
        {
            case 0:
                unitRank = UnitRank.Common; break;
            case 1:
                unitRank = UnitRank.Uncommon; break;
            case 2:
                unitRank = UnitRank.Rare; break;
            case 3:
                unitRank = UnitRank.Epic; break;
            case 4:
                unitRank = UnitRank.Legend; break;
        }

        foreach (var child in dataUnit.lsPropertiesBases)
        {
            if (child.unitRank == unitRank) lsResultCards.Add(child);
        }
        int randResultCard = Random.Range(0, lsResultCards.Count);
        return lsResultCards[randResultCard];
    }

    int GetRandomCardIndex()
    {
        int rand = Random.Range(0, 100); // Random từ 0 -> 99
        if (rand < 35) return 0;   // 35% Common
        if (rand < 60) return 1;   // 25% Uncommon (35 + 25)
        if (rand < 80) return 2;   // 20% Rare (35 + 25 + 20)
        if (rand < 95) return 3;   // 15% Epic (35 + 25 + 20 + 15)
        return 4;                  // 5% Legend (35 + 25 + 20 + 15 + 5)
    }


    public void SetDisPlayCard(PropertiesUnitsBase unitsBase)
    {
        this.iconUnit.sprite = unitsBase.SpriteUnit;
        this.iconUnit.SetNativeSize();
        this.iconUnit.transform.localScale = new Vector3(0.33f,0.33f,0.33f);
        this.unitName.text = unitsBase.unitType.ToString();
    }

    public void SetDisPlayCardDefault(Sprite sprite)
    {
        this.iconUnit.sprite = sprite;
        this.iconUnit.transform.localScale = Vector3.one;
        this.iconUnit.SetNativeSize();
        this.unitName.text = "";

        Debug.LogError("Default complete");
    }


    public override void LoadComponent()
    {
        base.LoadComponent();
        this.bg = transform.Find("bg").GetComponent<Image>();
        this.iconUnit = transform.Find("icon").GetComponent<Image>();
        this.unitName = transform.Find("unitName").GetComponent<TextMeshProUGUI>();

    }
}
