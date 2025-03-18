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



    public void SetInfo(Sprite icon, Sprite bg, Sprite box_level, TextMeshProUGUI rankUnit, TextMeshProUGUI level)
    {
        this.icon.sprite = icon;
        this.bg.sprite = bg;
        this.box_level.sprite = box_level;
        this.rankUnit = rankUnit;
        this.currentLevel= level;
    }
}
