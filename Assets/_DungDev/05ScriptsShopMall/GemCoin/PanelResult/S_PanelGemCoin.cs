using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class S_PanelGemCoin : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI txtAmount;




    public void SetDisplayResult(Sprite sprite, string amount)
    {
        this.icon.sprite = sprite;
        this.txtAmount.text = amount;
    }
}
