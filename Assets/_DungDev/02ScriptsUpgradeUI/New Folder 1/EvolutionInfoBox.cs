using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EvolutionInfoBox : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameUnits;
    [SerializeField] TextMeshProUGUI levelUnits;
    [SerializeField] Image progessBar;
    [SerializeField] TextMeshProUGUI currentCard;
    [SerializeField] TextMeshProUGUI cardsToUpgrade;
    [Space(10)]
    [SerializeField] List<Image> lsSpriteStar;

    public void UpdateUI(UnitsType unitsType)
    {
        this.SetSpriteStar(unitsType);
        this.SetProgessBarValue(unitsType);
        this.SetCurrentCard(unitsType);
    }


    public void SetSpriteStar(UnitsType unitsType)
    {
        PropertiesUnitsBase unitData = GameController.Instance.dataContain.dataUnits.GetPropertiesBases(unitsType);
        for (int i = 0; i < 5; i++)
        {
            if (i < unitData.starLevel)
                lsSpriteStar[i].sprite = UpgradeBoxCtrl.Instance.SpriteStarOn;
            else
                lsSpriteStar[i].sprite = UpgradeBoxCtrl.Instance.SpriteStarOff;
        }
    }

    public void SetName_LevelUnits(string name, string level)
    {
        this.nameUnits.text = name;
        this.levelUnits.text = level;
    }
    public void SetCurrentCard(UnitsType unitsType)
    {
        PropertiesUnitsBase dataUnit = GameController.Instance.dataContain.dataUnits.GetPropertiesBases(unitsType);
        DataUserGame dataUser = GameController.Instance.dataContain.dataUser;

        this.currentCard.text = dataUser.FindUnitCard(dataUnit).cardCount.ToString();
        this.cardsToUpgrade.text = "/" + dataUnit.GetCostCard.ToString();
    }

    public void SetProgessBarValue(UnitsType unitsType)
    {
        PropertiesUnitsBase dataUnit = GameController.Instance.dataContain.dataUnits.GetPropertiesBases(unitsType);
        DataUserGame dataUser = GameController.Instance.dataContain.dataUser;

        this.progessBar.fillAmount = dataUser.FindUnitCard(dataUnit).cardCount / dataUnit.GetCostCard;
    }

}
