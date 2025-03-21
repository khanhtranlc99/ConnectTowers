using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayTopUnit : MonoBehaviour
{
    [SerializeField] CardUnitType CardUnitType;
    [SerializeField] Image icon;
    [SerializeField] Image bg;
    [SerializeField] Image box_level;
    [SerializeField] TextMeshProUGUI rankUnit;
    [SerializeField] TextMeshProUGUI currentLevel;

    [SerializeField] List<Image> lsSpriteStar;


    private void OnEnable()
    {
        switch (CardUnitType)
        {
            case CardUnitType.Soldier:
                this.UpdateUI(GameController.Instance.dataContain.dataUser.CurrentCardSoldier);
                break;
            case CardUnitType.Beast:
                this.UpdateUI(GameController.Instance.dataContain.dataUser.CurrentCardBeast);
                break;
            case CardUnitType.Mage:
                this.UpdateUI(GameController.Instance.dataContain.dataUser.CurrentCardMage);
                break;
        }
    }
    public void UpdateUI(PropertiesUnitsBase unitData)
    {
        this.SetInfo(unitData);
        this.SetSpriteStar(unitData);
    }

    public void SetInfo(PropertiesUnitsBase unitData)
    {
        this.icon.sprite = unitData.SpriteUnit;
        this.icon.SetNativeSize();
        this.bg.sprite = unitData.FrameRank;
        this.box_level.sprite = unitData.BoxRank;
        this.rankUnit.text = unitData.unitRank.ToString();
        this.currentLevel.text= "Level: "+ unitData.currentLevel.ToString();
    }

    public void SetSpriteStar(PropertiesUnitsBase unitData)
    {
        for (int i = 0; i < 5; i++)
        {
            if (i < unitData.starLevel)
                lsSpriteStar[i].sprite = UpgradeBoxCtrl.Instance.SpriteStarOn;
            else
                lsSpriteStar[i].sprite = UpgradeBoxCtrl.Instance.SpriteStarOff;
        }
    }
}
