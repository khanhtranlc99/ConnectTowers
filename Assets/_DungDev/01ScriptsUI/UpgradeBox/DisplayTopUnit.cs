using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayTopUnit : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] Image bg;
    [SerializeField] Image box_level;
    [SerializeField] TextMeshProUGUI rankUnit;
    [SerializeField] TextMeshProUGUI currentLevel;

    [SerializeField] List<Image> lsSpriteStar;

    public void SetInfo(Sprite icon, Sprite bg, Sprite box_level, string rankUnit, string level)
    {
        this.icon.sprite = icon;
        this.icon.SetNativeSize();
        this.bg.sprite = bg;
        this.box_level.sprite = box_level;
        this.rankUnit.text = rankUnit;
        this.currentLevel.text= level;
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
}
